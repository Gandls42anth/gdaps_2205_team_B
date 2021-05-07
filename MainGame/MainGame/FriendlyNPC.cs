using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;

namespace MainGame
{
    class FriendlyNPC : GameObject
    {
        // fields
        private Rectangle npcRect;
        private Texture2D npcTexture;
        private string message;
        private bool touched;
        private int messageNum;
        private Random rng;
        Timer npcTime;
        int elapsed;

        // properties
        // no property for touched, message number, or rng 
        //since there's no need to get/set them outside of this class
        public Rectangle NPCRect { get { return npcRect; } }
        public Texture2D NPCTexture { get { return npcTexture; } }
        public string Message { get { return message; } }

        // position/size properties
        public int X
        {
            get { return this.npcRect.X; }
            set
            {
                this.npcRect = new Rectangle(new Point(value, this.npcRect.Y), this.npcRect.Size);
            }
        }
        public int Y
        {
            get { return this.npcRect.Y; }
            set
            {
                this.npcRect = new Rectangle(new Point(this.npcRect.X, value), this.npcRect.Size);
            }
        }
        public int NPCWidth
        {
            get { return this.npcRect.Width; }
            set
            {
                this.npcRect.Width = value;
            }
        }
        public int NPCHeight
        {
            get { return this.npcRect.Height; }
            set
            {
                this.npcRect.Height = value;
            }
        }
        public bool Touched
        {
            get { return this.touched; }
        }

        // constructor
        public FriendlyNPC(Rectangle rect, Texture2D txt) : base(rect,txt)
        {
            this.npcRect = rect;
            this.npcTexture = txt;
            this.touched = false;

            npcTime = new System.Timers.Timer();
            this.elapsed = 0;

            this.rng = new Random();

            // rng decides what the npc says
            this.messageNum = rng.Next(15);
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

                case 4:
                    this.message = "Fun fact: birds aren't real";
                    break;
                //The following quotes are made by Andy
                case 5:
                    this.message = "Life is one big Jojo reference";
                    break;

                case 6:
                    this.message = "I'm a Sonic fan, so what?";
                    break;

                case 7:
                    this.message = "Download CounterStrike source";
                    break;

                case 8:
                    this.message = "Man, this really has been a 'Giraffe Noise 2'";
                    break;

                case 9:
                    this.message = "Only a small percentage of you are subscribed";
                    break;

                case 10:
                    this.message = "STOP POSTING ABOUT AMONG US! I'M TIRED OF SEEING IT!";
                    break;

                case 11:
                    this.message = "Buy Miitopia for the Nintendo Switch on May 21, 2021";
                    break;

                case 12:
                    this.message = "Nice of the princess to invite us over for a picnic, eh Luigi?";
                    break;

                case 13:
                    this.message = "Among Us in real life SUS SUS";
                    break;

                case 14:
                    this.message = "( ͡° ͜ʖ ͡°)";
                    break;
                    // default case makes the message blank since rng shouldn't give a number outside the given range
                default:
                    this.message = "";
                    break;
            }
        }

        // interaction detection
        public void Update(Rectangle incoming)
        {
            // collision detection
            if(this.npcRect.Intersects(incoming))
            {
                this.touched = true;

                this.npcTime.Start();
                this.npcTime.Elapsed += new ElapsedEventHandler(AfterTime);
            }

        }

        private void AfterTime(object sender, ElapsedEventArgs e)
        {
            this.npcTime.Stop();
            if(this.elapsed > 500)
            {
                this.touched = false;
            }
            else
            {
                this.npcTime.Start();
            }
            this.elapsed += 1;
        }

        // personalized draw method
        public void Draw(SpriteBatch sb, SpriteFont sf)
        {
            sb.Draw(npcTexture, NPCRect, null, Color.White, 0, new Vector2(50, 50), SpriteEffects.None, 0f);

            if (this.touched)
            {
                sb.DrawString(sf, this.message, new Vector2(35, 385), Color.White);
            }
        }

    }
}
