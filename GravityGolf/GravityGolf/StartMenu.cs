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
        public bool play = false;

        public StartMenu(ContentManager content) {
            logo = content.Load<Texture2D>("Logo");
            buttonPlay = content.Load<Texture2D>("ButtonPlay");
            playButton = new Button(new Rectangle((800 - 128), 500, 256, 128), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonPlay"));
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(logo, new Rectangle((800 - 256), 0, 512, 256), Color.White);
            //sb.Draw(buttonPlay, new Rectangle((800 - 128), 500, 256, 128), Color.White);
            playButton.Draw(sb, currentState);
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
        }
        public void Clear() {

        }
    }
}
