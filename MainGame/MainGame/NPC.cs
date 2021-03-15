using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainGame
{
    class NPC : GameObject
    {

        public NPC(Rectangle rect, Texture2D txt, string Message) : base(rect,txt)
        {

        }
    }
}
