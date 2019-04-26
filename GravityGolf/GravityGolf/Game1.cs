using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

public enum GameState
{
	Menu, //main menu
	Playing, //playing the game
	Paused, //paused within the game
    LevelComplete, //level has been completed
	GameWon, //game has been won (level 9 complete)
    LevelSelect //level select submenu
}

namespace GravityGolf
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int[] hiScores;

        public GameState state;
        Universe universe;
        StartMenu startMenu;
        PauseMenu pauseMenu;
        LevelMenu levelMenu;
        LevelComplete levelComplete;
        GameWon gameWon;

        int level;

        KeyboardState currentState;
        KeyboardState previousState;

        MouseState currentMouseState;
        MouseState previousMouseState;

        Texture2D background;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            universe = new Universe(GraphicsDevice, Content);
            startMenu = new StartMenu(Content);
            pauseMenu = new PauseMenu(Content);
            levelMenu = new LevelMenu(Content);
            levelComplete = new LevelComplete(Content);
            gameWon = new GameWon(Content);

            level = 1;
			state = GameState.Menu;

            hiScores = new int[9];
            
            IsMouseVisible = true;

            FileStream outStream = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;

            if (!File.Exists("hi.score"))
            {
                for (int i = 0; i < 9; i++)
                    hiScores[i] = 0;

                outStream = File.OpenWrite("hi.score");
                writer = new BinaryWriter(outStream);
                foreach(int i in hiScores)
                {
                    writer.Write(i);
                }
                writer.Close();
            }
            else
            {
                outStream = File.OpenRead("hi.score");
                reader = new BinaryReader(outStream);
                for(int i = 0; i < 9; i++)
                {
                    hiScores[i] = reader.ReadInt32();
                }
                reader.Close();
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Background");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            ///finite state machine
            switch (state)
            {
                case GameState.Menu:
                    startMenu.Update(currentMouseState, previousMouseState);
                    if (startMenu.Play == true) {
                        universe.Clear();
                        universe.LoadLevel("Content\\levels\\level" + level + ".level");
                        state = GameState.Playing;
                    }
                    else if (startMenu.Level)
                        state = GameState.LevelSelect;
                    else if ((currentState.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape)) || startMenu.Exit)
                        Exit();
					break;

                case GameState.LevelSelect:
                    levelMenu.Update(currentMouseState, previousMouseState);
                    if(levelMenu.MenuClick == true)
                    {
                        state = GameState.Menu;
                        universe.Clear();
                    }
                    for (int i = 0; i < 9; i++) { 
                        if (levelMenu.levelButtonsClick[i])
                        {
                            universe.Clear();
                            universe.LoadLevel("Content\\levels\\level" + (i + 1) + ".level");
                            level = i + 1;
                            state = GameState.Playing;
                        }
                    }
                    break;

				case GameState.Playing:
					universe.Update();
                    if (universe.hole.InGoal(universe.ball)) {
                        if (level == 9)
                            state = GameState.GameWon;
                        else
                            state = GameState.LevelComplete;
                    }
                    else if (currentState.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape))
                        state = GameState.Paused;
					break;

				case GameState.Paused:
                    pauseMenu.Update(currentMouseState, previousMouseState);
                    if (pauseMenu.PlayClick)
                        state = GameState.Playing;
                    else if (pauseMenu.MenuClick) {
                        universe.LoadLevel("Content\\levels\\level" + level + ".level");
                        state = GameState.Menu;
                    }
                    else if (pauseMenu.ExitClick)
                        Exit();                   
					break;

                case GameState.LevelComplete:
                    levelComplete.Update(currentMouseState, previousMouseState);
                    if (levelComplete.PlayClick)
                    {
                        universe.Clear();
                        if (hiScores[level - 1] > universe.Strokes)
                            hiScores[level - 1] = universe.Strokes;
                        level++;
                        universe.LoadLevel("Content\\levels\\level" + level + ".level");
                        state = GameState.Playing;
                    }
                    else if (levelComplete.MenuClick)
                        state = GameState.Menu;
                    break;

				case GameState.GameWon:
                    level = 1;
                    gameWon.Update(currentMouseState, previousMouseState);
                    if (gameWon.MenuClick)
                        state = GameState.Menu;
                    else if (gameWon.ExitClick)
                        Exit();
					break;
			}

            previousState = currentState;
            previousMouseState = currentMouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background,new Rectangle(0,0,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight),Color.White);
			switch (state)
			{
				case GameState.Menu:
                    startMenu.Draw(spriteBatch);
					break;
                case GameState.LevelSelect:
                    levelMenu.Draw(spriteBatch);
                    break;
				case GameState.Playing:
					universe.Draw(graphics.GraphicsDevice, spriteBatch);
					break;
				case GameState.Paused:
                    pauseMenu.Draw(spriteBatch);
					break;
                case GameState.LevelComplete:
                    levelComplete.Draw(spriteBatch);
                    break;
				case GameState.GameWon:
                    gameWon.Draw(spriteBatch);
					break;
			}
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}