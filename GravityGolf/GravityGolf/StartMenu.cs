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

        public bool play = false;
        public bool level = false;
        public bool exit = false;


        public StartMenu(ContentManager content) {
            logo = content.Load<Texture2D>("Logo");
            buttonPlay = content.Load<Texture2D>("ButtonPlay");
            playButton = new Button(new Rectangle((800 - 128), 500, 256, 128), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonPlayOvr"));
            levelButton = new Button(new Rectangle((800 - 128), 500 + 128 + 40, 256, 128), content.Load<Texture2D>("ButtonLevel"), content.Load<Texture2D>("ButtonLevelOvr"));
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(logo, new Rectangle((800 - 256), 100, 512, 256), Color.White);
            //sb.Draw(buttonPlay, new Rectangle((800 - 128), 500, 256, 128), Color.White);
            playButton.Draw(sb, currentState);
            levelButton.Draw(sb, currentState);
        }
        public void Update(MouseState current, MouseState previous) {
            currentState = current;
            previousState = previous;
            playButton.Update(current, previous);
            if (playButton.IsClick(current, previous))
            {
                play = true;
            }
            else
            {
                play = false;
            }
            levelButton.Update(current, previous);
            if (levelButton.IsClick(current, previous))
            {
                level = true;
            }
            else
            {
                level = false;
            }
        }
        public void Clear() {

        }
    }
}
