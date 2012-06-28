using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BomberLoutre.Screens;
using BomberLoutre.Components;
using System.IO;

namespace BomberLoutre.World
{
    public class Map : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D GroundTexture;
        BomberLoutre GameRef;
        GameScreen GameScreen;
        string mapFileName;
        static byte[,] matrix;
        static bool[,] walls; 

        public Map(BomberLoutre gameRef, string fileName, GameScreen gameScreen) : base(gameRef)
        {
            GameRef = gameRef;
            mapFileName = fileName;
            GameScreen = gameScreen;
            matrix = new byte[13, 11];
            walls = new bool[13, 11];
        }

        public override void Initialize()
        {            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            using (StreamReader fileReader = new StreamReader(Path.Combine("Maps", mapFileName)))
            {
                string currentLine = string.Empty;
                string[] splits;

                for (int i = 0; i < 11; ++i) // On va lire les 11 lignes du fichier map (= 11 lignes de la map)
                {
                    currentLine = fileReader.ReadLine();
                    splits = currentLine.Split(';');

                    for (int j = 0; j < 13; ++j) // On va attribuer chacune des 13 colonnes de la ligne courante
                    {
                        matrix[j, i] = byte.Parse(splits[j]);
                        if (matrix[j, i] != 0) walls[j, i] = true;
                    }
                }

                currentLine = fileReader.ReadLine();
                splits = currentLine.Split('#', '_', '\\', '.');
                GroundTexture = Game.Content.Load<Texture2D>(string.Format("Graphics/Textures/{0}/{1}/{2}", splits[1], splits[2], splits[3]));
            }

            for (int i = 0; i < 13; ++i)
            {
                for (int j = 0; j < 11; ++j)
                {
                    if (matrix[i, j] == 1)
                    {
                        GameScreen.AddBox(new Box(GameRef, new Vector2(i+1, j+1)));
                    }

                    if (matrix[i, j] == 2)
                    {
                        GameScreen.AddRock(new Rock(GameRef, new Vector2(i + 1, j + 1)));
                    }
                }
            }

/*          A utiliser quand on aura des textures (plusieurs) rock/box
            currentLine = fileReader.ReadLine();
            splits = currentLine.Split('#', '_', '\\', '.');
            GroundTexture = Game.Content.Load<Texture2D>("Graphics/Textures/" + splits[1] + "/" + splits[2] + "/" + splits[3]);

            currentLine = fileReader.ReadLine();
            splits = currentLine.Split('#', '_', '\\', '.');
            GroundTexture = Game.Content.Load<Texture2D>("Graphics/Textures/" + splits[1] + "/" + splits[2] + "/" + splits[3]);
 */
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            GameRef.spriteBatch.Draw(GroundTexture, new Vector2(Config.MapLayer.X, Config.MapLayer.Y), Config.MapLayer, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }

        public static Vector2 PointToVector(int x, int y)
        {
            return new Vector2((float) Math.Floor((x - Config.MapLayer.X) / (float)Config.TileWidth) + 1, (float)Math.Floor((y - Config.MapLayer.Y) / (float)Config.TileHeight) + 1); 
        }

        public static Vector2 CellToVector(int x, int y)
        {
            return new Vector2(((x-1) * Config.TileWidth) + Config.MapLayer.X, ((y-1) * Config.TileHeight) + Config.MapLayer.Y);
        }

        public static bool IsWall(int x, int y)
        {
            if (x < 1 || x > 13 || y < 1 || y > 11) return false; // On dit qu'il n'y a pas collision mais le joueur sera bloqué par les limites de la zone-map
            return walls[x-1, y-1];
        }

        public static void SetWall(int x, int y)
        {
            walls[x-1, y-1] = true;
        }

        public static void RemoveWall(int x, int y)
        {
            walls[x-1, y-1] = false;
        }
    }
}
