using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Fireworks
{
    class Art
    {
        static public Texture2D firework;
        static public Texture2D moon;
        static public Texture2D star;
        static public Texture2D point;

        static public void Load(ContentManager content)
        {
            firework = content.Load<Texture2D>("firework");
            moon = content.Load<Texture2D>("moon");
            star = content.Load<Texture2D>("star");
            point = content.Load<Texture2D>("point");
        }
    }
}
