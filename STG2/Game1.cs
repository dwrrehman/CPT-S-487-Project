using System;
using System.Collections.Generic;
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
        private Texture2D _enemyTexture;
        private Texture2D _enemyTextureGreen;
        public static Texture2D PixelTexture;

        private Background _background;
        private PlayerPlanne _playerPlanne;
        private Menu _menu;
        private bool _gameStarted = false;
        private List<Enemy> _enemies;
        private double _enemySpawnTimer;


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
            _texture3 = Content.Load<Texture2D>("button2");

            var font = Content.Load<SpriteFont>("Fonts");
            _menu = new Menu(_texture3, font, new Vector2(100, 400),"Start");
            _background = new Background(new Vector2(0, 0), 4, _texture);
            _playerPlanne = new PlayerPlanne(new Vector2(200, 750), _texture2, 10, 5, STG.direction.Up);
            _enemies = new List<Enemy>();

            _enemyTexture = new Texture2D(GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            _enemyTexture.SetData(data);

            _enemyTextureGreen = new Texture2D(GraphicsDevice, 50, 50);
            Color[] greenData = new Color[50 * 50];
            for (int i = 0; i < greenData.Length; ++i) greenData[i] = Color.Green;
            _enemyTextureGreen.SetData(greenData);

            PixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            PixelTexture.SetData(new[] { Color.White }); // Set a single white pixel



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

                _enemySpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (_enemySpawnTimer >= 2) // Spawn an enemy every 2 seconds
                {
                    int enemyXPosition = new Random().Next(50, 400);

                    // Alternate between spawning Red and Green enemies
                    if (_enemies.Count % 2 == 0)
                    {
                        // Red enemy moves **straight down**
                        _enemies.Add(new Enemy(new Vector2(enemyXPosition, 50), _enemyTexture, 3, 2, direction.Down, 1.5, EnemyType.Downward));
                    }
                    else
                    {
                        // Green enemy moves **side-to-side in mid-screen**
                        _enemies.Add(new Enemy(new Vector2(0, 300), _enemyTextureGreen, 3, 2, direction.Right, 1.5, EnemyType.SideToSide));
                    }

                    _enemySpawnTimer = 0;
                    //_enemies.Add(new Enemy(new Vector2(Random.Shared.Next(50, 400), 50), _texture3, 3, 2, direction.Down, 1.5));
                    //_enemySpawnTimer = 0;
                }

                // Update enemies
                foreach (var enemy in _enemies)
                {
                    enemy.Update(gameTime);
                }
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
                foreach (var enemy in _enemies)
                {
                    enemy.Draw(_spriteBatch);
                }
            }
               
       
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
