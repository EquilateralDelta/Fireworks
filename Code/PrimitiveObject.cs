using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    public class PrimitiveObject : Entity
    {
        Texture2D sprite;
        protected Color color { get; private set; }
        Color tempColor;

        float fadeSpeed;
        public float opacity { get; set; }
        float airResistance;
        public Vector2 scale { get; set; }
        protected bool forDelete;

        public PrimitiveObject(Vector2 pos, float iMass, float angle, float speed, Texture2D iSprite, Color iColor, float airRes = 0, float fSpeed = 0)
            : this(pos, iSprite, iColor)
        {
            mass = iMass;
            airResistance = airRes;
            fadeSpeed = fSpeed;
            Forse(speed * speed * mass, angle);
        }

        public PrimitiveObject(Vector2 pos, Texture2D iSprite, Color iColor) : base(pos, 0, 0)
        {
            sprite = iSprite;
            color = iColor;
            tempColor = iColor;
            fadeSpeed = 0;
            airResistance = 0;
            opacity = 1;
            scale = Vector2.One;
            forDelete = false;
        }

        static public bool isDelete(PrimitiveObject obj) { return obj.forDelete; }

        virtual public void Update(int millisecs)
        {
            if (!(new Rectangle(-50, -50, (int)Game1.windowSize.X + 50, (int)Game1.windowSize.Y + 50).Contains(new Point((int)position.X, (int)position.Y)))
                || (opacity <= 0))
                forDelete = true;

            if (fadeSpeed != 0)
            {
                opacity -= fadeSpeed * millisecs;
            }
            if (opacity < 0)
                tempColor.A = 0;
            else
                tempColor.A = (byte)(color.A * opacity);

            forseAirResistance(airResistance);

            angle = speedAngle;

            UpdateEntity(millisecs);
        }

        virtual public void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite, position, new Rectangle(0, 0, sprite.Width, sprite.Height), 
                tempColor, angle, new Vector2(.5f * sprite.Width, .5f * sprite.Height), scale, SpriteEffects.None, 1);
        }
    }
}
