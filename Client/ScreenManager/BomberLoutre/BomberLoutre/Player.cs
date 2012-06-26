using System;
using System.Collections.Generic;
using BomberLoutre.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberLoutre
{
    class Player
    {
        #region Field Region

        private BomberLoutre gameRef;
        private int id;

        public OtterSprite Sprite { get; protected set; }

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

        #endregion

        #region Constructor Region
        public Player(int id, BomberLoutre game, Vector2 position, int currentFrame)
        {
            this.id = id;
            this.gameRef = game;

            Texture2D spriteTexture = gameRef.Content.Load<Texture2D>("Graphics/Characters/otterSprite");

            Sprite = new OtterSprite(spriteTexture, currentFrame);
            
            isAlive = true;
            bombPower = 1;
            bombNumber = 1;
            bombAvailable = 1;
        }
        #endregion

        #region XNA Method Region
        public void Update(GameTime gameTime)
        {                
        }

        public void Draw(GameTime gameTime)
        {
            
        }
        #endregion

        #region Method Region

        #endregion
    }
}
