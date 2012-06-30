using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BomberLoutre.Sprites
{
    class OtterSprite : Sprite
    {
        private float Timer;
        private float Interval;
        private int CurrentFrame;
        public float SpriteSpeed { get; set; }
        public Vector2 Direction { get; set; }
        private Boolean Facing;
        public Point CellOffset { get; set; }

        private Config.LookDirection oldLookDirection;
        public Config.LookDirection LookDirection { get; set; }

        // Coordonnées sur la map, et non relatives à l'écran
        public int X { get; set; }
        public int Y { get; set; }


        public OtterSprite(Texture2D texture, int currentFrame, Vector2 position) : base(texture)
        {
            this.CurrentFrame = currentFrame;
            SpritePosition = position;
            Facing = true; // Par défaut, la Sprite est de face. Si elle monte, elle sera de dos

            SpriteSpeed = 0.3f;
            Interval = 80f;

            SpriteWidth = Config.OtterWidth;
            SpriteHeight = Config.OtterHeight;

            CellOffset = new Point(0, 0);

            LookDirection = Config.LookDirection.Down;
        }

        public void Update(GameTime gameTime)
        {
            oldLookDirection = LookDirection;
            int line = 0, column = 0;

            /* TODO : Bitch please, clean this shit... */
            if (CurrentFrame < 7) line = 0;
            else if (CurrentFrame < 14) line = 1;
            else if (CurrentFrame < 21) line = 2;
            else if (CurrentFrame < 28) line = 3;
            else if (CurrentFrame < 35) line = 4;
            else if (CurrentFrame < 42) line = 5;

            /* TODO : Bitch please, clean this other shit... */

            if (CurrentFrame == 0 || CurrentFrame == 7 || CurrentFrame == 14 || CurrentFrame == 21 || CurrentFrame == 28 || CurrentFrame == 35)
                column = 0;
            if (CurrentFrame == 1 || CurrentFrame == 8 || CurrentFrame == 15 || CurrentFrame == 22 || CurrentFrame == 29 || CurrentFrame == 36)
                column = 1;
            if (CurrentFrame == 2 || CurrentFrame == 9 || CurrentFrame == 16 || CurrentFrame == 23 || CurrentFrame == 30 || CurrentFrame == 37)
                column = 2;
            if (CurrentFrame == 3 || CurrentFrame == 10 || CurrentFrame == 17 || CurrentFrame == 24 || CurrentFrame == 31 || CurrentFrame == 38)
                column = 3;
            if (CurrentFrame == 4 || CurrentFrame == 11 || CurrentFrame == 18 || CurrentFrame == 25 || CurrentFrame == 32 || CurrentFrame == 39)
                column = 4;
            if (CurrentFrame == 5 || CurrentFrame == 12 || CurrentFrame == 19 || CurrentFrame == 25 || CurrentFrame == 33 || CurrentFrame == 40)
                column = 5;
            if (CurrentFrame == 6 || CurrentFrame == 13 || CurrentFrame == 20 || CurrentFrame == 26 || CurrentFrame == 34 || CurrentFrame == 41)
                column = 6;

            // Encadre la frame
            sourceRect = new Rectangle(column * SpriteWidth, line * SpriteHeight, SpriteWidth, SpriteHeight);            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, SpritePosition, sourceRect, Color.White);
        }

        public void AnimateRight(GameTime gameTime)
        {
            if (LookDirection != oldLookDirection)
            {
                CurrentFrame = (Facing) ? 14 : 28;    
            }

            Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Timer > Interval)
            {
                CurrentFrame++;

                if (Facing)
                {
                    if (CurrentFrame > 20)
                    {
                        CurrentFrame = 14;
                    }
                }

                else
                {
                    if (CurrentFrame > 34)
                    {
                        CurrentFrame = 28;
                    }
                }

                Timer = 0f;
            }
        }

        public void AnimateUp(GameTime gameTime)
        {
            if (LookDirection != oldLookDirection)
            {
                CurrentFrame = 35;
            }

            Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Facing = false;

            if (Timer > Interval)
            {
                CurrentFrame++;

                if (CurrentFrame > 41)
                {
                    CurrentFrame = 35;
                }

                Timer = 0f;
            }
        }

        public void AnimateDown(GameTime gameTime)
        {
            if (LookDirection != oldLookDirection)
            {
                CurrentFrame = 0;
            }

            Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Facing = true;

            if (Timer > Interval)
            {
                CurrentFrame++;

                if (CurrentFrame > 6)
                {
                    CurrentFrame = 0;
                }

                Timer = 0f;
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            if (LookDirection != oldLookDirection)
            {
                CurrentFrame = (Facing) ? 7 : 21;
            }

            Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Timer > Interval)
            {
                CurrentFrame++;

                if (Facing)
                {
                    if (CurrentFrame > 13)
                    {
                        CurrentFrame = 7;
                    }
                }

                else
                {
                    if(CurrentFrame > 27)
                    {
                        CurrentFrame = 21;
                    }
                }

                Timer = 0f;
            }
        }

        private Rectangle SourceRectangle()
        {
            return sourceRect;
        }
    }
}
