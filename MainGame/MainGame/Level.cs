using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainGame
{
    class Level : GameObject
    {

        //This is the level class, it's a gameobject that represents an entire level
        private GameState GS;
        private int LevelNum;
        private Rectangle finishLine;
        private Rectangle defaultRect;
        
        //This represents a 2d  array of where guard should be in a level
        //Its first value is always 5 since thats the number of rows
        //The second value will be dependent on the length of the level, determined by the number of level and the difficulty
        private bool[,] guard;
        private Guard baseGuard;
        List<Guard> guardList;

        // the same goes for friendly npcs
        private bool[,] friend;
        private FriendlyNPC baseNPC;
        List<FriendlyNPC> npcList;

        public int Num
        {
            get { return this.LevelNum; }
        }

        //Helper Properties
        public int X
        {
            get { return this.position.X; }
            set
            {
                this.position.X = value;
                this.finishLine = new Rectangle(this.X + this.position.Width * (5 + LevelNum), this.Y, 100, this.position.Height);
            }
        }
        public int Y
        {
            get { return this.position.Y; }
            set
            {
                this.position.Y = value;
                this.finishLine = new Rectangle(this.X + this.position.Width * (5 + LevelNum), this.Y, 100, this.position.Height);
            }
        }

        public Rectangle DefaultRect
        {
            get { return this.defaultRect; }
        }

        public Rectangle FinishLine
        {
            get { return this.finishLine; }
        }

        public List<Guard> Guards
        {
            get { return this.guardList; }
        }

        public Guard BaseGuard
        {
            get { return this.baseGuard; }
        }

        public List<FriendlyNPC> NPCList
        {
            get { return this.npcList; }
        }

        // constructor
        public Level(GameState gs,int levelNum, Texture2D txt,Rectangle rect,Guard baseGuard, FriendlyNPC baseNPC) : base(rect,txt)
        {
            Random randy = new Random();
            this.defaultRect = rect;
            this.GS = gs;
            this.LevelNum = levelNum;
            bool curAdd = false;

            this.baseGuard = baseGuard;
            guardList = new List<Guard>();

            this.baseNPC = baseNPC;
            npcList = new List<FriendlyNPC>();

            this.finishLine = new Rectangle(this.X + this.position.Width * (5 + LevelNum), this.Y, 100, this.position.Height);

            //This is where the random generation logic is handled for levels
            if (gs == GameState.Normal)
            {
                // guard generation
                int guards = randy.Next(3 + levelNum,5 + levelNum * 2);
                int positionsNum = (5 + levelNum) * 3;
                guard = new bool[5, positionsNum];
                for (int i = 0; i < positionsNum; i++)
                {
                    for (int p = 0; p < 5; p++)
                    {
                        //This is a bit complicated to explain, but it allows for reasonable distribution of the guards, balancing
                        //The amount of guards left with the amount of spots left on the map
                        //It also favors creating them on different rows instead of different columns
                        //(Since all of them on different rows would create an easier game)
                        if (guards != 0)
                        {
                            int positionsLeft = guard.Length - (i*5) - p;
                            curAdd = randy.Next(positionsLeft) < guards;
                            guard[p, i] = curAdd;
                            if (curAdd)
                            {
                                guards -= 1;
                                guardList.Add(new Guard
                                    (new Rectangle
                                    (this.position.X + this.position.Width + ((int)this.position.Width / 3 * i),
                                    155 + 50 * p,
                                    (int)baseGuard.GuardWidth, 
                                    (int)baseGuard.GuardHeight),
                                    baseGuard.Texture, 
                                    levelNum,
                                    baseGuard.ViewCone,
                                    true));
                            }
                        }
                        else
                        //If we're all out of guard to give, the remaining spots will be set to false
                        {
                            guard[p, i] = false;
                        }
                    }
                }

                // friendly npc generation, there are a max of 3 friendlies per level
                // generation is similar to that of guard generation, just with a lot less of them (in higher levels at least)
                int friends = randy.Next(4);
                friend = new bool[5, 15];
                for(int n = 0; n < 15; n++)
                {
                    for(int m = 0; m < 5; m++)
                    {
                        if(friends != 0)
                        {
                            int positionsLeft = 15 - (n * 5) - m;
                            curAdd = randy.Next(positionsLeft) < friends;
                            friend[m, n] = curAdd;
                            if(curAdd)
                            {
                                friends -= 1;
                                npcList.Add(new FriendlyNPC(
                                    new Rectangle(
                                        this.position.X + this.position.Width + 
                                        ((int)this.position.Width / 3 * n),
                                        155 + 50 * n,
                                        (int)baseNPC.NPCWidth, (int)baseNPC.NPCHeight),
                                    baseNPC.Texture));
                            }
                        }
                        else
                        {
                            friend[m, n] = false;
                        }
                    }
                }

            }
            else if (gs == GameState.Hard)
            {
                int guards = randy.Next(5 + levelNum*2, 7 + levelNum * 3);
                int positionsNum = (5 + levelNum) * 3;
                guard = new bool[5, positionsNum];
                for (int i = 0; i < positionsNum; i++)
                {

                    {
                        for (int p = 0; p < 5; p++)
                        {
                            //This is a bit complicated to explain, but it allows for reasonable distribution of the guards, balancing
                            //The amount of guards left with the amount of spots left on the map
                            //It also favors creating them on different rows instead of different columns
                            //(Since all of them on different rows would create an easier game)
                            if (guards != 0)
                            {
                                int positionsLeft = guard.Length - (i * 5) - p;
                                curAdd = randy.Next(positionsLeft) < guards;
                                guard[p, i] = curAdd;
                                if (curAdd)
                                {
                                    guards -= 1;
                                    guardList.Add(new Guard
                                        (new Rectangle
                                        (this.position.X + this.position.Width + ((int)this.position.Width / 3 * i),
                                        155 + 50 * p,
                                        (int)baseGuard.GuardWidth,
                                        (int)baseGuard.GuardHeight),
                                        baseGuard.Texture, levelNum,
                                        baseGuard.ViewCone,
                                        true));
                                }
                            }
                            else
                            //If we're all out of guard to give, the remaining spots will be set to false
                            {
                                guard[p, i] = false;
                            }
                        }
                    }
                }

                // friendly npc generation, there are a max of 3 friendlies per level
                // generation is similar to that of guard generation, just with a lot less of them (in higher levels at least)
                int friends = randy.Next(4);
                friend = new bool[5, 15];
                for (int n = 0; n < 15; n++)
                {
                    for (int m = 0; m < 5; m++)
                    {
                        if (friends != 0)
                        {
                            int positionsLeft = 15 - (n * 5) - m;
                            curAdd = randy.Next(positionsLeft) < friends;
                            friend[m, n] = curAdd;
                            if (curAdd)
                            {
                                friends -= 1;
                                npcList.Add(new FriendlyNPC(
                                    new Rectangle(
                                        this.position.X + this.position.Width +
                                        ((int)this.position.Width / 3 * n),
                                        155 + 50 * n,
                                        (int)baseNPC.NPCWidth, (int)baseNPC.NPCHeight),
                                    baseNPC.Texture));
                            }
                        }
                        else
                        {
                            friend[m, n] = false;
                        }
                    }
                }
            }
            else
            {
                //This code is never supposed to run
                guard = new bool[0, 0];
            }
        }
        //The the Guard 2d array now has randomly placed "trues" where guards are supposed to be

        // personalized draw method
        public void Draw(SpriteBatch sb, SpriteFont nsf, Texture2D  finishLine, SpriteFont finish)
        {
            //This for loop allows for almost infinitely large levels to be drawn, by redrawing the background level sprite  and shifting the x
            //value over and over
            for(int i = 0; i < 5+LevelNum; i++)
            {
                sb.Draw(this.texture, new Rectangle(new Point(this.position.X + this.position.Width*i,this.position.Y), this.position.Size),Color.White);
                sb.DrawString(nsf,string.Format("{0}",i),new Vector2(this.position.X + this.position.Width*i,this.position.Y),Color.Red);
            }

            // guards
            for(int p = 0; p < guardList.Count; p++)
            {
                guardList[p].Draw(sb);
            }

            // friendly npcs
            for(int f = 0; f < npcList.Count; f++)
            {
                npcList[f].Draw(sb, nsf);
            }

            // finish line
            sb.Draw(finishLine, new Rectangle(this.X + this.position.Width * (5 + LevelNum), this.Y,100,this.position.Height),new Rectangle(this.X + this.position.Width * (5 + LevelNum), this.Y, 100, this.position.Height), Color.White * 2.0f);
            sb.DrawString(finish, "F\nI\nN\nI\nS\nH", new Vector2(this.X + this.position.Width * (5 + LevelNum) + 30,this.Y),Color.Black);
        }

        // for all npc movement
        public void Move(int speed = 5)
        {
            for(int i = 0; i < guardList.Count; i++)
            {
                guardList[i].X -= speed+2;
            }

            for(int i = 0; i < npcList.Count; i++)
            {
                npcList[i].X -= speed + 2;
            }

            this.X -= speed;
        }

        // collision detection for every guard
        public bool Collision(Player p)
        {
            for(int i = 0; i < guardList.Count; i++)
            {
                if (guardList[i].CollisionBox.Intersects(p.CollisionBox))
                {
                    return true;
                }
            }
            return false;
        }

        // player's win con
        public bool Win(Player p)
        {
            if (this.finishLine.Intersects(p.CollisionBox))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Level Next()
        {
            //We cant use the "position" rectangle because thats changing, on initialization we set a private rectangle as the default
            //Then reference that when a new level advancement is done
            return new Level(this.GS, this.LevelNum + 1, this.texture, this.defaultRect, this.baseGuard, this.baseNPC);
        }












    }
}
