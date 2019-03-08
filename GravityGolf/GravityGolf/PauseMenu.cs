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
        Texture2D buttonExit;
        Texture2D buttonMenu;
        Texture2D buttonPlay;
        Texture2D pauseOverlay;
        Game1 game;

        public PauseMenu(ContentManager content, Game1 game) {
            buttonExit = content.Load<Texture2D>("ButtonExit");
            buttonMenu = content.Load<Texture2D>("ButtonMenu");
            buttonPlay = content.Load<Texture2D>("ButtonPlay");
            pauseOverlay = content.Load<Texture2D>("PauseOverlay");
            this.game = game;
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(pauseOverlay, new Rectangle(0, 0, 1600, 900), Color.White);
            sb.Draw(buttonPlay, new Rectangle(0, 0, 256, 128), Color.White);
            sb.Draw(buttonMenu, new Rectangle(256, 0, 256, 128), Color.White);
            sb.Draw(buttonExit, new Rectangle(512, 0, 256, 128), Color.White);
        }

        public void Update() {
            if (Keyboard.GetState().IsKeyDown(Keys.Tab)) {
                game.state = GameState.Playing;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) {
                game.state = GameState.Menu;
            }
        }
    }
}
