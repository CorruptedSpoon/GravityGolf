using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityGolf
{
    // Both the Planet and Player will be GameObjects.
    // While this isn't strictly necesary given our 
    // structures, it prevents us from having to rewrite a lot of code

    /// <summary>
    /// A circular object with a mass
    /// </summary>
    abstract class GameObject
    {
        protected Vector2 center;
        protected int radius;
        protected float mass;
        protected Texture2D tex; //maybe get rid of these (this and color)?
        protected Color? color; 

        /// <summary>
        /// Gets the x-coordinate of this GameObject's center
        /// </summary>
        public float X
        {
            get
            {
                return center.X;
            }
            protected set
            {
                center.X = value;
            }
        }

        /// <summary>
        /// Get's the y-coordinate of this GameObject's center
        /// </summary>
        public float Y
        {
            get
            {
                return center.Y;
            }
            protected set
            {
                center.Y = value;
            }
        }


        /// <summary>
        /// Creates a new circlular GameObject centered at center with radius radius and mass mass.
        /// Its texture will be tex and it will have a color mask of color
        /// </summary>
        /// <param name="center">The center Vector2 of this GameObject</param>
        /// <param name="radius">This GameObject's radius</param>
        /// <param name="mass">This gameObject's mass</param>
        /// <param name="tex">This GameObject's Texture2D</param>
        /// <param name="color">This GameObject's color mask</param>
        public GameObject(Vector2 center, int radius, float mass, Texture2D tex = null, Color? color = null)
        {
            this.center = center;
            this.radius = radius;
            this.mass = mass;
            this.tex = tex;
            this.color = color==null?Color.White:color;
        }

        /// <summary>
        /// Draws this GameObject on the screen
        /// </summary>
        /// <param name="sb">the SpriteBAtch with which to draw this</param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, new Rectangle((int)(X+0.5)-radius, (int)(Y+0.5)-radius, 2*radius, 2*radius), (Color)color);
        }
    }
}
