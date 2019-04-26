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

        private bool menuClick = false;

        public bool MenuClick { get { return menuClick; } }

        public List<bool> levelButtonsClick = new List<bool>();

        public LevelMenu(ContentManager content)
        {
            //Row 1
            levelButtons.Add(new Button(new Rectangle(80, 450, 256, 128), content.Load<Texture2D>("level1preview"), content.Load<Texture2D>("ButtonPlayOvr")));
            levelButtons.Add(new Button(new Rectangle(376, 450, 256, 128), content.Load<Texture2D>("level2preview"), content.Load<Texture2D>("ButtonPlayOvr")));
            levelButtons.Add(new Button(new Rectangle(672, 450, 256, 128), content.Load<Texture2D>("level3preview"), content.Load<Texture2D>("ButtonPlayOvr")));
            levelButtons.Add(new Button(new Rectangle(968, 450, 256, 128), content.Load<Texture2D>("level4preview"), content.Load<Texture2D>("ButtonPlayOvr")));
            levelButtons.Add(new Button(new Rectangle(1264, 450, 256, 128), content.Load<Texture2D>("level5preview"), content.Load<Texture2D>("ButtonPlayOvr")));

            //Row 2
            levelButtons.Add(new Button(new Rectangle(228, 618, 256, 128), content.Load<Texture2D>("level6preview"), content.Load<Texture2D>("ButtonPlayOvr")));
            levelButtons.Add(new Button(new Rectangle(524, 618, 256, 128), content.Load<Texture2D>("level7preview"), content.Load<Texture2D>("ButtonPlayOvr")));
            levelButtons.Add(new Button(new Rectangle(820, 618, 256, 128), content.Load<Texture2D>("level8preview"), content.Load<Texture2D>("ButtonPlayOvr")));
            levelButtons.Add(new Button(new Rectangle(1116, 618, 256, 128), content.Load<Texture2D>("level9preview"), content.Load<Texture2D>("ButtonPlayOvr")));

            foreach (Button button in levelButtons) {
                levelButtonsClick.Add(false);
            }

            menuButton = new Button(new Rectangle(20, 20, 256, 128), content.Load<Texture2D>("ButtonMenu"), content.Load<Texture2D>("ButtonMenuOvr"));

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
            {
                Console.WriteLine("Menu");
                menuClick = true;
            }
            else
                menuClick = false;
        }
    }
}
