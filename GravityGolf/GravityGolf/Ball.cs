using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityGolf {
    class Ball : GameObject {
        public Ball(Vector2 center, int radius, float mass, Texture2D texture, Color color) : base(center, radius, mass, texture, color) {

        }

    }
}
