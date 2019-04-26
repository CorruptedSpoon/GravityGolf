using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GravityGolf
{
    class Universe
    {
        List<Planet> planets = new List<Planet>();
        public Ball ball;
        public Hole hole;

		private Vector2? click1;
		private Vector2? click2;

		private const float LaunchStrength = 0.05f;
		private const int G = 150;

		ButtonState oldState;

        private bool planetIntersect;
        
        private int strokes;

        private GraphicsDevice graphics;

        private ContentManager content;
        private SpriteFont font;

        public int Strokes {
            get { return strokes; }
        }

        /// <summary>
        /// Creates a new empty Universe
        /// </summary>
        public Universe(GraphicsDevice graphics, ContentManager content)
        {
			click1 = null;
			click2 = null;
            planetIntersect = false;
            this.graphics = graphics;
            this.content = content;
            font = this.content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// Returns the net gravitational force vector at position pos
        /// </summary>
        /// <param name="pos">The position at which to calculate the force</param>
        /// <returns>the net gravitational force vector at position pos</returns>
        public Vector2 ForceAt(Vector2 pos)
        {
            //this method had a linear gravity that gets stronger the farther away you are
            //the line inside the loop should br force+=planet.ForceAt(pos);
            Vector2 force = new Vector2();
            foreach(Planet planet in planets)
            {
                force += planet.ForceAt(pos);
            }
            if(hole.onPlanet == false)
            {
                force += hole.ForceAt(pos);
            }
            return force;
        }

        /// <summary>
        /// returns the escape velocity when travelling perpendicular to the center of the grvitational system at position p 
        /// </summary>
        /// <param name="pos">the position vector at which to calculate the escape velocity</param>
        /// <returns>the escape velocity at point p</returns>
        private float EscapeVelocityAt(Vector2 pos)
        {
            Vector2 massPos = new Vector2(0, 0); //sum of products of positions and masses
            float mass = 0;
            foreach(Planet p in planets)
            {
                Vector2 displacement = pos - p.Center;
                massPos = p.Mass * displacement;
                mass += p.Mass;
            }
            Vector2 center = massPos / mass;
            return (float)Math.Sqrt(2 * G * mass / center.Length());
        }

        /// <summary>
        /// Adds planet p to this Universe
        /// </summary>
        /// <param name="p">The planet to add.  Must not be null.</param>
        public void Add(Planet p)
        {
            planets.Add(p);
        }

        //I just threw this method here for now.  This seems pretty horrible as the ball should not be changable.  We can figure out a better way later.
        public void SetBall(Ball b)
        {
            ball = b;
        }
        public void SetHole(Hole h)
        {
            hole = h;
        }

        /// <summary>
        /// Draws all of this Universe's content
        /// </summary>
        /// <param name="graphicsDevice">the GraphicsDevice to use to draw the assets</param>
        /// <param name="sb">the SpriteBatch to use to draw the assets</param>
        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch sb)
        {
            float xMultiplier = Math.Sign(ball.X - graphicsDevice.Viewport.Width / 2);
            float yMultiplier = Math.Sign(ball.Y - graphicsDevice.Viewport.Height / 2);
            float xForZoom = ball.X + xMultiplier * ball.Radius * 3;
            float yForZoom = ball.Y + Math.Sign(ball.Y - graphicsDevice.Viewport.Height / 2) * ball.Radius * 3;

            float scale = 1f;
            if (xForZoom > graphicsDevice.Viewport.Width || xForZoom < 0 || yForZoom > graphicsDevice.Viewport.Height || yForZoom<0)
                scale = Math.Min(graphicsDevice.Viewport.Width / (2*(xForZoom - graphicsDevice.Viewport.Width/2) * xMultiplier),
                    graphicsDevice.Viewport.Height / (2 * (yForZoom - graphicsDevice.Viewport.Height / 2) * yMultiplier));

            foreach (Planet planet in planets)
            {
                planet.Draw(graphicsDevice, sb, scale);
            }
            ball.Draw(graphicsDevice, sb, scale);
            hole.Draw(graphicsDevice, sb, scale);
            if (!FirstClick() && Mouse.GetState().LeftButton == ButtonState.Pressed && !(click1==null||click2==null))
                DrawArc(graphicsDevice, sb, ball.Center, LaunchStrength*((Vector2)click1 - (Vector2)click2), 50,
                    LaunchStrength * ((Vector2)click1 - (Vector2)click2).Length() < EscapeVelocityAt(ball.Center)?(Color?)null:Color.Red);

            sb.DrawString(font, "Strokes: " + strokes, new Vector2(30, 30), Color.White);
        }

        /// <summary>
        /// Runs all necessary logic for player input and collision detection
        /// </summary>
        public void Update() //Check win condition, move ball
        {
            bool planetIntersectChange = false;
            Planet touching = null;
            foreach(Planet planet in planets)
            {
                //check to see if the ball is experiencing collision w/ any of the planets
                if (planet.IsInside(ball.Center - ball.Radius * planet.UnitNormalAt(ball.Center))) {
                    planetIntersect = true;
                    touching = planet;
                    ball.Unclip(touching);
                    break;
                }
                else {
                    planetIntersect = false;
                    touching = null;
                }
            }
            if (planetIntersect) //iff in a planet
            {
                ball.Accelerate(-ball.direction); //apply normal force
				//----player controls----
				if (FirstClick())
				{
					click1 = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
				}
				else if (Mouse.GetState().LeftButton == ButtonState.Pressed) //while dragging
				{
					click2 = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
				}
				else if (oldState == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released && click1 != null)
				{
                    if (!touching.IsInside(ball.Center - ball.Radius * touching.UnitNormalAt(ball.Center) + (LaunchStrength * ((Vector2)click1 - (Vector2)click2)))) {
                        ball.Accelerate(LaunchStrength * ((Vector2)click1 - (Vector2)click2));
                        strokes++;
                    }
				}
			}
			else
			{
				ball.Accelerate(G * ForceAt(ball.Center));

                //click automatically are nulled if you aren't touching a planet
                click1 = null;
                click2 = null;
			}
			
            planetIntersectChange = planetIntersect;

			oldState = Mouse.GetState().LeftButton;

			ball.Translate(); // we always do this or we get stuck.  Time cannot freeze, to stop just make Direction <0, 0>
		}

        /// <summary>
        /// removes all planets from this Universe
        /// </summary>
        public void Clear()
        {
            planets.Clear();
        }

        //planets can be determined by a vector2, and a PlanetType enum. The enum will determine the mass, radius, and texture of the planet.
        /// <summary>
        /// Loads a level from a file
        /// </summary>
        /// <param name="level">The filename</param>
        /// <param name="content">The content manager used to load the files</param>
        public void LoadLevel(string level)
        {
            strokes = 0;
            //levelNum = int.Parse(level.Substring(20, 1)); 
            BinaryReader input = null;
            try
            {
                Stream inStream = File.OpenRead(level);
                input = new BinaryReader(inStream);

                //numbers for radius and mass here should be constant, numbers that I put should be changed
                SetBall(new Ball(new Vector2(input.ReadInt32(), input.ReadInt32()),10,1,content.Load<Texture2D>("red")));
                //test hole
                SetHole(new Hole(new Vector2(input.ReadInt32(), input.ReadInt32()), 10, 10, content.Load<Texture2D>("hole"), Color.White, false));

                int num = input.ReadInt32();

                for(int x = 0; x < num; x++)
                {
                    int vx = input.ReadInt32();
                    int vy = input.ReadInt32();
                    Vector2 vector = new Vector2(vx, vy);
                    //determines the statistics of each planet type, change numbers for planets as seen fit
                    switch (input.ReadInt32())
                    {
                        case (int)PlanetType.small:
                            planets.Add(new Planet(vector, 100, 30, content.Load<Texture2D>("PlanetSmall"), Color.White));
                            break;

                        case (int)PlanetType.medium:
                            planets.Add(new Planet(vector, 150, 68, content.Load<Texture2D>("PlanetMedium"), Color.White));
                            break;

                        case (int)PlanetType.big:
                            planets.Add(new Planet(vector, 200, 120, content.Load<Texture2D>("PlanetBig"), Color.White));
                            break;
                    }
                }
            }
            catch
            {
                Console.Write("NO LEVELS TO LOAD");
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }



        /// <summary>
        /// Returns whether the left mouse button was just hit
        /// </summary>
        /// <returns>whether the left mouse button was just hit</returns>
		private bool FirstClick()
		{
            return Mouse.GetState().LeftButton == ButtonState.Pressed && oldState == ButtonState.Released;

        }

        /// <summary>
        /// Draws the trajectory of a particle launched at velocity for pos over iteration frames
        /// </summary>
        /// <param name="graphicsDevice">the GraphicsDevice to use</param>
        /// <param name="sb">the spritebatch with which to draw the path.  Begin() must have already been called</param>
        /// <param name="pos">the initial position of the particle</param>
        /// <param name="velocity">the initial velocity of the particle</param>
        /// <param name="iterations">the number of future frames over which to draw the trajectory</param>
        /// <param name="color">the color in which to draw the arc</param>
        private void DrawArc(GraphicsDevice graphicsDevice ,SpriteBatch sb, Vector2 pos, Vector2 velocity, int iterations, Color? color = null)
        {
            float xMultiplier = Math.Sign(ball.X - graphicsDevice.Viewport.Width / 2);
            float yMultiplier = Math.Sign(ball.Y - graphicsDevice.Viewport.Height / 2);
            float xForZoom = ball.X + xMultiplier * ball.Radius * 3;
            float yForZoom = ball.Y + Math.Sign(ball.Y - graphicsDevice.Viewport.Height / 2) * ball.Radius * 3;

            float scale = 1f;
            if (xForZoom > graphicsDevice.Viewport.Width || xForZoom < 0 || yForZoom > graphicsDevice.Viewport.Height || yForZoom < 0)
                scale = Math.Min(graphicsDevice.Viewport.Width / (2 * (xForZoom - graphicsDevice.Viewport.Width / 2) * xMultiplier),
                    graphicsDevice.Viewport.Height / (2 * (yForZoom - graphicsDevice.Viewport.Height / 2) * yMultiplier));

            Texture2D tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData(new Color[] { Color.White }, 0, 1);
            Vector2 nextPos;
            for(int i = 0; i<iterations; i++)
            {
                nextPos = pos + velocity;
                velocity += G * ForceAt(nextPos);
                int xCenter = (int)(graphicsDevice.Viewport.Width / 2 + (pos.X - graphicsDevice.Viewport.Width / 2) * scale);
                int yCenter = (int)(graphicsDevice.Viewport.Height / 2 + (pos.Y - graphicsDevice.Viewport.Height / 2) * scale);
                sb.Draw(tex, 
                    new Vector2(xCenter, yCenter), 
                    null, 
                    color==null?Color.White:(Color)color, 
                    (float) Math.Atan2((nextPos - pos).Y, (nextPos - pos).X), 
                    new Vector2(0, 0), 
                    new Vector2((nextPos - pos).Length(), 1), 
                    SpriteEffects.None, 1);
                pos = nextPos;
            }
        }

        
    }
}