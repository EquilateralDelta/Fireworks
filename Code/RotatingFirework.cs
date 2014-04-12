using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    class RotatingFirework : PrimitiveFirework
    {
        float upForse;
        float angleSpeed;

        RotatingFirework(Vector2 iPos, float iAngle, float iSpeed, Color iColor, int points, float upForse, float angleSpeed)
            : base(iPos, iAngle, iSpeed, iColor, points)
        {
            this.upForse = upForse;
            this.angleSpeed = angleSpeed;
        }

        public override void Update(int millisecs)
        {
            //Forse(upForse * millisecs, -(float)Math.PI / 2);
            Forse(upForse * speed * speed, angle);
            ForseAngle(angleSpeed);

            base.Update(millisecs);
        }

        static public RotatingFirework getNew()
        {
            Vector2 position;
            position.X = rand.Next(creationOffset, (int)Game1.windowSize.X - creationOffset - 450);
            position.Y = Game1.windowSize.Y - 1;

            float speed = (float)(rand.NextDouble() * maxMinusMinSpeed + minSpeed);
            float angle = (float)(-Math.PI / 2 + (rand.NextDouble() - .5) * angleMaxDev);
            ColorConversion.ColorHSL newColor = new ColorConversion.ColorHSL();
            newColor.H = rand.Next(360);
            newColor.L = .5f; newColor.S = 1f;
            Color color = ColorConversion.ColorConversion.HSLtoRGB(newColor);
            //new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), 255);
            int points = pointsMin + rand.Next(pointsAdd);
            float newUpForse = .00005f * Mass / speed;
            float newAngleSpeed = (4f + (float)rand.NextDouble()) / 5000 / speed;
            if (rand.Next(0, 1) == 0)
                newAngleSpeed *= -1;
            RotatingFirework firework = new RotatingFirework(position, angle, speed, color, points, newUpForse, ((float)rand.NextDouble() - .5f) / 10000);
            firework.scale = new Vector2(.6f, .8f);
            return firework;
        }
    }
}
