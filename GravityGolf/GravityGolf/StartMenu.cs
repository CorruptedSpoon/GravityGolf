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
    class StartMenu {
        Texture2D logo;
        Texture2D buttonPlay;

        MouseState currentState;
        MouseState previousState;

        Button playButton;
        Button levelButton;
        Button exitButton;

        private bool play = false;
        private bool level = false;
        private bool exit = false;

        public bool Play { get { return play; } }
        public bool Level { get { return level; } }
        public bool Exit { get { return exit; } }


        public StartMenu(ContentManager content) {
            logo = content.Load<Texture2D>("Logo");
            buttonPlay = content.Load<Texture2D>("ButtonPlay");
            playButton = new Button(new Rectangle(672, 420, 256, 128), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonPlayOvr"));
            levelButton = new Button(new Rectangle(672, 578, 256, 128), content.Load<Texture2D>("ButtonLevel"), content.Load<Texture2D>("ButtonLevelOvr"));
            exitButton = new Button(new Rectangle(672, 736, 256, 128), content.Load<Texture2D>("ButtonExit"), content.Load<Texture2D>("ButtonExitOvr"));

        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(logo, new Rectangle(544, 100, 512, 256), Color.White);
            playButton.Draw(sb, currentState);
            levelButton.Draw(sb, currentState);
            exitButton.Draw(sb, currentState);
        }
        public void Update(MouseState current, MouseState previous) {
            currentState = current;
            previousState = previous;
            playButton.Update(current, previous);
            if (playButton.IsClick(current, previous))
                play = true;
            else
                play = false;

            levelButton.Update(current, previous);
            if (levelButton.IsClick(current, previous))
                level = true;
            else
                level = false;
            exitButton.Update(current, previous);
            if (exitButton.IsClick(current, previous))
                exit = true;
            else
                exit = false;
        }
        public void Clear() {

        }
    }
}
