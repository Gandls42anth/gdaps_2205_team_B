using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace MainGame
{
    enum GameState
    {
        Title,
        Normal,
        Hard,
        Speedrun,
        GameOver,
        Win
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // fields
        private GameState currentState;

        private Player player;
        private SpriteFont backLayer;
        private SpriteFont frontLayer;
        private SpriteFont Subtitle;
        private SpriteFont Normal;
        private Texture2D GiraffeSprite;
        private Texture2D GiraffeSpriteWalk;
        private Texture2D viewCone;
        private Texture2D finishLine;
        private List<Color> Colors;
        private KeyboardState KBS;
        private KeyboardState prevKBS;
        private int c;
        private int playTime;
        private int shift;
        private StreamReader reader;
        private StreamWriter writer;
        private bool written;
        private Level level;

        private Rectangle GiraffeRectangle;

        private List<Guard> Guards;
        private Texture2D GuardSprite;
        private Rectangle GuardRectangle;
        private Guard guard1;


        private Texture2D background;

        //This is for the game over screen
        private Texture2D deadGiraffeSprite;
        private Rectangle deadGiraffeRectangle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playTime = 0;
            c = 0;
            shift = 0;
            written = false;
            Colors = new List<Color>(6) { Color.Red, Color.DarkOrange, Color.Yellow, Color.Green, Color.Blue, Color.Violet };
            this.prevKBS = new KeyboardState();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // for title screen
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this.backLayer = this.Content.Load<SpriteFont>("File");
            this.frontLayer = this.Content.Load<SpriteFont>("FrontLayer");
            this.Subtitle = this.Content.Load<SpriteFont>("Subtitle");
            this.Normal = this.Content.Load<SpriteFont>("Normal");

            // for in game
            this.viewCone = this.Content.Load<Texture2D>("unnamed");
            this.finishLine = this.Content.Load<Texture2D>("1227835");
            this.GiraffeSprite = this.Content.Load<Texture2D>("NewGiraffe");
            this.GiraffeSpriteWalk = this.Content.Load<Texture2D>("GiraffeWalk");

            //Background
            this.background = this.Content.Load<Texture2D>("roadLong");

            //Guards
            this.GuardSprite = this.Content.Load<Texture2D>("BrandNewGuard");

            //Dead Giraffe
            deadGiraffeSprite = this.Content.Load<Texture2D>("GiraffeDead");
            deadGiraffeRectangle = new Rectangle(500, 200, deadGiraffeSprite.Width / 4, deadGiraffeSprite.Height / 4);

            currentState = GameState.Title;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KBS = Keyboard.GetState();

            // TODO: Add your update logic here

            switch (currentState)
            {
                case GameState.Title:
                    written = false;
                    if (c % 30 == 0)
                    {
                        shift++;
                    }
                    c++;
                    //This handles the switches between states
                    //But first it handles initialization logic
                    if (SingleKeyPress(Keys.N, KBS, prevKBS))
                    {
                        currentState = GameState.Normal;
                        GiraffeRectangle = new Rectangle(100, 200, (int)GiraffeSprite.Width /2 , (int)GiraffeSprite.Height/2);
                        GuardRectangle = new Rectangle(750, 305, (int)GuardSprite.Width /2, (int)GuardSprite.Height/2 );
                        guard1 = new Guard(GuardRectangle, this.GuardSprite, 3, this.viewCone);
                        this.player = new Player(GiraffeRectangle, this.GiraffeSprite);
                        //Attempting first level creation
                        this.level = new Level(GameState.Normal, 0, background, new Rectangle(0, 100, (int)background.Width / 2, (int)background.Height / 2), guard1);
                    }
                    else if (SingleKeyPress(Keys.H, KBS, prevKBS))
                    {
                        currentState = GameState.Hard;
                        GiraffeRectangle = new Rectangle(100, 200, (int)GiraffeSprite.Width/2 , (int)GiraffeSprite.Height/2 );
                        GuardRectangle = new Rectangle(750, 305, (int)GuardSprite.Width /2, (int)GuardSprite.Height /2);
                        guard1 = new Guard(GuardRectangle, this.GuardSprite, 3, this.viewCone);
                        this.player = new Player(GiraffeRectangle, this.GiraffeSprite);
                        //Attempting first level creation
                        this.level = new Level(GameState.Hard, 0, background, new Rectangle(0, 100, (int)background.Width / 2, (int)background.Height / 2), guard1);

                    }
                    else if (SingleKeyPress(Keys.S, KBS, prevKBS))
                    {
                        currentState = GameState.Speedrun;
                        GiraffeRectangle = new Rectangle(100, 200, (int)GiraffeSprite.Width / 2, (int)GiraffeSprite.Height / 2);
                        GuardRectangle = new Rectangle(750, 305, (int)GuardSprite.Width / 2, (int)GuardSprite.Height / 2);
                        guard1 = new Guard(GuardRectangle, this.GuardSprite, 3, this.viewCone);
                        this.player = new Player(GiraffeRectangle, this.GiraffeSprite);
                        //Attempting first level creation
                        this.level = new Level(GameState.Normal, 0, background, new Rectangle(0, 100, (int)background.Width / 2, (int)background.Height / 2), guard1);
                    }
                    break;


                //This Section handless the state logic
                //For the Normal, Hard and speedrun Modes
                case GameState.Normal:
                    playTime += 1;
                    level.Move(2);

                    // player movement
                    player.Update(gameTime);


                    // temporary for now, until we can get the full game working
                    if (SingleKeyPress(Keys.Enter, KBS, prevKBS))
                    {
                        currentState = GameState.Title;
                    }


                    if (level.Win(player))
                    {
                        currentState = GameState.Win;
                    }
                    if (level.Collision(player))
                    {
                        currentState = GameState.GameOver;
                    }



                    break;

                case GameState.Hard:
                    playTime += 1;
                    level.Move(2);

                    // player movement
                    player.Update(gameTime);


                    // temporary for now, until we can get the full game working
                    if (SingleKeyPress(Keys.Enter, KBS, prevKBS))
                    {
                        currentState = GameState.Title;
                    }


                    if (level.Win(player))
                    {
                        currentState = GameState.Win;
                    }
                    if (level.Collision(player))
                    {
                        currentState = GameState.GameOver;
                    }
                    break;

                case GameState.Speedrun:
                    playTime += 1;
                    level.Move(2);

                    // player movement
                    player.Update(gameTime);


                    // temporary for now, until we can get the full game working
                    if (SingleKeyPress(Keys.Enter, KBS, prevKBS))
                    {
                        currentState = GameState.Title;
                    }


                    if (level.Win(player))
                    {
                        currentState = GameState.Win;
                    }
                    if (level.Collision(player))
                    {
                        currentState = GameState.GameOver;
                    }
                    break;

                case GameState.GameOver:
                    c++;
                    if (!written)
                    {
                        written = true;
                        writer = new StreamWriter("Scores");
                        writer.Write(playTime / 60 + "s");
                        writer.Close();
                    }
                    if (SingleKeyPress(Keys.Enter, KBS, prevKBS))
                    {
                        currentState = GameState.Title;
                    }
                    break;
                case GameState.Win:
                    c++;
                    if (SingleKeyPress(Keys.Enter, KBS, prevKBS))
                    {
                        currentState = GameState.Title;
                    }
                    break;

                default:
                    break;
            }
            prevKBS = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();

            switch (currentState)
            {
                case GameState.Title:
                    GraphicsDevice.Clear(Color.Black);

                    // code for the special rainbow title screen
                    _spriteBatch.DrawString(frontLayer, "Giraffe Noise 2", new Vector2(35, 7), Color.OrangeRed);
                    _spriteBatch.DrawString(Subtitle, "Choose Mode: ", new Vector2(35, 187), Color.Green);
                    _spriteBatch.DrawString(Normal, "Normal - Press 'N'\nHard - Press 'H'", new Vector2(35, 237), Color.Gray);
                    _spriteBatch.DrawString(frontLayer, "B", new Vector2(35, 87 + (15 * (float)Math.Sin(c / 6))), Colors.ToArray()[(shift) % Colors.Count]);
                    _spriteBatch.DrawString(frontLayer, "E", new Vector2(95, 87 + (15 * (float)Math.Sin(c / 6 + 30))), Colors.ToArray()[(1 + shift) % Colors.Count]);
                    _spriteBatch.DrawString(frontLayer, "G", new Vector2(155, 87 + (15 * (float)Math.Sin(c / 6 + 60))), Colors.ToArray()[(2 + shift) % Colors.Count]);
                    _spriteBatch.DrawString(frontLayer, "I", new Vector2(235, 87 + (15 * (float)Math.Sin(c / 6 + 90))), Colors.ToArray()[(3 + shift) % Colors.Count]);
                    _spriteBatch.DrawString(frontLayer, "N", new Vector2(275, 87 + (15 * (float)Math.Sin(c / 6 + 120))), Colors.ToArray()[(4 + shift) % Colors.Count]);
                    _spriteBatch.DrawString(frontLayer, "?", new Vector2(345, 87 + (15 * (float)Math.Sin(c / 6 + 150))), Colors.ToArray()[(5 + shift) % Colors.Count]);
                    _spriteBatch.DrawString(Normal, "Speedrun - Press 'S'", new Vector2(35, 307), Color.Gray);

                    break;

                case GameState.Normal:
                    level.Draw(_spriteBatch,Normal,finishLine,this.Subtitle);
                    _spriteBatch.DrawString(
                        frontLayer, 
                        string.Format("Total Guards: {0}",level.Guards.Count), 
                        new Vector2(35, 7),
                        Color.OrangeRed
                        );

                    _spriteBatch.DrawString(Normal, string.Format("To exit, press enter"), new Vector2(35, 435), Color.Yellow);
                    player.Draw(_spriteBatch);
                    
                    break;

                case GameState.Hard:
                    level.Draw(_spriteBatch, Normal, finishLine, this.Subtitle);
                    _spriteBatch.DrawString(
                        frontLayer,
                        string.Format("Total Guards: {0}", level.Guards.Count),
                        new Vector2(35, 7),
                        Color.OrangeRed
                        );

                    _spriteBatch.DrawString(Normal, string.Format("To exit, press enter"), new Vector2(35, 435), Color.Yellow);
                    player.Draw(_spriteBatch);

                    break;

                case GameState.Speedrun:
                    level.Draw(_spriteBatch, Normal, finishLine, this.Subtitle);
                    _spriteBatch.DrawString(
                        frontLayer,
                        string.Format("Total Guards: {0}", level.Guards.Count),
                        new Vector2(35, 7),
                        Color.OrangeRed
                        );

                    _spriteBatch.DrawString(Normal, string.Format("To exit, press enter"), new Vector2(35, 435), Color.Yellow);
                    player.Draw(_spriteBatch);
                    break;

                case GameState.GameOver:
                    GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.DrawString(frontLayer, "GAME OVER", new Vector2(35, 7), Color.OrangeRed);
                    _spriteBatch.DrawString(Normal, "To exit, press enter", new Vector2(35, 307), Color.Yellow);
                    player.Orientation = c;
                    player.Draw(_spriteBatch);
                    break;
                case GameState.Win:
                    GraphicsDevice.Clear(Color.White);
                    _spriteBatch.DrawString(frontLayer, "You WIN!", new Vector2(35, 7), Color.GreenYellow);
                    _spriteBatch.DrawString(Normal, "To exit, press enter", new Vector2(35, 307), Color.Black);
                    player.Orientation = c;
                    player.Draw(_spriteBatch);
                    break;

                default:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // key press helper methods
        protected bool SingleKeyPress(Keys key, KeyboardState kbs, KeyboardState prevkbs)
        {
            return (kbs.IsKeyUp(key) && prevkbs.IsKeyDown(key));
        }

        protected bool KeyHold(Keys key, KeyboardState kbs, KeyboardState prevkbs)
        {
            return (kbs.IsKeyDown(key) && prevkbs.IsKeyDown(key));
        }

        protected bool SingleMousePress(ButtonState b, MouseState ms, MouseState prevms)
        {
            return (ms.LeftButton != b && prevms.LeftButton == b);
        }

        //This method checks for if the player makes contact with any guards
        private void Contact()
        {
            if (player.Y == guard1.Y)
            {
                if (player.X > guard1.X)
                {
                    if (player.X -guard1.X < 15)
                    {
                        currentState = GameState.GameOver;
                    }
                }

                if (guard1.X > player.X)
                {
                    if (guard1.X - player.X < 100)
                    {
                        currentState = GameState.GameOver;
                    }
                }

                if (player.X == guard1.X)
                {
                    currentState = GameState.GameOver;
                }


            }

            /*
            if (player.Position.Intersects(guard1.Position) == true)
            {
                currentState = GameState.GameOver;
            }
            */

            //For Andy, the old giraffe sprite was divided by 4 and the guard sprite divided by 2
        }
    }
}
