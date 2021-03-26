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
        
        //This represents a 2d  array of where guard should be in a level
        //Its first value is always 5 since thats the number of rows
        //The second value will be dependent on the length of the level, determined by the number of level and the difficulty
        private bool[,] guard;
        
        public Level(GameState gs,int levelNum, Texture2D txt,Rectangle rect) : base(rect,txt)
        {
            Random randy = new Random();
            this.GS = gs;
            this.LevelNum = levelNum;
            bool curAdd = false;

            //This is where the random generation logic is handled for levels
            if (gs == GameState.Normal) {
                int guards = randy.Next(5 + levelNum * 2);
                int guardsAdded = 0;
                int positionsNum = (5 + levelNum) * 3;
                guard = new bool[5,positionsNum];
            for (int i = 0; i < positionsNum; i++)


                {


                    for(int p = 0; p < 5; p++)
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




            //This is the same code as before, with altered coefficients to create longer, denser maps
            }else if(gs == GameState.Hard)
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
                guard = new bool[0,0];
            }
        }












    }
}
