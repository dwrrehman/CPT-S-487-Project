﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using STG;
using STG2.Content;

namespace STG2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Texture2D _texture2;
        private Texture2D _texture3;
        private Texture2D _texture4;

        private Background _background;
        private PlayerPlanne _playerPlanne;
        private List<PlayerBullet> _bullets;
        private Menu _menu;
        private bool _gameStarted = false;
        private double start = 0;
        private double Cooldown = 0.3;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 850;
            _graphics.PreferredBackBufferWidth = 480;
            Content.RootDirectory = "Content";
            _bullets = new List<PlayerBullet>();

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
            _texture4 = Content.Load<Texture2D>("missile");
            var font = Content.Load<SpriteFont>("Fonts");
            _menu = new Menu(_texture3, font, new Vector2(100, 400),"Start");
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            double currentTime = gameTime.TotalGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!_gameStarted)
            {
                _menu.Update(gameTime);
                if (_menu.IsPressed)
                {
                    _gameStarted = true;
                }
            }
            else
            {
                var keyboardState = Keyboard.GetState();

                // Handle shooting
                if (keyboardState.IsKeyDown(Keys.Space) && currentTime > start + Cooldown)
                {
                    PlayerBullet bullet = _playerPlanne.Shoot(_texture4);
                    _bullets.Add(bullet);
                    start = currentTime;
                }

                // Handle movement
                direction newDirection = direction.None;
                bool isUpPressed = keyboardState.IsKeyDown(Keys.W);
                bool isDownPressed = keyboardState.IsKeyDown(Keys.S);
                bool isLeftPressed = keyboardState.IsKeyDown(Keys.A);
                bool isRightPressed = keyboardState.IsKeyDown(Keys.D);
                bool isShiftPressed = keyboardState.IsKeyDown(Keys.LeftShift) ||
                                     keyboardState.IsKeyDown(Keys.RightShift);

                // Set diagonal movement
                if (isUpPressed && isLeftPressed) newDirection = direction.UpLeft;
                else if (isUpPressed && isRightPressed) newDirection = direction.UpRight;
                else if (isDownPressed && isLeftPressed) newDirection = direction.DownLeft;
                else if (isDownPressed && isRightPressed) newDirection = direction.DownRight;
                // Set cardinal movement
                else if (isUpPressed) newDirection = direction.Up;
                else if (isDownPressed) newDirection = direction.Down;
                else if (isLeftPressed) newDirection = direction.Left;
                else if (isRightPressed) newDirection = direction.Right;

                _playerPlanne.Direction = newDirection;
                _playerPlanne.SetSpeedMode(isShiftPressed);

                if (newDirection != direction.None)
                {
                    _playerPlanne.Update();
                }

                // Update bullets
                for (int i = 0; i < _bullets.Count; i++)
                {
                    _bullets[i].Move();
                    if (_bullets[i].Position.Y == 0)
                    {
                        _bullets.RemoveAt(i);
                        i--;
                    }
                }

                base.Update(gameTime);
            }
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
                foreach (var bullet in _bullets)
                {
                    bullet.Draw(_spriteBatch);
                }
            }
               
       
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
