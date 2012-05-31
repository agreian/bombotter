﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BombOtter.Sprite
{
    class OtterSprite : Sprite
    {
        public float timer { get; set; }
        public float interval { get; set; }
        public int currentFrame { get; set; }
        public int spriteSpeed { get; set; }
        public Vector2 origin { get; set; }
        public Boolean facing { get; set; }

        KeyboardState currentKBState;
        KeyboardState previousKBState;

        public int WindowHeight { get; set; } // utiles pour déplacer la loutre
        public int WindowWidth { get; set; }

        public OtterSprite(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight, int windowWidth, int windowHeight) 
            : base(texture, spriteWidth, spriteHeight)
        {
            this.currentFrame = currentFrame;
            spritePosition = new Vector2(400, 300);
            facing = true; // Par défaut, la sprite est de face. Si elle monte, elle sera de dos
            spriteSpeed = 2;
            interval = 100f;
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
        }

        public void HandleSpriteMovement(GameTime gameTime)
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

            sourceRect = new Rectangle(column * spriteWidth, line * spriteHeight, spriteWidth, spriteHeight);

            if (currentKBState.IsKeyDown(Keys.Right) == true)
            {
                AnimateRight(gameTime);


                if (spritePosition.X < (WindowWidth - (spriteWidth / 2)))
                {
                    spritePosition = new Vector2(spritePosition.X + spriteSpeed, spritePosition.Y);
                }
            }

            else if (currentKBState.IsKeyDown(Keys.Left) == true)
            {
                AnimateLeft(gameTime);

                if (spritePosition.X > (spriteWidth / 2))
                {
                    spritePosition = new Vector2(spritePosition.X - spriteSpeed, spritePosition.Y);
                }
            }

            else if (currentKBState.IsKeyDown(Keys.Down) == true)
            {
                AnimateDown(gameTime);

                if (spritePosition.Y < (WindowHeight - (spriteHeight / 2)))
                {
                    spritePosition = new Vector2(spritePosition.X, spritePosition.Y + spriteSpeed);
                }
            }

            else if (currentKBState.IsKeyDown(Keys.Up) == true)
            {
                AnimateUp(gameTime);

                if (spritePosition.Y > 25)
                {
                    spritePosition = new Vector2(spritePosition.X, spritePosition.Y - spriteSpeed);
                }
            }

            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
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
    }
}
