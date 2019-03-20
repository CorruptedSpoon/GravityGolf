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
        Ball ball;
        Hole hole;

		private Vector2? click1;
		private Vector2? click2;

		private const float LaunchStrength = 0.05f;
		private const int G = 150;

		ButtonState oldState;

        public Universe()
        {
			click1 = null;
			click2 = null;
        }
        
        //Gets gravitational force at position pos
        public Vector2 ForceAt(Vector2 pos)
        {
            //this method had a linear gravity that gets stronger the farther away you are
            //the line inside the loop should br force+=planet.ForceAt(pos);
            Vector2 force = new Vector2();
            foreach(Planet planet in planets)
            {
                force += planet.ForceAt(pos);
            }
            if(hole.onPlanet == true)
            {
                force += hole.ForceAt(pos);
            }
            return force;
        }

        public void Add(Planet p)
        {
            planets.Add(p);
        }

        //I just threw this method here for now.  This seems pretty horrible as the ball should not be changable.  We can figure out a better way later.
        public void SetBall(Ball b) //Maybe put in constructor?
        {
            ball = b;
        }
        public void SetHole(Hole h)
        {
            hole = h;
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch sb)
        {
            foreach (Planet planet in planets)
            {
                planet.Draw(sb);
            }
            ball.Draw(sb);
            hole.Draw(sb);
            if (!FirstClick() && Mouse.GetState().LeftButton == ButtonState.Pressed && !(click1==null||click2==null))
                DrawArc(graphicsDevice, sb, ball.Center, LaunchStrength*((Vector2)click2 - (Vector2)click1), 50);
        }

        public void Update() //Check win condition, move ball
        {
            //Need to make the hole point, check if ball is in that point, then win
            bool planetIntersect = false;
            bool planetIntersectChange = false;
            Planet touching = null;
            foreach(Planet planet in planets)
            {
                //check to see if the ball is experiencing collision w/ any of the planets
				if (planet.IsInside(ball.Center - ball.Radius*planet.UnitNormalAt(ball.Center)))
                {
                    planetIntersect = true;
                    touching = planet;
                    ball.Unclip(touching);
                }
            }
            if (planetIntersect == true) //iff in a planet
            {
                ball.Accelerate(-ball.Direction); //apply normal force
				//----player controls----
				if (FirstClick())
				{
					click1 = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
				}
				else if (Mouse.GetState().LeftButton == ButtonState.Pressed) //while dragging
				{
					click2 = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
				}
				else if (oldState == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
				{
                    //if (!touching.IsInside(ball.Center - ball.Radius * touching.UnitNormalAt(ball.Center) + LaunchStrength * ((Vector2)click2 - (Vector2)click1))) {
                        ball.Accelerate(LaunchStrength * ((Vector2)click2 - (Vector2)click1));
                    //}
				}
			}
			else
			{
				ball.Accelerate(G * ForceAt(ball.Center));
			}

            //Checking if ball in goal
            if(hole.InGoal(ball) == true)
            {
                Console.WriteLine("IN GOAL");
            }
			
            if(planetIntersect != planetIntersectChange) //this seems very wrong; why should stroke increase whenever I land on or leave a planet rather than when I hit the ball?
            {
                //Increase stroke
            }
            planetIntersectChange = planetIntersect;

			oldState = Mouse.GetState().LeftButton;
			ball.Translate(); // we always do this or we get stuck.  Time cannot freeze, to stop just make Direction <0, 0>
		}

        public void Clear()
        {
            planets.Clear();
        }

        //loads a level using a string for the .level
        //planets can be determined by a vector2, and a PlanetType enum. The enum will determine the mass, radius, and texture of the planet.
        public void LoadLevel(string level, ContentManager content)
        {
            BinaryReader input = null;
            try
            {
                Stream inStream = File.OpenRead(level);
                input = new BinaryReader(inStream);

                //numbers for radius and mass here should be constant, numbers that I put should be changed
                SetBall(new Ball(new Vector2(input.ReadInt32(), input.ReadInt32()),10,1,content.Load<Texture2D>("red")));
                //test hole
                SetHole(new Hole(new Vector2(1200, 800), 10, 10, content.Load<Texture2D>("red"), Color.Blue, false));

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
            finally
            {
                if (input != null)
                    input.Close();
            }
        }

		private bool FirstClick()
		{
			if (Mouse.GetState().LeftButton == ButtonState.Pressed && oldState == ButtonState.Released)
				return true;
			return false;
		}

        /// <summary>
        /// Draws the trajectory of a particle launched at velocity for pos over iteration frames
        /// </summary>
        /// <param name="graphicsDevice">the GraphicsDevice to use</param>
        /// <param name="sb">the spritebatch with which to draw the path.  Begin() must have already been called</param>
        /// <param name="pos">the initial position of the particle</param>
        /// <param name="velocity">the initial velocity of the particle</param>
        /// <param name="iterations">the number of future frames over which to draw the trajectory</param>
        private void DrawArc(GraphicsDevice graphicsDevice ,SpriteBatch sb, Vector2 pos, Vector2 velocity, int iterations)
        {
            Texture2D tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData(new Color[] { Color.White }, 0, 1);
            Vector2 nextPos;
            for(int i = 0; i<iterations; i++)
            {
                velocity += G * ForceAt(pos);
                nextPos = pos + velocity;
                sb.Draw(tex, 
                    pos, 
                    null, 
                    Color.White, 
                    (float) Math.Atan2((nextPos - pos).Y, (nextPos - pos).X), 
                    new Vector2(0, 0), 
                    new Vector2((nextPos - pos).Length(), 1), 
                    SpriteEffects.None, 1);
                pos = nextPos;
            }
        }
    }
}
