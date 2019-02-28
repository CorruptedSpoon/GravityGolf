using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

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
        int level;
        int numStrokes;

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

            universe = new Universe();
            level = 0;

            //creating an example level 1 using LevelWriter
            List<PlanetStruct> level1 = new List<PlanetStruct>();
            level1.Add(new PlanetStruct(220, 200, PlanetType.medium));
            level1.Add(new PlanetStruct(1100, 300, PlanetType.small));
            level1.Add(new PlanetStruct(700, 500, PlanetType.big));
            LevelWriter.WriteLevel("level1", 60, 60, level1);
            
            /*BinaryWriter output = null;
            try
            {
                Stream outStream = File.OpenWrite("1.level");
                output = new BinaryWriter(outStream);

                //--Ball--//
                output.Write(20f); //x of ball
                output.Write(20f); //y of ball
                output.Write(5); //radius
                output.Write("red");//texture

                output.Write((byte)1); //number of planets

                //--Planet--//
                output.Write(100f); //x of planet
                output.Write(100f); //y of planet
                output.Write(50); //radius
                output.Write(150); //mass
                output.Write("red");//texture
            }
            finally
            {
                if (output != null)
                    output.Close();
            }*/
            NextLevel();

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            universe.Update();

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
            universe.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void NextLevel()
        {
            numStrokes = 0;
            level++;
            universe.Clear();

            /*BinaryReader input = null;
            try {
                Stream inStream = File.OpenRead(level+".level");
                input = new BinaryReader(inStream);

                universe.SetBall(new Ball(
                    new Vector2(input.ReadSingle(), input.ReadSingle()),
                        input.ReadInt32(),
                        1,
                        Content.Load<Texture2D>(input.ReadString())));
                byte i = 0;
                byte total = input.ReadByte();//number of planets
                while (i++<total)//inStream.Position < inStream.Length) 
                {
                    universe.Add(new Planet(
                        new Vector2(input.ReadSingle(), input.ReadSingle()), 
                        input.ReadInt32(), 
                        input.ReadInt32(),
                        Content.Load<Texture2D>(input.ReadString()) ));
                }
            } 
            finally {
                if (input!=null)
                    input.Close();
            }*/

            universe.LoadLevel("level1.level", Content);
        }
    }
}
