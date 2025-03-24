using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace STG2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        
        public ScreenManager ScreenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 850;
            _graphics.PreferredBackBufferWidth = 480;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
           
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

           
            ScreenManager = new ScreenManager();
            ScreenManager.ChangeScreen(new Menu(this));



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {



            InputManager.Update();
            ScreenManager.Update(gameTime);
            base.Update(gameTime);

        }
        
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Transparent);


            ScreenManager.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
