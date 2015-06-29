using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Fireworks
{
    public class Entity
    {
        static float gravityForse = 0.0000003f;

        protected float mass { get; set; }
        protected float speed { get; private set; }
        protected float speedAngle { get; private set; }
        Vector2 _speedVect;
        protected Vector2 speedVect
        {
            get { return _speedVect; }
            private set
            {
                _speedVect = value;
                speed = _speedVect.Length();
                speedAngle = (float)Math.Atan2(_speedVect.Y, _speedVect.X);
            }
        }
        protected Vector2 position { get; private set; }



        float _angle;
        protected float angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                if (_angle > Math.PI)
                    _angle -= (float)Math.PI * 2;
                if (_angle < -Math.PI)
                    _angle += (float)Math.PI * 2;
            }
        }
        protected float angleSpeed { get; private set; }

        public Entity(Vector2 pos, float iMass, float iAngle)
        {
            position = pos;
            mass = iMass;
            angle = iAngle;
            speedVect = Vector2.Zero;
            angleSpeed = 0;
        }

        protected void Forse(float value, float angle)
        {
            Vector2 addSpeed = speedFromForse(value, angle);

            speedVect += addSpeed;
        }


        protected void ForseAngle(float value)
        {
            value = energyToSpeed(value);
            angleSpeed += value;
        }

        protected void SetSpeed(Vector2 value)
        {
            speedVect = value;
        }

        protected void forseAirResistance(float res)
        {
            speedVect *= 1 - res;
        }

        protected void UpdateEntity(int millisecs)
        {
            Forse(gravityForse * mass * mass * millisecs, (float)Math.PI / 2);

            position += speedVect * millisecs;
            angle += angleSpeed * millisecs;
        }

        private Vector2 speedFromForse(float value, float iAngle)
        {
            value = energyToSpeed(value);

            return (new Vector2((float)Math.Cos(iAngle) * value,
                (float)Math.Sin(iAngle) * value));
        }

        private float energyToSpeed(float value)
        {
            if (mass == 0)
                return 0;
            float result;
            value /= mass;
            result = (float)Math.Sqrt(Math.Abs(value));
            if (value < 0)
                result = -result;
            return result;
        }
    }
}
