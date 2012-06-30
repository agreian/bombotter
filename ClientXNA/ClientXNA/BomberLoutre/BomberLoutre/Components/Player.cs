using BomberLoutre.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BomberLoutre.Controls;
using BomberLoutre.Screens;
using BomberLoutre.World;
using Microsoft.Xna.Framework.Input;

namespace BomberLoutre.Components
{
    public class Player
    {
        #region Field Region

        private BomberLoutreGame GameRef;
        private GameScreen gameScreen;
        public int Id { get; set; }

        public OtterSprite Sprite { get; protected set; }

        KeyboardState currentKBState;
        KeyboardState previousKBState;

        private bool bombDropped;

        // Characteristics
        public bool IsAlive { get; set; }       // Joueur vivant
        public int BombPower { get; set; }      // Rayon d'explosion de la bombe
        public int BombNumber { get; set; }     // Capacité totale de bombes déposables
        public int BombAvailable { get; set; }  // Capacité courante de bombes déposables
        public bool CanKick { get; set; }       // Peut kicker les bombes

        #endregion

        #region Constructor Region
        public Player(int id, BomberLoutreGame game, Vector2 position, int currentFrame, GameScreen gameScreen)
        {
            this.id = id;
            GameRef = game;
            this.gameScreen = gameScreen;

            bombDropped = false;

            IsAlive = true;
            BombPower = 1;
            BombNumber = 1;
            BombAvailable = BombNumber;
            CanKick = false;

            Texture2D spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/otterSprite");
            Sprite = new OtterSprite(spriteTexture, currentFrame, position);
        }
        #endregion

        #region XNA Method Region

        public void Update(GameTime gameTime)
        {
            CheckBombing();

            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();

            if (previousKBState != currentKBState)
                Sprite.CellOffset = new Point(Config.OtterWidth / 4, Config.OtterHeight / 2);

            if (InputHandler.Maintained("Right", PlayerIndex.One))
            {
                Sprite.LookDirection = Config.LookDirection.Right;
                Sprite.AnimateRight(gameTime);

                if (Sprite.SpritePosition.X < (Config.MapLayer.Width + Config.MapLayer.X - Config.OtterWidth))
                {
                    if (!Map.IsObstacle((int)Sprite.CellPosition.X + 1, (int)Sprite.CellPosition.Y) && !Map.IsBomb((int)Sprite.CellPosition.X + 1, (int)Sprite.CellPosition.Y))
                    {
                        Sprite.Direction = Vector2.Normalize(new Vector2(1, 0));
                        Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * (Sprite.SpriteSpeed);

                        // Contournement
                        if ((Sprite.SpritePosition.Y - Config.MapLayer.Y) > ((Sprite.CellPosition.Y * Config.TileHeight) - Config.TileHeight - Config.TileHeight / 5))
                        {
                            Sprite.SpritePosition = new Vector2(Sprite.SpritePosition.X, Sprite.SpritePosition.Y - 3);
                        }
                    }

                    else // La case vers laquelle on se dirige est bloquante mais on veut pouvoir pousser la taupe dans le guichet ;D
                    {
                        // Tant que le bord droit de la loutre n'a pas dépassé le bord gauche de la cellule bloquante...
                        if (!(Sprite.SpritePosition.X + Config.OtterWidth > (Sprite.CellPosition.X * Config.TileWidth + Config.MapLayer.X)))
                        {
                            Sprite.Direction = Vector2.Normalize(new Vector2(1, 0));
                            Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Sprite.SpriteSpeed;
                        }
                    }
                }
            }

            else if (InputHandler.Maintained("Left", PlayerIndex.One))
            {
                Sprite.LookDirection = Config.LookDirection.Left;
                Sprite.AnimateLeft(gameTime);

                if (Sprite.SpritePosition.X > Config.MapLayer.X)
                {
                    if (!Map.IsObstacle((int)Sprite.CellPosition.X - 1, (int)Sprite.CellPosition.Y) && !Map.IsBomb((int)Sprite.CellPosition.X - 1, (int)Sprite.CellPosition.Y))
                    {
                        Sprite.Direction = Vector2.Normalize(new Vector2(-1, 0));
                        Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Sprite.SpriteSpeed;


                        // Contournement
                        if ((Sprite.SpritePosition.Y - Config.MapLayer.Y) > ((Sprite.CellPosition.Y * Config.TileHeight) - Config.TileHeight - Config.TileHeight / 5))
                        {
                            Sprite.SpritePosition = new Vector2(Sprite.SpritePosition.X, Sprite.SpritePosition.Y - 3);
                        }
                    }

                    else // La case vers laquelle on se dirige est bloquante mais on veut pouvoir pousser la taupe dans le guichet ;D
                    {
                        // Tant que le bord gauche de la loutre n'a pas dépassé le bord droit de la caisse bloquante
                        if (!(Sprite.SpritePosition.X + Config.OtterWidth < (Sprite.CellPosition.X * Config.TileWidth + Config.MapLayer.X)))
                        {
                            Sprite.Direction = Vector2.Normalize(new Vector2(-1, 0));
                            Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Sprite.SpriteSpeed;
                        }
                    }
                }

            }

            else if (InputHandler.Maintained("Down", PlayerIndex.One))
            {
                Sprite.LookDirection = Config.LookDirection.Down;
                Sprite.AnimateDown(gameTime);

                if (Sprite.SpritePosition.Y < (Config.MapLayer.Height + Config.MapLayer.Y - Config.OtterHeight))
                {
                    if (!Map.IsObstacle((int)Sprite.CellPosition.X, (int)Sprite.CellPosition.Y + 1) && !Map.IsBomb((int)Sprite.CellPosition.X, (int)Sprite.CellPosition.Y + 1))
                    {
                        Sprite.Direction = Vector2.Normalize(new Vector2(0, 1));
                        Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Sprite.SpriteSpeed;


                        // Contournement
                        if ((Sprite.SpritePosition.X - Config.MapLayer.X + Config.OtterWidth / 2) > ((Sprite.CellPosition.X * Config.TileWidth) - Config.TileWidth / 4))
                        {
                            Sprite.SpritePosition = new Vector2(Sprite.SpritePosition.X - 3, Sprite.SpritePosition.Y);
                        }
                    }

                    else
                    {
                        // Tant que les pieds de la loutre n'ont pas atteint le bord haut de la case bloquante...
                        if (!(Sprite.SpritePosition.Y + Config.OtterHeight > (Sprite.CellPosition.Y * Config.TileHeight + Config.MapLayer.Y)))
                        {
                            Sprite.Direction = Vector2.Normalize(new Vector2(0, 1));
                            Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Sprite.SpriteSpeed;
                        }
                    }
                }
            }

            else if (InputHandler.Maintained("Up", PlayerIndex.One))
            {
                Sprite.LookDirection = Config.LookDirection.Up;
                Sprite.AnimateUp(gameTime);

                if (Sprite.SpritePosition.Y > Config.MapLayer.Y)
                {
                    if (!Map.IsObstacle((int)Sprite.CellPosition.X, (int)Sprite.CellPosition.Y - 1) && !Map.IsBomb((int)Sprite.CellPosition.X, (int)Sprite.CellPosition.Y - 1))
                    {
                        Sprite.Direction = Vector2.Normalize(new Vector2(0, -1));
                        Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Sprite.SpriteSpeed;

                        // Contournement
                        if ((Sprite.SpritePosition.X - Config.MapLayer.X + Config.OtterWidth / 2) > ((Sprite.CellPosition.X * Config.TileWidth) - Config.TileWidth / 4))
                        {
                            Sprite.SpritePosition = new Vector2(Sprite.SpritePosition.X - 3, Sprite.SpritePosition.Y);
                        }
                    }

                    else // La case vers laquelle on se dirige est bloquante mais on veut pouvoir pousser la taupe dans le guichet ;D
                    {
                        // Tant que les pieds de la loutre n'ont pas touché le bord inférieur de la case précédant la bloquante...
                        if (!(Sprite.SpritePosition.Y + Config.OtterHeight < Sprite.CellPosition.Y * Config.TileHeight + Config.MapLayer.Y))
                        {
                            Sprite.Direction = Vector2.Normalize(new Vector2(0, -1));
                            Sprite.SpritePosition += Sprite.Direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Sprite.SpriteSpeed;
                        }
                    }
                }
            }

            else
                Sprite.CellOffset = new Point(Config.OtterWidth / 4, Config.OtterHeight / 2);

            Sprite.CellPosition = Map.PointToVector((int)(Sprite.SpritePosition.X + Sprite.CellOffset.X), (int)(Sprite.SpritePosition.Y + Sprite.CellOffset.Y));
            
            if (Map.IsBonus((int) Sprite.CellPosition.X, (int) Sprite.CellPosition.Y))
            {
                gameScreen.ApplyBonus(this, (int) Sprite.CellPosition.X, (int) Sprite.CellPosition.Y);
            }


            Sprite.X = (int)Sprite.SpritePosition.X - Config.MapLayer.X;
            Sprite.Y = (int)Sprite.SpritePosition.Y - Config.MapLayer.Y;

            Sprite.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime, GameRef.spriteBatch);
        }
        #endregion

