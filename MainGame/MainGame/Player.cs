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
        private bool caught;
        private int speed;
        private Rectangle playerRect;
        private Texture2D playerTexture;
        public Player(Rectangle rect,Texture2D txt) : base(rect, txt)
        {
            this.playerTexture = txt;
            this.playerRect = rect;
        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(playerTexture, playerRect, Color.White);
        }
    }
}
