﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainGame
{
    class Player
    {
        private bool caught;
        private int orientation;
        private Rectangle playerRect;
        private Texture2D playerTexture;
        private bool flip;
        private Rectangle playerCollision;
        //Position Properties to help change values

        public int X // player's x coord
        {
            get { return this.playerRect.X; }
            set
            {
                this.playerRect = new Rectangle(new Point(value, this.playerRect.Y), this.playerRect.Size);
                this.playerCollision = new Rectangle(new Point(this.playerRect.X, this.playerRect.Y - (playerRect.Height - 40)), this.playerCollision.Size);
            }
        }
        public int Y // player's y coord
        {
            get { return this.playerRect.Y; }
            set
            {
                this.playerRect = new Rectangle(new Point(this.playerRect.X, value), this.playerRect.Size);
                this.playerCollision = new Rectangle(new Point(this.playerCollision.X, this.playerRect.Y - (playerRect.Height - 40)), this.playerCollision.Size);
            }
        }

        // player's dimensions
        public int PlayerHeight
        {
            get { return playerRect.Height; }
        }

        public int PlayerWidth
        {
            get { return playerRect.Width; }
        }

        //This represents the orientation in degrees, Clockwise, from  the +x axis
        public int Orientation
        {
            get { return this.orientation; }
            set
            {
                orientation = value;
            }
        }
        //Simple bool for flip/no flip
        public bool Flip
        {
            get { return this.flip; }
            set
            {
                this.flip = value;
            }
        }
        //This is for changing the texture(to simulate walking)
        public Texture2D PlayerTexture
        {
            get { return this.playerTexture; }
            set
            {
                this.playerTexture = value;
            }
        }

        public Player(Rectangle rect,Texture2D txt) : base(rect, txt)
        {
            this.playerTexture = txt;
            this.playerRect = rect;
            this.orientation = 000;
            this.flip = true;

            this.playerCollision = new Rectangle(new Point(this.X, this.Y - (playerRect.Height - 40)), new Point(playerRect.Width, 30));
        }


        public void Draw(SpriteBatch sb)
        {
            
            if (flip == false)
            {
                sb.Draw(playerTexture, playerRect, null, Color.White, (float)(((double)orientation/180)*Math.PI), new Vector2(50, 50), SpriteEffects.None, 0f);
            }else if(flip == true)
            {
                sb.Draw(playerTexture, playerRect, null, Color.White, (float)(((double)orientation/180)*Math.PI), new Vector2(50, 50), SpriteEffects.FlipHorizontally, 0f);
            }
        }

      
    }
}
