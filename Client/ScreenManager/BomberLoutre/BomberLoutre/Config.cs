﻿using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BomberLoutre
{
    static class Config
    {
        public static int PlayerNumber = 1;
        public static Point MapSize = new Point(13, 11);
        public static int TileWidth = 60;
        public static int TileHeight = 60;

        public static Point[] PlayersPositions = new Point[]
        {
            new Point(1, 1),
            new Point(MapSize.X - 2, MapSize.Y - 2),
            new Point(1, MapSize.Y - 2),
            new Point(MapSize.X - 2, 1),
        };
        
        public static bool DisplayName = true;

        public static bool Invincible = false;
        public static TimeSpan PlayerInvincibleTimer = TimeSpan.FromSeconds(3);
        public static float InvincibleBlinkFrequency = 0.5f;

        public static float Volume = 0.0f;

        public static int[,] Resolutions = new int[,] { { 1024, 768 }, { 1280, 1024 }, { 1366, 768 }, { 1920, 1080 } };
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