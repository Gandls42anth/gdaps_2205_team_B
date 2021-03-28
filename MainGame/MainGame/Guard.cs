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
        private Rectangle guardRect;
        private Texture2D guardTexture;
        private Rectangle guardCollision;
        private int orientation;

        // constructor
        public Guard(Rectangle rect,Texture2D txt, int alert) : base(rect, txt)
        {
            this.guardTexture = txt;
            this.guardRect = rect;
            this.alert = alert;
        }

        public int X
        {
            get { return this.guardRect.X; }

            set
            {
                this.guardRect = new Rectangle(new Point(value, this.guardRect.Y), this.guardRect.Size);
                //this.guardCollision = new Rectangle(new Point(this.guardRect.X, this.guardRect.Y - (guardRect.Height - 40)), this.guardCollision.Size);
            }
        }

        public int Y
        {
            get { return this.guardRect.Y; }

            set
            {
                this.guardRect = new Rectangle(new Point(value, this.guardRect.X), this.guardRect.Size);
            }
        }

        public int GuardHeight
        {
            get { return guardRect.Height; }
        }

        public int GuardWidth
        {
            get { return guardRect.Width; }
        }

        public int Orientation
        {
            get { return this.orientation; }
            set
            {
                orientation = value;
            }
        }

        // guard movement
        public void GuardMovement()
        {

        }

        public void Draw(SpriteBatch sb)
        {

            //sb.Draw(playerTexture, playerRect, null, Color.White, (float)(((double)orientation / 180) * Math.PI), new Vector2(50, 50), SpriteEffects.None, 0f);

            sb.Draw(guardTexture, guardRect, null, Color.White, (float)(((double)orientation / 180) * Math.PI), new Vector2(50,50), SpriteEffects.None, 0f);
            
        }
    }
}
