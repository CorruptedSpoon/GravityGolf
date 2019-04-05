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
    class LevelMenu
    {
        MouseState currentState;

        Button menuButton;
        List<Button> levelButtons = new List<Button>();
        public bool menuClick = false;
        List<bool> levelButtonsClick = new List<bool>();

        public LevelMenu(ContentManager content)
        {
            levelButtons.Add(new Button(new Rectangle(80, 450, 256, 196), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonMenu")));
            levelButtons.Add(new Button(new Rectangle(376, 450, 256, 196), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonMenu")));
            levelButtons.Add(new Button(new Rectangle(672, 450, 256, 196), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonMenu")));
            levelButtons.Add(new Button(new Rectangle(968, 450, 256, 196), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonMenu")));
            levelButtons.Add(new Button(new Rectangle(1264, 450, 256, 196), content.Load<Texture2D>("ButtonPlay"), content.Load<Texture2D>("ButtonMenu")));
            foreach(Button button in levelButtons) {
                levelButtonsClick.Add(false);
            }

            menuButton = new Button(new Rectangle(0, 0, 256, 128), content.Load<Texture2D>("ButtonMenu"), content.Load<Texture2D>("ButtonMenu"));

        }

        public void Draw(SpriteBatch sb)
        {
            foreach(Button button in levelButtons)
            {
                button.Draw(sb, currentState);
            }
            menuButton.Draw(sb, currentState);
        }

        public void Update(MouseState cur, MouseState pre)
        {
            currentState = cur;

            for(int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].Update(cur, pre);
                if (levelButtons[i].IsClick(cur, pre))
                    levelButtonsClick[i] = true;
                else
                    levelButtonsClick[i] = false;
            }

            menuButton.Update(cur, pre);
            if (menuButton.IsClick(cur, pre))
                menuClick = true;
            else
                menuClick = false;
        }
    }
}
