using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityGolf {
    class Ball : GameObject {

        // -----Fields-----
        public Vector2 direction;



        // -----Constructor-----

        /// <summary>
        /// Constructor for the ball
        /// </summary>
        /// <param name="center">center point of the ball</param>
        /// <param name="radius">radius of the ball</param>
        /// <param name="mass">mass of the ball</param>
        /// <param name="texture">texture of the ball</param>
        /// <param name="color">color of the ball</param>
        public Ball(Vector2 center, int radius, float mass, Texture2D texture = null, Color? color = null) : base(center, radius, mass, texture, color) {
        }

        // -----Methods-----

        public void Translate() {
            center += direction;
        }

        public void Accelerate(Vector2 acc) {
            direction += acc;
        }
        public void Unclip(Planet planet) {
            float distance = planet.Radius - Vector2.Distance(planet.Center, center);
            Vector2 toMove = planet.UnitNormalAt(center) * (distance + radius);
            center += toMove;
        }
        public void Teleport(int x, int y) {
            center.X = x;
            center.Y = y;
        }
    }
}
