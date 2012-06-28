using BomberLoutre.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BomberLoutre.Controls;
using BomberLoutre.Screens;
using BomberLoutre.World;

namespace BomberLoutre.Components
{
    class Player
    {
        #region Field Region

        private BomberLoutre GameRef;
        private GameScreen gameScreen;
        private int id;

        public OtterSprite Sprite { get; protected set; }

        private bool bombDropped;

        // Characteristics
        private bool isAlive;       // Joueur vivant
        private int bombPower;      // Rayon d'explosion de la bombe
        private int bombNumber;     // Capacité totale de bombes déposables
        private int bombAvailable;  // Capacité courante de bombes déposables
        private bool canKick;       // Peut kicker les bombes

        #endregion

        #region Property Region

        public int Id
        {
            get { return id; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
        }

        public int BombPower
        {
            get { return bombPower; }
        }

        public int BombAvailable
        {
            get { return bombAvailable; }
        }

        public int BombNumber
        {
            get { return bombNumber; }
        }

        public bool CanKick
        {
            get { return canKick; }
        }

        #endregion

        #region Constructor Region
        public Player(int id, BomberLoutre game, Vector2 position, int currentFrame, GameScreen gameScreen)
        {
            this.id = id;
            GameRef = game;
            this.gameScreen = gameScreen;

            bombDropped = false;

            isAlive = true;
            bombPower = 3;
            bombNumber = 1;
            bombAvailable = 1;
            canKick = false;

            Texture2D spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/otterSprite");
            Sprite = new OtterSprite(spriteTexture, currentFrame, position);
        }
        #endregion

        #region XNA Method Region

        public void Update(GameTime gameTime)
        {
            CheckBombing();
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
            if (InputHandler.KeyDown(Properties.App.Default.KeySpace))
            {
                if (!bombDropped)
                {
                    DropBomb();
                    bombDropped = true;
                }
            }

            else bombDropped = false;
        }

        private void DropBomb()
        {
            Vector2 bombPosition = new Vector2(Sprite.SpritePosition.X - Config.OtterWidth / 2, Sprite.SpritePosition.Y - Config.OtterHeight / 3);

            Bomb newBomb = new Bomb(id, bombPosition, bombPower, GameRef, gameScreen);
            gameScreen.AddBomb(newBomb);

            Sprite.CellPosition = Map.PointToVector((int)newBomb.Sprite.SpritePosition.X, (int)newBomb.Sprite.SpritePosition.Y);
        }


        #endregion
    }
}
