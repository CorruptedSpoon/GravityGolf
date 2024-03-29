﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GravityGolf
{
    class Button
    {
        protected Rectangle rect;
        protected Texture2D normalTexture;
        protected Texture2D hoverTexture; //if we want to do it, we can change it in the universe class
        protected bool prevMouseDown = false;

        public int X {
            get { return rect.X; }
        }

        public int Y { 
            get { return rect.Y; }
        }

        public Button(Rectangle rect, Texture2D normalTexture, Texture2D hoverTexture) { 
            this.rect = rect;
            this.normalTexture = normalTexture;
            this.hoverTexture = hoverTexture;
        }

        private bool mouseHover(MouseState mouse) //Checks if mouse is hovering - potentially for hover texture
        {
            if(mouse.X < rect.X + normalTexture.Width && mouse.X > rect.X && mouse.Y < rect.Y + normalTexture.Height && mouse.Y > rect.Y)
            {
                return true;
            }
            return false;
        }

        public bool IsClick(MouseState cur, MouseState pre) //Checks click, returns true once, usable in update
        {
            if(mouseHover(cur) && cur.LeftButton == ButtonState.Released && pre.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch sb, MouseState mouse){
            if(mouseHover(mouse)){
                sb.Draw(hoverTexture, rect, Color.White);
            }
            else{ 
                sb.Draw(normalTexture, rect, Color.White);
            }
        }

        public void Update(MouseState cur, MouseState pre){
            mouseHover(cur);
            if(IsClick(cur, pre)) {
                //do something on click
            }
        }
    }
}
