using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TitleScreenTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont backLayer;
        private SpriteFont frontLayer;
        private SpriteFont Subtitle;
        private SpriteFont Normal;
        private List<Color> Colors;
        private int c;
        private int shift;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            c = 0;
            shift = 0;
            Colors = new List<Color>(6) { Color.Red, Color.DarkOrange, Color.Yellow, Color.Green, Color.Blue, Color.Violet };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this.backLayer = this.Content.Load<SpriteFont>("File");
            this.frontLayer = this.Content.Load<SpriteFont>("FrontLayer");
            this.Subtitle = this.Content.Load<SpriteFont>("Subtitle");
            this.Normal = this.Content.Load<SpriteFont>("Normal");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            if (c % 30 == 0)
            {
                shift++;
            }
            c++;




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(Subtitle, "Choose Mode: ", new Vector2(35, 137), Color.Green);
            _spriteBatch.DrawString(Normal, "Normal\nHard", new Vector2(35, 187), Color.Gray);
            _spriteBatch.DrawString(frontLayer, "B", new Vector2(35, 37 + (15 * (float)Math.Sin(c / 6))), Colors.ToArray()[(shift) % Colors.Count]);
            _spriteBatch.DrawString(frontLayer, "E", new Vector2(95, 37 + (15 * (float)Math.Sin(c / 6 + 30))), Colors.ToArray()[(1 + shift) % Colors.Count]);
            _spriteBatch.DrawString(frontLayer, "G", new Vector2(155, 37 + (15 * (float)Math.Sin(c / 6 + 60))), Colors.ToArray()[(2 + shift) % Colors.Count]);
            _spriteBatch.DrawString(frontLayer, "I", new Vector2(235, 37 + (15 * (float)Math.Sin(c / 6 + 90))), Colors.ToArray()[(3 + shift) % Colors.Count]);
            _spriteBatch.DrawString(frontLayer, "N", new Vector2(275, 37 + (15 * (float)Math.Sin(c / 6 + 120))), Colors.ToArray()[(4 + shift) % Colors.Count]);
            _spriteBatch.DrawString(frontLayer, "?", new Vector2(345, 37 + (15 * (float)Math.Sin(c / 6 + 150))), Colors.ToArray()[(5 + shift) % Colors.Count]);
            _spriteBatch.DrawString(Normal, "Speedrun", new Vector2(35, 257), Color.Gray);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
