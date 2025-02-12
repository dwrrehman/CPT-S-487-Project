using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using STG;

namespace STG2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Texture2D _texture2;
        private Texture2D _texture3;

        private Background _background;
        private PlayerPlanne _playerPlanne;
        private Menu _menu;
        private bool _gameStarted = false;


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
            _texture = Content.Load<Texture2D>("background3");
            _texture2 = Content.Load<Texture2D>("plane");
            _background = new Background( new Vector2(0, 0),4, _texture);
            _playerPlanne = new PlayerPlanne(new Vector2(200, 750), _texture2,10,5,STG.direction.Up);
            _texture3 = Content.Load<Texture2D>("button2");
            var font = Content.Load<SpriteFont>("Fonts");
            _menu = new Menu(_texture3, font, new Vector2(100, 400),"Start");
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            if (!_gameStarted) {
                _menu.Update(gameTime);
                if (_menu.IsPressed) {
                    _gameStarted = true;
                }
            }
            else {
                var keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    _playerPlanne.Direction = direction.Up;
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    _playerPlanne.Direction = direction.Down;
                }
                else if (keyboardState.IsKeyDown(Keys.A))
                {
                    _playerPlanne.Direction = direction.Left;
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    _playerPlanne.Direction = direction.Right;
                }
                _playerPlanne.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Transparent);

            _spriteBatch.Begin();
            _background.Draw(_spriteBatch);
            if (!_gameStarted) { 
                _menu.Draw(_spriteBatch);
            }
            else {
                _playerPlanne.Draw(_spriteBatch);
            }
               
       
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
