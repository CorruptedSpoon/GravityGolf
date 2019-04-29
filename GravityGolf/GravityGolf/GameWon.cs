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
    class GameWon
    {
        MouseState currentState;
        MouseState previousState;

        Texture2D gameWonOverlay;
        Texture2D hiScoreBlank;
        private SpriteFont font;

        Button menuButton;
        Button exitButton;

        private bool menuClick;
        private bool exitClick;

        int[] scores;
        bool[] gotHi;

        public bool MenuClick { get { return menuClick; } }
        public bool ExitClick { get { return exitClick; } }

        int totalStrokes = 0;


        public GameWon(ContentManager content)
        {
            gameWonOverlay = content.Load<Texture2D>("EndOverlay");
            hiScoreBlank = content.Load<Texture2D>("HiScoreBlank");
            font = content.Load<SpriteFont>("font");

            scores = new int[9];

            menuButton = new Button(new Rectangle(672, 500, 256, 128), content.Load<Texture2D>("ButtonMenu"), content.Load<Texture2D>("ButtonMenuOvr"));
            exitButton = new Button(new Rectangle(672, 668, 256, 128), content.Load<Texture2D>("ButtonExit"), content.Load<Texture2D>("ButtonExitOvr"));
        }

        public void GetTotalStrokes(int[] strokes)
        {
            for (int i = 0; i < 9; i++)
            {
                if (strokes[i] != int.MaxValue)
                    totalStrokes += strokes[i];
                scores[i] = strokes[i];
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gameWonOverlay, new Rectangle(0, 0, 1600, 900), Color.White);

            for(int i = 0; i < 10; i++)
            {
                sb.Draw(hiScoreBlank, new Rectangle(150 + i * 128, 300, 128, 128), Color.White);
                if(i < 9)
                    sb.DrawString(font, scores[i].ToString(), new Vector2(200 + i * 128, 325), Color.Black);
                else
                    sb.DrawString(font, totalStrokes.ToString(), new Vector2(175 + i * 128, 325), Color.Black);
            }

            menuButton.Draw(sb, currentState);
            exitButton.Draw(sb, currentState);
        }
        public void Update(MouseState current, MouseState previous)
        {
            currentState = current;
            previousState = previous;

            menuButton.Update(currentState, previousState);
            if(menuButton.IsClick(current, previous))
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
