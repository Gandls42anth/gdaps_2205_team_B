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

        //This is the level class, its a gameobject that represents an entire level
        private GameState GS;
        private int LevelNum;
        List<Guard> guardList;
        
        //This represents a 2d  array of where guard should be in a level
        //Its first value is always 5 since thats the number of rows
        //The second value will be dependent on the length of the level, determined by the number of level and the difficulty
        private bool[,] guard;

        //Helper Properties
        public int X
        {
            get { return this.position.X; }
            set
            {
                this.position.X = value;
            }
        }
        public int Y
        {
            get { return this.position.Y; }
            set
            {
                this.position.Y = value;
            }
        }

        public List<Guard> Guards
        {
            get { return this.guardList; }
        }

        
        public Level(GameState gs,int levelNum, Texture2D txt,Rectangle rect,Guard baseGuard) : base(rect,txt)
        {
            Random randy = new Random();
            this.GS = gs;
            this.LevelNum = levelNum;
            bool curAdd = false;
            guardList = new List<Guard>();

            //This is where the random generation logic is handled for levels
            if (gs == GameState.Normal)
            {
                int guards = randy.Next(5 + levelNum * 2);
                int guardsAdded = 0;
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
                                curAdd = randy.Next(guard.Length - (i * 5) - p) < guards;
                                guard[p, i] = curAdd;
                                if (curAdd)
                                {
                                    guards -= 1;
                                    guardsAdded += 1;
                                    guardList.Add(new Guard
                                        (new Rectangle
                                        (this.Position.X + this.texture.Width + ((int)this.Texture.Width / 3 * i),
                                        50 * p,
                                        (int)baseGuard.GuardWidth, 
                                        (int)baseGuard.GuardHeight), 
                                        baseGuard.Texture, levelNum));
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
            }
            else if (gs == GameState.Hard)
            {
                {
                    int guards = randy.Next(5 + levelNum * 3);
                    int guardsAdded = 0;
                    int positionsNum = (5 + levelNum) * 4;
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
                                curAdd = randy.Next((positionsNum * 5) - (i * 5) - p) < guards;
                                guard[p, i] = curAdd;
                                if (curAdd)
                                {
                                    guards -= 1;
                                    guardsAdded += 1;
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
            }
            else
            {
                //This code is never supposed to run
                guard = new bool[0, 0];
            }
        }
        //The the Guard 2d array now has randomly placed "trues" where guards are supposed to be



        public void Draw(SpriteBatch sb)
        {
            //This for loop allows for almost infinitely large levels to be drawn, by redrawing the background level sprite  and shifting the x
            //value over and over
            for(int i = 0; i < 5+LevelNum; i++)
            {
                sb.Draw(this.texture, new Rectangle(new Point(this.position.X + this.position.Width*i,this.position.Y), this.position.Size),Color.White);
            }
            for(int p = 0; p < guardList.Count; p++)
            {
                guardList[p].Draw(sb);
            }
        }


        public void Move(int speed = 5)
        {
            for(int i = 0; i < guardList.Count; i++)
            {
                guardList[i].X -= speed;
            }

            this.X -= speed;
        }












    }
}
