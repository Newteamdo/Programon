using System;

namespace Programon
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MainWindow game = new MainWindow())
            {
                game.Run();
            }
        }
    }
#endif
}

