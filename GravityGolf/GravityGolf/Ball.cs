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
        private Vector2 direction;

        // -----Properties-----
        //They're get properties for the fields please don't make me comment
        //every single one of these
        public Vector2 Direction { get { return direction; } }

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
    }
}
