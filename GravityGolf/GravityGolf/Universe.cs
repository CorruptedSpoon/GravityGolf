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
        /*Gravitational Constant*/ private double G = (6.674f * Math.Pow(10, -11));
        
        public Universe()
        {

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
            ball.Accelerate(ForceAt(ball.Center));
            ball.Translate();
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
                            planets.Add(new Planet(vector, 100, 10, content.Load<Texture2D>("PlanetSmall"), Color.White));
                            break;

                        case (int)PlanetType.medium:
                            planets.Add(new Planet(vector, 200, 20, content.Load<Texture2D>("PlanetMedium"), Color.White));
                            break;

                        case (int)PlanetType.big:
                            planets.Add(new Planet(vector, 300, 30, content.Load<Texture2D>("PlanetBig"), Color.White));
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
    }
}
