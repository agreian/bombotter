using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using BomberLoutre.World;

namespace BomberLoutre.Sprites
{
    class OtterSprite : Sprite
    {
        public float timer { get; set; }
        public float interval { get; set; }
        public int currentFrame { get; set; }
        public float SpriteSpeed { get; set; }
        public Vector2 origin { get; set; }
        public Vector2 direction { get; set; }
        public Boolean facing { get; set; }

        KeyboardState currentKBState;
        KeyboardState previousKBState;

        public OtterSprite(Texture2D texture, int currentFrame, Vector2 position) : base(texture)
        {
            this.currentFrame = currentFrame;
            SpritePosition = position;
            facing = true; // Par défaut, la Sprite est de face. Si elle monte, elle sera de dos
            SpriteSpeed = 0.30f;
            interval = 80f;
            SpriteWidth = Config.OtterWidth;
            SpriteHeight = Config.OtterHeight;
        }

        public void Update(GameTime gameTime)
        {
            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();
            int line = 0, column = 0;

            /* TODO : Bitch please, clean this shit... */
            if (currentFrame < 7) line = 0;
            else if (currentFrame < 14) line = 1;
            else if (currentFrame < 21) line = 2;
            else if (currentFrame < 28) line = 3;
            else if (currentFrame < 35) line = 4;
            else if (currentFrame < 42) line = 5;

            /* TODO : Bitch please, clean this other shit... */

            if (currentFrame == 0 || currentFrame == 7 || currentFrame == 14 || currentFrame == 21 || currentFrame == 28 || currentFrame == 35)
                column = 0;
            if (currentFrame == 1 || currentFrame == 8 || currentFrame == 15 || currentFrame == 22 || currentFrame == 29 || currentFrame == 36)
                column = 1;
            if (currentFrame == 2 || currentFrame == 9 || currentFrame == 16 || currentFrame == 23 || currentFrame == 30 || currentFrame == 37)
                column = 2;
            if (currentFrame == 3 || currentFrame == 10 || currentFrame == 17 || currentFrame == 24 || currentFrame == 31 || currentFrame == 38)
                column = 3;
            if (currentFrame == 4 || currentFrame == 11 || currentFrame == 18 || currentFrame == 25 || currentFrame == 32 || currentFrame == 39)
                column = 4;
            if (currentFrame == 5 || currentFrame == 12 || currentFrame == 19 || currentFrame == 25 || currentFrame == 33 || currentFrame == 40)
                column = 5;
            if (currentFrame == 6 || currentFrame == 13 || currentFrame == 20 || currentFrame == 26 || currentFrame == 34 || currentFrame == 41)
                column = 6;

            // Encadre la frame
            sourceRect = new Rectangle(column * SpriteWidth, line * SpriteHeight, SpriteWidth, SpriteHeight);

            if (currentKBState.IsKeyDown(Keys.Right) == true)
            {
                AnimateRight(gameTime);

                if (SpritePosition.X < ((Config.MapLayer.Width + Config.MapLayer.X) - (SpriteWidth / 2)))
                {
                    if (!Map.IsWall((int) CellPosition.X + 1, (int) CellPosition.Y))
                    {
                        direction = Vector2.Normalize(new Vector2(1, 0));
                        SpritePosition += direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds * SpriteSpeed;
                    }
                }
            }

            else if (currentKBState.IsKeyDown(Keys.Left) == true)
            {
                AnimateLeft(gameTime);

                if (SpritePosition.X > Config.MapLayer.X + (SpriteWidth / 2))
                {
                    direction = Vector2.Normalize(new Vector2(-1, 0));
                    SpritePosition += direction * (float) gameTime.ElapsedGameTime.TotalMilliseconds * SpriteSpeed;
                }
            }

            else if (currentKBState.IsKeyDown(Keys.Down) == true)
            {
                AnimateDown(gameTime);

                if (SpritePosition.Y < ((Config.MapLayer.Height + Config.MapLayer.Y) - (SpriteHeight / 2)))
                {
                    direction = Vector2.Normalize(new Vector2(0, 1));
                    SpritePosition += direction * (float) gameTime.ElapsedGameTime.TotalMilliseconds * SpriteSpeed;
                }
            }

            else if (currentKBState.IsKeyDown(Keys.Up) == true)
            {
                AnimateUp(gameTime);

                if (SpritePosition.Y > Config.MapLayer.Y + (SpriteHeight / 2))
                {
                    direction = Vector2.Normalize(new Vector2(0, -1));
                    SpritePosition += direction * (float) gameTime.ElapsedGameTime.TotalMilliseconds * SpriteSpeed;
                }
            }

            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            CellPosition = Map.PointToVector((int) (SpritePosition.X), (int) (SpritePosition.Y + SpriteHeight/4));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, SpritePosition, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            //spriteBatch.Draw(SpriteTexture, SpritePosition, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void AnimateRight(GameTime gameTime)
        {
            if (currentKBState != previousKBState)
            {
                currentFrame = (facing) ? 14 : 28;    
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (facing)
                {
                    if (currentFrame > 20)
                    {
                        currentFrame = 14;
                    }
                }

                else
                {
                    if (currentFrame > 34)
                    {
                        currentFrame = 28;
                    }
                }

                timer = 0f;
            }
        }

        public void AnimateUp(GameTime gameTime)
        {
            if (currentKBState != previousKBState)
            {
                currentFrame = 35;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            facing = false;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 41)
                {
                    currentFrame = 35;
                }

                timer = 0f;
            }
        }

        public void AnimateDown(GameTime gameTime)
        {
            if (currentKBState != previousKBState)
            {
                currentFrame = 0;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            facing = true;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 6)
                {
                    currentFrame = 0;
                }

                timer = 0f;
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            if (currentKBState != previousKBState)
            {
                currentFrame = (facing) ? 7 : 21;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (facing)
                {
                    if (currentFrame > 13)
                    {
                        currentFrame = 7;
                    }
                }

                else
                {
                    if(currentFrame > 27)
                    {
                        currentFrame = 21;
                    }
                }

                timer = 0f;
            }
        }

        public Rectangle SourceRectangle()
        {
            return sourceRect;
        }
    }
}
