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
        // fields
        private Rectangle view;
        private int alert;

        // constructor
        public Guard(Rectangle rect,Texture2D txt, int alert) : base(rect, txt)
        {
            this.alert = alert;
        }

        // guard movement
        public void GuardMovement()
        {

        }
    }
}
