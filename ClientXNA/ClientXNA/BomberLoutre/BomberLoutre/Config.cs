﻿using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BomberLoutre
{
    public static class Config
    {
        public static int PlayerNumber = 1;

        public static int OtterWidth = 64;
        public static int OtterHeight = 72;
        public static int HitBoxWidth = 45;
        public static int HitBoxHeight = 45;

        public static int ExplosionDelay = 2; // s
        public static int BurningDelay = 400; // ms

        public static Dictionary<string, int> BonusDrop = new Dictionary<string, int>(); // rempli dans BomberLoutreGame()
        public const string BonusPower = "POWER";
        public const string BonusPowerMax = "POWERMAX";
        public const string BonusBomb = "BOMB";
        public const string BonusKick = "KICK";
        public const string BonusSpeed = "SPEED";

        public static Point MapSize = new Point(13, 11);
        public static int TileWidth = 60;
        public static int TileHeight = 60;
        public static Rectangle MapLayer = new Rectangle((Properties.App.Default.ScreenWidth - MapSize.X * TileWidth) / 2, (Properties.App.Default.ScreenHeight - MapSize.Y * TileHeight) / 2, MapSize.X * TileWidth, MapSize.Y * TileHeight);

        public static Point[] PlayersPositions = new Point[]
        {
            new Point(1, 1),
            new Point(MapSize.X - 2, MapSize.Y - 2),
            new Point(1, MapSize.Y - 2),
            new Point(MapSize.X - 2, 1),
        };

        public enum LookDirection { Up, Down, Left, Right };
        
        public static bool DisplayName = true;

        public static bool Invincible = false;
        public static TimeSpan PlayerInvincibleTimer = TimeSpan.FromSeconds(3);
        public static float InvincibleBlinkFrequency = 0.5f;

        public static float Volume = 0.0f;

        public static int[,] Resolutions = new int[,] { {900, 700}, { 1024, 768 }, { 1280, 1024 }, { 1366, 768 }, { 1920, 1080 } };
        public static int IndexResolution = 0;

        public static int InitialBombPower = 1;
        public static int InitialBombNumber = 1;
        public static float InitialWalkSpeed = 0.20f;
        public static int MaxBombPower = 6;
        public static float SpeedUpIncrement = 0.025f;
        public static float MaxWalkSpeed = 0.30f;

        /* Option menu */
        public const string ControlOptionString = "Contrôles";
        public const string ResolutionOptionString = "Résolution";
        public const string FullScreenOptionString = "Plein écran";
        public const string MusicOptionString = "Musique";
        public const string SoundOptionString = "Sons";
        public const string BackOptionString = "Retour";

        public static int[,] PlayerPosition = new int[,] { { 0 + MapLayer.X, 0 + MapLayer.Y }, { 0 + MapLayer.X, MapSize.Y * TileHeight - OtterHeight + MapLayer.Y }, { MapSize.X * TileWidth - OtterWidth + MapLayer.X, 0 + MapLayer.Y }, { MapSize.X * TileWidth - OtterWidth + MapLayer.X, MapSize.Y * TileHeight - OtterHeight + MapLayer.Y } };


        public static void UpdateMapLayer()
        {
            MapLayer = new Rectangle((Properties.App.Default.ScreenWidth - MapSize.X * TileWidth) / 2, (Properties.App.Default.ScreenHeight - MapSize.Y * TileHeight) / 2, MapSize.X * TileWidth, MapSize.Y * TileHeight);
        }

        public static void UpdatePlayerPosition()
        {
            PlayerPosition = new int[,] { { 0 + MapLayer.X, 0 + MapLayer.Y }, { 0 + MapLayer.X, MapSize.Y * TileHeight - OtterHeight + MapLayer.Y }, { MapSize.X * TileWidth - OtterWidth + MapLayer.X, 0 + MapLayer.Y }, { MapSize.X * TileWidth - OtterWidth + MapLayer.X, MapSize.Y * TileHeight - OtterHeight + MapLayer.Y } };
        }
    }
}