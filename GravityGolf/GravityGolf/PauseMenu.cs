using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GravityGolf {
    class PauseMenu {
        Texture2D pauseOverlay;

        Button playButton;
        Button menuButton;
        Button exitButton;

        MouseState currentState;
        MouseState previousState;

        public bool menuClick;
        public bool playClick;
        public bool exitClick;

        public PauseMenu(ContentManager content) {
            pauseOverlay = content.Load<Texture2D>("PauseOverlay");

            playButton = new Button(new Rectangle(0, 0, 256, 128), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonPlayOvr"));
            menuButton = new Button(new Rectangle(256, 0, 256, 128), content.Load<Texture2D>("ButtonMenu"), content.Load<Texture2D>("ButtonMenuOvr"));
            exitButton = new Button( new Rectangle(512, 0, 256, 128), content.Load<Texture2D>("ButtonExit"), content.Load<Texture2D>("ButtonExitOvr"));
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(pauseOverlay, new Rectangle(0, 0, 1600, 900), Color.White);
            //sb.Draw(buttonPlay, new Rectangle(0, 0, 256, 128), Color.White);
            //sb.Draw(buttonMenu, new Rectangle(256, 0, 256, 128), Color.White);
            //sb.Draw(buttonExit, new Rectangle(512, 0, 256, 128), Color.White);

            playButton.Draw(sb, currentState);
            menuButton.Draw(sb, currentState);
            exitButton.Draw(sb, currentState);
        }

        public void Update(MouseState current, MouseState previous) {
            currentState = current;
            previousState = previous;

            playButton.Update(currentState, previousState);
            if (playButton.IsClick(current, previous))
                playClick = true;
            else
                playClick = false;

            menuButton.Update(currentState, previousState);
            if (menuButton.IsClick(current, previous))
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
