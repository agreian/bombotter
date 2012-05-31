using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BombOtter.Sprite
{
    class CaseSprite : Sprite
    {
        public CaseSprite(Texture2D texture, int width, int height) : base(texture, width, height)
        {
        }

        public CaseSprite(Texture2D texture, int width, int height, Vector2 position) : base(texture, width, height, position)
        {
        }
    }
}
