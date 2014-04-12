using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    class Star : PrimitiveObject
    {
        static int FLASHTIME = 60;

        Random rand;
        int flashTimer;
        bool isFlashing;
        Vector2 baseSize;

        private Star(Vector2 pos, Texture2D iSprite, Color iColor, Random rand, Vector2 size) : base(pos, iSprite, iColor) 
        {
            this.rand = rand;
            flashTimer = 0;
            isFlashing = false;
            this.scale = size;
            baseSize = size;
        }

        static public Star getInstance(Random rand)
        {
            Star star;
            Vector2 pos = new Vector2(rand.Next(0, (int)Game1.windowSize.X),
               rand.Next((int)Game1.windowSize.Y));

            Vector2 scale = new Vector2((float)(rand.NextDouble() + .1f) / 6);
            star = new Star(pos, Art.star, Color.LightYellow, rand, scale);
            star.opacity = .7f;
            return star;
        }

        public override void Update(int millisecs)
        {
            if (isFlashing)
            {
                flashTimer -= millisecs;
                if (flashTimer < -FLASHTIME)
                {
                    isFlashing = false;
                    this.scale = baseSize;
                }
                else
                    this.scale = Vector2.Multiply(baseSize, (1 + (float)(FLASHTIME - Math.Abs(flashTimer)) / FLASHTIME) * 2);
            }
            else
                if (rand.Next(0, 20000) == 42)
                {
                    isFlashing = true;
                    flashTimer = FLASHTIME;
                }

            base.Update(millisecs);
        }
    }
}
