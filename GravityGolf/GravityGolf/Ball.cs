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
        private float x;
        private float y;
        private float lastX;
        private float lastY;

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
            lastX = center.X;
            x = center.X;
            lastY = center.Y;
            y = center.Y;
        }

        // -----Methods-----

        /// <summary>
        /// Tests to see if the ball is moving
        /// </summary>
        /// <param name="center">the current center of the ball</param>
        /// <returns>returns true if the ball is moving</returns>
        public bool IsMoving(Vector2 center) {
            if (lastX == x)
                return true;
            else
                return false;
        }

        /// <summary>
        /// updates the ball logic and fields
        /// </summary>
        public void Update() {
            lastX = x;
            x = center.X;
            lastY = y;
            y = center.Y;
        }
    }
}
