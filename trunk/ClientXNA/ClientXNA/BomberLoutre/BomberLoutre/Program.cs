using System;

namespace BomberLoutre
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BomberLoutreGame game = new BomberLoutreGame())
            {
                game.Run();
            }
        }
    }
#endif
}

