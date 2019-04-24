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
    class LevelComplete {
        MouseState currentState;
        MouseState previousState;

        Texture2D levelCompleteOverlay;

        Button playButton;
        Button menuButton;

        public bool menuClick;
        public bool playClick;

        public LevelComplete (ContentManager content) {
            levelCompleteOverlay = content.Load<Texture2D>("LevelComplete");

            playButton = new Button(new Rectangle(40, 40, 256, 128), content.Load<Texture2D>("ButtonNext"), content.Load<Texture2D>("ButtonNextOvr"));
            menuButton = new Button(new Rectangle(256 + 80, 40, 256, 128), content.Load<Texture2D>("ButtonMenu"), content.Load<Texture2D>("ButtonMenuOvr"));
        }
        public void Draw(SpriteBatch sb) {
            sb.Draw(levelCompleteOverlay, new Rectangle(0, 0, 1600, 900), Color.White);

            playButton.Draw(sb, currentState);
            menuButton.Draw(sb, currentState);
        }
        public void Update(MouseState current, MouseState previous) {
            currentState = current;
            previousState = previous;

            playButton.Update(currentState, previousState);
            if(playButton.IsClick(current, previous))
                playClick = true;
            else
                playClick = false;

            menuButton.Update(currentState, previousState);
            if (menuButton.IsClick(current, previous))
                menuClick = true;
            else
                menuClick = false;
        }
    }
}
