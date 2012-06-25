using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BomberLoutre
{
    static class Config
    {
        public static Point MapSize = new Point(17, 17);
        public static Point MinimumMapSize = new Point(9, 9);
        public static Point[] MaximumMapSize = new Point[]
        {
            new Point(17, 17),
            new Point(23, 23),
            new Point(33, 29),
            new Point(53, 33)
        };

        public static bool FullScreen = false;

        public static Point[] PlayersPositions = new Point[]
        {
            new Point(1, 1),
            new Point(MapSize.X - 2, MapSize.Y - 2),
            new Point(1, MapSize.Y - 2),
            new Point(MapSize.X - 2, 1),
            new Point((int)Math.Ceiling((double)(MapSize.X - 2)/(double)2), (int)Math.Ceiling((double)(MapSize.Y - 2)/(double)2))
        };

        public static Keys[] DefaultKeys = new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space };
        
        public static Color[] PlayersColor = new Color[] { Color.White, Color.White, Color.White, Color.White };
        
        public static bool DisplayName = true;

        public static bool Invincible = false;
        public static TimeSpan PlayerInvincibleTimer = TimeSpan.FromSeconds(3);
        public static float InvincibleBlinkFrequency = 0.5f;

        public static float Volume = 0.0f;
        public static bool SoundState = true;
        public static bool MusicState = true;

        public static int[,] Resolutions = new int[,] { { 800, 600 }, { 1024, 768 }, { 1280, 1024 }, { 1366, 768 }, { 1920, 1080 } };
        public static int IndexResolution = 0;

        public static bool ActiveSuddenDeath = false;


        /* Option menu */
        public const string ControlOptionString = "Contrôles";
        public const string ResolutionOptionString = "Résolution";
        public const string FullScreenOptionString = "Plein écran";
        public const string MusicOptionString = "Musique";
        public const string SoundOptionString = "Sons";
        public const string BackOptionString = "Retour";
    }
}