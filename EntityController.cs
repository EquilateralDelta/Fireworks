using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    public class EntityController
    {
        static Random rand = new Random();
        List<PrimitiveObject> addList;
        List<PrimitiveObject> objList;
        bool isUpdating;

        public EntityController()
        {
            objList = new List<PrimitiveObject>();
            addList = new List<PrimitiveObject>();
            Random seed = new Random();

            CreateMoonAndStars();
        }

        private void CreateMoonAndStars()
        {
            Vector2 moonPosition = new Vector2(300, 100);
            int starCount = 300;

            PrimitiveObject moon;
            moon = new PrimitiveObject(moonPosition, Art.moon, Color.LightYellow);
            Add(moon);

            Random rand = new Random();

            for (int i = 0; i < starCount; i++)
                Add(Star.getInstance(rand));
        }

        public void Update(int millisecs)
        {
            if (rand.Next(50) == 0)
                addFirework();

            isUpdating = true;
            foreach (var obj in objList)
                obj.Update(millisecs);
            isUpdating = false;

            foreach (var obj in addList)
                Add(obj);
            addList.Clear();

            objList.RemoveAll(PrimitiveObject.isDelete);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var obj in objList)
                obj.Draw(batch);
        }

        public void Add(PrimitiveObject obj)
        {
            if (isUpdating)
                addList.Add(obj);
            else
                objList.Add(obj);
        }

        private void addFirework()
        {
            if (rand.Next(0, 2) == 0)
                Add(PrimitiveFirework.getNew());
            else
                Add(RotatingFirework.getNew());
        }
    }
}
