using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

public enum GameState
{
	Menu, 
	Playing,
	Paused,
	Complete,//player completes all holes; menu gives stats (distinct from Menu state, aka not a submenu)
    Tool 
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

        Universe universe;
        StartMenu startMenu;
        PauseMenu pauseMenu;
        int level;
        int numStrokes;
		public GameState state;

        KeyboardState currentState;
        KeyboardState previousState;

        MouseState currentMouseState;
        MouseState previousMouseState;

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
            //testing
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            universe = new Universe(GraphicsDevice);
            startMenu = new StartMenu(Content);
            pauseMenu = new PauseMenu(Content);

            level = 0;
			state = GameState.Menu;

            if (Program.tool)
            {
                state = GameState.Tool;
            }

            //creating an example level 1 using LevelWriter
            List<PlanetStruct> level1 = new List<PlanetStruct>();
            level1.Add(new PlanetStruct(220, 200, PlanetType.medium));
            level1.Add(new PlanetStruct(1100, 300, PlanetType.small));
            level1.Add(new PlanetStruct(700, 500, PlanetType.big));
            LevelWriter.WriteLevel("level1", 1200, 800, level1);
            
            NextLevel();

            IsMouseVisible = true;

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            //finite state maching works as follows:
            //from menu, space to start, esc to close program
            //from game, space to pause
            //from pause menu, space to resume, esc to main menu

			switch (state)
			{
				case GameState.Menu:
                    startMenu.Update(currentMouseState, previousMouseState);
                    if ((currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))||startMenu.play == true)
                        state = GameState.Playing;
                    else if (currentState.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape))
                        Exit();
					break;
				case GameState.Playing:
					universe.Update();
                    if (currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))
                        state = GameState.Paused;
					break;
				case GameState.Paused:
                    if (currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))
                        state = GameState.Playing;
                    else if (currentState.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape))
                        state = GameState.Menu;
                    pauseMenu.Update();
					break;
				case GameState.Complete:
					break;
                case GameState.Tool:
                    if (Program.toolUpdate)
                    {
                        universe.Clear();
                        universe.LoadLevel(Program.level, Content);
                        Program.toolUpdate = false;
                    }
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
			switch (state)
			{
				case GameState.Menu:
                    startMenu.Draw(spriteBatch);
					break;
				case GameState.Playing:
					universe.Draw(graphics.GraphicsDevice, spriteBatch);
					break;
				case GameState.Paused:
                    pauseMenu.Draw(spriteBatch);
					break;
				case GameState.Complete:
					break;
                case GameState.Tool:
                    if (Program.toolDraw)
                    {
                        if (!Program.toolUpdate)
                        {
                            universe.Draw(GraphicsDevice, spriteBatch);
                            Program.toolDraw = false;
                        }
                    }
                    break;
			}
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void NextLevel()
        {
            numStrokes = 0;
            level++;
            universe.Clear();

            universe.LoadLevel("level1.level", Content);
        }
    }
}
