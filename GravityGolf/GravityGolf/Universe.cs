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

		private Vector2? click1;
		private Vector2? click2;

		private const float LaunchStrength = 0.05f;
		private const int G = 150;

		ButtonState oldState;

        Game1 game;

        public Universe(Game1 game)
        {
			click1 = null;
			click2 = null;
            this.game = game;
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

        public void Draw(SpriteBatch sb)
        {
            foreach (Planet planet in planets)
            {
                planet.Draw(sb);
            }
            ball.Draw(sb);
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
				else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{
					click2 = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
				}
				else if (oldState == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
				{
                    if (!touching.IsInside(ball.Center - ball.Radius * touching.UnitNormalAt(ball.Center) + LaunchStrength * ((Vector2)click2 - (Vector2)click1))) {
                        ball.Accelerate(LaunchStrength * ((Vector2)click2 - (Vector2)click1));
                    }
					Console.WriteLine(((Vector2)click1).X);
					Console.Write(click2);
				}
			}
			else
			{
				ball.Accelerate(G * ForceAt(ball.Center));
			}
			
            if(planetIntersect != planetIntersectChange) //this seems very wrong; why should stroke increase whenever I land on or leave a planet rather than when I hit the ball?
            {
                //Increase stroke
            }
            planetIntersectChange = planetIntersect;

			oldState = Mouse.GetState().LeftButton;
			ball.Translate(); // we always do this or we get stuck.  Time cannot freeze, to stop just make Direction <0, 0>

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) {
                game.state = GameState.Paused;
            }
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
    }
}
