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
        //Hole mass is set, 5 works well and anything higher feels too easy
        //Hole radius - always 20
        public Hole(Vector2 center, int radius, float mass, Texture2D tex, Color? color, bool onPlnt) : base(center, radius, mass, tex, color)
        {
            onPlanet = onPlnt;
            if (onPlanet == true)
            {
                this.mass = 0;
            }
            else
            {
                this.mass = 5;
            }
            this.radius = 20;
        }

        //Checks if center of ball is in radius of goal
        public bool InGoal(Ball ball)
        {
            return (ball.Center - center).Length() <= radius;
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
