using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GravityGolf
{
    class GameWon
    {
        MouseState currentState;
        MouseState previousState;

        Texture2D gameWonOverlay;

        Button menuButton;
        Button exitButton;

        public bool menuClick;
        public bool exitClick;

        public GameWon(ContentManager content)
        {
            gameWonOverlay = content.Load<Texture2D>("GameWon");

            menuButton = new Button(new Rectangle(672, 500, 256, 128), content.Load<Texture2D>("ButtonMenu"), content.Load<Texture2D>("ButtonMenuOvr"));
            exitButton = new Button(new Rectangle(672, 628, 256, 128), content.Load<Texture2D>("ButtonExit"), content.Load<Texture2D>("ButtonExitOvr"));
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gameWonOverlay, new Rectangle(0, 0, 1600, 900), Color.White);

            menuButton.Draw(sb, currentState);
            exitButton.Draw(sb, currentState);
        }
        public void Update(MouseState current, MouseState previous)
        {
            currentState = current;
            previousState = previous;

            menuButton.Update(currentState, previousState);
            if(menuButton.IsClick(current, previous))
                menuClick = true;
            else
                menuClick = false;

            exitButton.Update(currentState, previousState);
            if (exitButton.IsClick(current, previous))
                exitClick = true;
            else
                exitClick = false;
        }
    }
}
