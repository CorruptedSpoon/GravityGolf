using System;
using System.Windows.Forms;

namespace GravityGolf
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        public static bool tool;
        public static bool toolUpdate;
        public static bool toolDraw;
        public static string level;

        /// <summary>
        /// The main entry point for the application. adding "-tool" to command line arguments launches external tool
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length>0 && args[0] == "-tool")
            {
                Application.Run(new Form1());
                tool = true;
                toolUpdate = false;
                toolDraw = false;
                
            }
            using (var game = new Game1())
            game.Run();
            
        }
    }
#endif
}
