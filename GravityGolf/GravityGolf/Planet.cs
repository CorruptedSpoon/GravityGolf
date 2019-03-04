using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityGolf
{
    class Planet:GameObject
    {
        /// <summary>
        /// Creates a new Planet centered at center with radius radius and mass mass.
        /// Its texture will be tex and it will have a color mask of color
        /// </summary>
        /// <param name="center">The center point of this Planet</param>
        /// <param name="radius">This Planet's radius</param>
        /// <param name="mass">This Planet's mass</param>
        /// <param name="tex">This Planett's Texture2D</param>
        /// <param name="color">This Planet's color mask</param>
        public Planet(Vector2 center, int radius, float mass, Texture2D tex = null, Color? color = null)
            : base(center, radius, mass, tex, color) { }

        /// <summary>
        /// Returns the gravitational force caused by this Planet at the point represented by the position vector p
        /// </summary>
        /// <param name="p">The point at which to calculate the gravitaional force</param>
        /// <returns>the gravitaional force caused by this Planet at the point represented by the position vector p</returns>
        public Vector2 ForceAt(Vector2 p)
        {
            return -(mass/(p-center).LengthSquared())*UnitNormalAt(p);
        }

        /// <summary>
        /// Returns the normalized vector which is normal to this Planet's surface and passd through p
        /// </summary>
        /// <param name="p">The position vector through which the returned vector passes</param>
        /// <returns>A vector of length 1 such that it would pass through p if placed at center and extended indefinately</returns>
        public Vector2 UnitNormalAt(Vector2 p)
        {
            Vector2 unitNorm = p - center;
            unitNorm.Normalize();
            return unitNorm;
        }


        /// <summary>
        /// Returns whether of not the point represented by the position vector p is inside of this Planet
        /// </summary>
        /// <param name="p">The position Vector being checked</param>
        /// <returns>whether of not the point represented by the position vector p is inside of this Planet</returns>
        public bool IsInside(Vector2 p)
        {
            //this should be faster than checking if the length is less than radius becuause an extra multiplication should be faster than a squre root
            return (p - center).LengthSquared() < radius * radius; 
        }
    }

    //used for loading a specific planet type (radius, mass, texture)
    public enum PlanetType { big, medium, small }

    //used for level writing and reading
    public struct PlanetStruct
    {
        public int x;
        public int y;
        public PlanetType planetType;

        public PlanetStruct(int x, int y, PlanetType planetType)
        {
            this.x = x;
            this.y = y;
            this.planetType = planetType;
        }
    }
}
