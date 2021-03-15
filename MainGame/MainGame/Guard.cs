using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainGame
{
    class Guard : GameObject
    {
        public Guard(Rectangle rect,Texture2D txt, int alert) : base(rect, txt)
        {

        }
    }
}
