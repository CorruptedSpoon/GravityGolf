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
    abstract class Button
    {
        protected int x;
        protected int y;
        protected Texture2D normalTexture;
        protected Texture2D hoverTexture; //if we want to do it, we can change it in the universe class
        protected bool prevMouseDown = false;

        public bool mouseHover(MouseState mouse)
        {
            if(mouse.X < x + normalTexture.Width && mouse.X > x && mouse.Y < y + normalTexture.Height && mouse.Y > y)
            {
                return true;
            }
            return false;
        }

        public bool isClick(MouseState mouse)
        {
            
            if(mouseHover(mouse) && mouse.LeftButton == ButtonState.Released && prevMouseDown == true)
            {
                return true;
            }
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                prevMouseDown = true;
            }
            else
            {
                prevMouseDown = false;
            }
            return false;
        }
    }
}
