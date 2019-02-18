using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GravityGolf
{
    class Universe
    {
        List<Planet> planets = new List<Planet>();
        Ball ball;
        //Gravitational Constant
        private double G = (6.674f * Math.Pow(10, -11));
        
        public Universe()
        {

        }
        
        //Gets gravitational force at position pos
        public Vector2 ForceAt(Point pos)
        {
            //this method had a linear gravity that gets stronger the farther away you are
            //the line inside the loop should br force+=planet.ForceAt(pos);
            Vector2 force = new Vector2();
            foreach(Planet planet in planets)
            {
                force.X += planet.X - pos.X;
                force.Y += planet.Y - pos.Y;
            }
            return force;
        }

        public void Add(Planet p)
        {
            planets.Add(p);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Planet planet in planets)
            {//Where are we going to put the texture file? With the object or just load it onto here?
                sb.Draw(null, new Vector2(planet.X, planet.Y), Color.White);
            }
        }

        public void Update() //Check win condition, move ball
        {

        }

        public void Clear()
        {
            planets.Clear();
        }
    }
}
