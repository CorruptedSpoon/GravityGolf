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
        Game1 game;

        public StartMenu(ContentManager content, Game1 game) {
            logo = content.Load<Texture2D>("Logo");
            buttonPlay = content.Load<Texture2D>("ButtonPlay");
            this.game = game;
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(logo, new Rectangle((800 - 256), 0, 512, 256), Color.White);
            sb.Draw(buttonPlay, new Rectangle((800 - 128), 500, 256, 128), Color.White);
        }
        public void Update() {

        }
        public void Clear() {

        }
    }
}
