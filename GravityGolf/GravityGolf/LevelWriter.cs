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
        public static void WriteLevel(string level, int ballX, int ballY, int holeX, int holeY, List<PlanetStruct> planets)
        {
            BinaryWriter output = null;
            try
            {
                Directory.CreateDirectory("levels");
                Stream outStream = File.OpenWrite("levels\\" + level + ".level");
                output = new BinaryWriter(outStream);

                output.Write(ballX);
                output.Write(ballY);
                output.Write(holeX);
                output.Write(holeY);

                output.Write(planets.Count);

                for (int x = 0; x < planets.Count; x++)
                {
                    output.Write(planets[x].x);
                    output.Write(planets[x].y);

                    output.Write((int)planets[x].planetType);
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
