using System;
using System.Collections.Generic;
using BomberLoutre.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BomberLoutre.Controls;
using BomberLoutre.Screens;

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
            
            Texture2D spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/otterSprite");
            Sprite = new OtterSprite(spriteTexture, currentFrame, position);
        }
        #endregion

        #region XNA Method Region

        public void Initialize()
        {
            bombDropped = false;

            isAlive = true;
            bombPower = 1;
            bombNumber = 1;
            bombAvailable = 1;
            canKick = false;
        }

        public void LoadContent()
        {

        }

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

        #region Method Region

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
            gameScreen.AddBomb(new Bomb(id, Sprite.spritePosition, GameRef));
        }
        #endregion
    }
}
