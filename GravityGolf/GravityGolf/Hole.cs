using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityGolf
{
    class Hole : GameObject
    {
        public bool onPlanet;
        public Hole(Vector2 center, int radius, float mass, Texture2D tex, Color? color, bool onPlanet) : base(center, radius, mass, tex, color)
        {
            if (onPlanet == true)
            {
                this.mass = 0;
            }
            else
            {
                this.mass = 10;
            }
            this.radius = 20;
        }

        public bool InGoal(Vector2 ball)
        {
            //If ball is intersecting with hole, return true
            if(Center.X + Radius <= ball.X && Center.X - Radius >= ball.X && Center.Y + Radius <= ball.Y && Center.Y - Radius >= ball.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2 ForceAt(Vector2 p)
        {
            return -(mass / (p - center).LengthSquared()) * UnitNormalAt(p);
        }

        public Vector2 UnitNormalAt(Vector2 p)
        {
            Vector2 unitNorm = p - center;
            unitNorm.Normalize();
            return unitNorm;
        }
    }
}
