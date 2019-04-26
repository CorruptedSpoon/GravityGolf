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

        private bool menuClick;
        private bool playClick;
        private bool exitClick;

        public bool PlayClick { get { return playClick; } }
        public bool MenuClick { get { return menuClick; } }
        public bool ExitClick { get { return exitClick; } }

        public PauseMenu(ContentManager content) {
            pauseOverlay = content.Load<Texture2D>("PauseOverlay");

            playButton = new Button(new Rectangle(376, 450, 256, 128), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonPlayOvr"));
            menuButton = new Button(new Rectangle(672, 450, 256, 128), content.Load<Texture2D>("ButtonMenu"), content.Load<Texture2D>("ButtonMenuOvr"));
            exitButton = new Button( new Rectangle(968, 450, 256, 128), content.Load<Texture2D>("ButtonExit"), content.Load<Texture2D>("ButtonExitOvr"));
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(pauseOverlay, new Rectangle(0, 0, 1600, 900), Color.White);
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
