using System;

namespace ScreenManager
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BomberLoutre game = new BomberLoutre())
            {
                game.Run();
            }
        }
    }
#endif
}

