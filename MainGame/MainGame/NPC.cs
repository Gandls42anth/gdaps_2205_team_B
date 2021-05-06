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
        // fields
        private Rectangle npcRect;
        private Texture2D npcTexture;
        private string message;
        private bool touched;
        private int messageNum;
        private Random rng;

        // properties
        // no property for touched, message number, or rng 
        //since there's no need to get/set them outside of this class
        public Rectangle NPCRect { get { return npcRect; } }
        public Texture2D NPCTexture { get { return npcTexture; } }
        public string Message { get { return message; } }

        // constructor
        public NPC(Rectangle rect, Texture2D txt) : base(rect,txt)
        {
            this.npcRect = rect;
            this.npcTexture = txt;
            this.touched = false;

            // rng decides what the npc says
            this.messageNum = rng.Next(4);
            switch(messageNum)
            {
                case 0:
                    this.message = "You got this Mr./Mrs./Mx. Giraffe!";
                    break;

                case 1:
                    this.message = "I'd give you something but I don't have money.";
                    break;

                case 2:
                    this.message = "Watch out! There's doggy doo in front of you!";
                    break;

                case 3:
                    this.message = "Hey, want a cupcake?";
                    break;

                    // default case makes the message blank since rng shouldn't give a number outside the given range
                default:
                    this.message = "";
                    break;
            }
        }

        // interaction detection
        public void Interact(Rectangle incoming)
        {
            if(this.npcRect.Contains(incoming) || this.npcRect.Intersects(incoming))
            {
                this.touched = true;
            }
        }

        // personalized draw method

        public void Draw(SpriteBatch sb, SpriteFont sf)
        {
            sb.Draw(npcTexture, NPCRect, null, Color.White, 0f, new Vector2(50, 50), SpriteEffects.None, 0f);

            if (this.touched)
            {
                sb.DrawString(sf, this.message, new Vector2(35, 400), Color.White);
            }
        }
    }
}
