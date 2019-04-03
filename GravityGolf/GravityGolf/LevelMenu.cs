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

        List<Button> levelButtons = new List<Button>();
        List<bool> levelButtonsClick = new List<bool>();

        public LevelMenu(ContentManager content)
        {
            levelButtons.Add(new Button(new Rectangle(80, 450, 256, 196), content.Load<Texture2D>("level1icon"), content.Load<Texture2D>("level1icon")));
            levelButtons.Add(new Button(new Rectangle(376, 450, 256, 196), content.Load<Texture2D>("level1icon"), content.Load<Texture2D>("level1icon")));
            levelButtons.Add(new Button(new Rectangle(672, 450, 256, 196), content.Load<Texture2D>("level1icon"), content.Load<Texture2D>("level1icon")));
            levelButtons.Add(new Button(new Rectangle(968, 450, 256, 196), content.Load<Texture2D>("level1icon"), content.Load<Texture2D>("level1icon")));
            levelButtons.Add(new Button(new Rectangle(1264, 450, 256, 196), content.Load<Texture2D>("level1icon"), content.Load<Texture2D>("level1icon")));
            levelButtonsClick.Add(false);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach(Button button in levelButtons)
            {
                button.Draw(sb, currentState);
            }
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
        }
    }
}
