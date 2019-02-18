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
        //Ball ball = new Ball;

        //Gets gravitational force at position pos
        /*public ForceAt(Point pos)
        {
            
        }
        */

        void Add(Planet p)
        {
            planets.Add(p);
        }

        void Draw()
        {

        }

        void Update()
        {

        }

        void Clear()
        {

        }
    }
}
