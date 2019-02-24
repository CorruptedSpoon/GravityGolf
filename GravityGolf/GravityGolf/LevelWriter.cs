using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GravityGolf
{
    static class LevelWriter
    {
        /// <summary>
        /// used to create a .level file
        /// </summary>
        /// <param name="level">name of .level file</param>
        /// <param name="ballX">x position of ball</param>
        /// <param name="ballY">y position of ball</param>
        /// <param name="vectorAndPlanetType">a list of arrays that contain a vector2 for the planets location, and a PlanetType (determines size, mass, and texture)</param>
        public static void WriteLevel(string level, int ballX, int ballY, List<object[]> vectorAndPlanetType)
        {
            BinaryWriter output = null;
            try
            {
                Stream outStream = File.OpenWrite(level + ".level");
                output = new BinaryWriter(outStream);

                output.Write(ballX);
                output.Write(ballY);

                output.Write(vectorAndPlanetType.Count);

                for (int x = 0; x < vectorAndPlanetType.Count; x++)
                {
                    output.Write(((Vector2)vectorAndPlanetType[x][0]).X);
                    output.Write(((Vector2)vectorAndPlanetType[x][0]).Y);

                    output.Write((int)vectorAndPlanetType[x][1]);
                }
            }
            finally
            {
                if (output != null)
                    output.Close();
            }
        }
    }
}
