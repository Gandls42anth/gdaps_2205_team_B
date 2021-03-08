using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainGame
{
    class Player : GameObject
    {
        protected Player(Rectangle rect,Texture2D txt) : base(rect, txt)
        {

        }
    }
}
