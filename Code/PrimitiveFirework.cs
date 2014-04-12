using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    class PrimitiveFirework : PrimitiveObject
    {
        protected static Random rand = new Random();
        protected static int creationOffset = 150;
        protected static int LifeTime = 1500;
        protected static int Mass = 1;
        protected static double minSpeed = .2;
        protected static double maxMinusMinSpeed = .3;
        protected static double angleMaxDev = Math.PI / 2.5;
        protected static float airResistance = .00005f;
        protected static int pointsMin = 150, pointsAdd = 100;

        protected static int colorDev = 100;
        protected static float pointsSpeed = .5f;
        
        int lifeTime;
        int pointsNumber;

        protected PrimitiveFirework(Vector2 iPos, float iAngle, float iSpeed, Color iColor, int points)
            : base(iPos, Mass, iAngle, iSpeed, Art.firework, iColor, airResistance)
        {
            lifeTime = LifeTime;
            pointsNumber = points;
            scale = new Vector2(((float)pointsNumber) / (pointsMin + pointsAdd));  
        }

        public override void Update(int millisecs)
        {
            lifeTime -= millisecs;
            if (lifeTime < 0)
            {
                createPoints();
                forDelete = true;
            }

            createTrial();
            base.Update(millisecs);
        }

        void createTrial()
        {
            for (int i = 0; i < 3; i++)
            {
                if (rand.Next(0, 1) == 0)
                {
                    float newAngle = angle - (float)Math.PI + (float)((rand.NextDouble() - .5) * Math.PI / 3);
                    float newSpeed = (.05f + (float)rand.NextDouble() / 6) * speed;
                    PrimitiveObject obj = new PrimitiveObject(position, .001f, newAngle, newSpeed, Art.point, color, 0, .001f);
                    obj.scale = new Vector2(.5f);
                    Game1.control.Add(obj);
                }
            }
        }

        void createPoints()
        {
            ColorConversion.ColorHSL oldColor = ColorConversion.ColorConversion.RGBtoHSL(color);

            for (int i = 0; i < pointsNumber; i++)
            {
                float speed = (float)rand.NextDouble() * pointsSpeed * ((float)pointsNumber / (pointsMin + pointsAdd));
                float angle = (float)(rand.NextDouble() * Math.PI * 2);
                Vector2 Speed = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * speed;
                Speed += speedVect;
                Color col = new Color(color.R + rand.Next(-colorDev, colorDev),
                    color.G + rand.Next(-colorDev, colorDev), color.B + rand.Next(-colorDev, colorDev), 255);


                PrimitiveObject obj = new PrimitiveObject(position, .05f, (float)Math.Atan2(Speed.Y, Speed.X), Speed.Length(),
                    Art.point, col, .03f, .0002f);
                obj.scale = new Vector2((float)rand.NextDouble() * .4f + .4f);

                Game1.control.Add(obj);    
            }
        }

        static public PrimitiveFirework getNew()
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
            PrimitiveFirework firework = new PrimitiveFirework(position, angle, speed, color, points);
            firework.scale = new Vector2(.6f);

            return firework;
        }
    }
}