        #region Player Method Region

        private void CheckBombing()
        {
            if (InputHandler.Maintained("Space", PlayerIndex.One))
            {
                if (!bombDropped && bombAvailable > 0)
                {
                    DropBomb();
                    bombAvailable--;
                    bombDropped = true;
                }
            }

            else bombDropped = false;
        }

        private void DropBomb()
        {
            Vector2 bombCell;
            Vector2 bombPosition = new Vector2(Sprite.SpritePosition.X - Config.OtterWidth / 2, Sprite.SpritePosition.Y - Config.OtterHeight / 3);

            // convertie en Cellule
            bombPosition = Map.PointToVector((int)bombPosition.X + 1, (int)bombPosition.Y + 1); 
            // reconverti en position (point (top,left) de la cellule ciblée) pour le positionnement
            bombPosition = new Vector2((bombPosition.X * Config.TileWidth) + Config.MapLayer.X, (bombPosition.Y * Config.TileHeight) + Config.MapLayer.Y);
            // Reconverti en Cellule pour déterminer s'il y a déjà une bombe à cette endroit
            bombCell = Map.PointToVector((int)bombPosition.X, (int)bombPosition.Y);

            if (!Map.IsBomb((int)bombCell.X, (int)bombCell.Y))
            {
                Bomb newBomb = new Bomb(id, bombPosition, bombPower, GameRef, gameScreen);
                gameScreen.AddBomb(newBomb);
                // Trick pour tenter de ne pas laisser le joueur bloqué sur sa bombe s'il l'a posée devant lui et que sa Cell était toujours celle de sa case précédente
                Sprite.CellPosition = Map.PointToVector((int)newBomb.Sprite.SpritePosition.X, (int)newBomb.Sprite.SpritePosition.Y);
            }
        }
        
        #endregion
    }
}
