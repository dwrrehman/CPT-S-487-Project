using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class LoseScreen : Screen
    {

        private MouseState _currentMouse;
        private SpriteFont _font;
        private MouseState _previousMouse;
        private Texture2D _button;
        private KeyboardState _currentKeyboard;
        private KeyboardState _previousKeyboard;
        private GamePadState _currentGamePad;
        private GamePadState _previousGamePad;
        private Rectangle _back;
        private bool _usingGamepad = false;

        Color startcolor;
        public LoseScreen(Game1 game1) : base(game1)
        {

        }
        public override void Show()
        {
            base.Show();
            _background = Game1.Content.Load<Texture2D>("background3");
            _button = Game1.Content.Load<Texture2D>("button2");
            _font = Game1.Content.Load<SpriteFont>("Fonts");
            _back = new Rectangle(100, 500, _button.Width, _button.Height);


            _speed = 4;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            Point mPosition = _currentMouse.Position;
            _previousKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();
            _previousGamePad = _currentGamePad;
            _currentGamePad = GamePad.GetState(PlayerIndex.One);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (_currentKeyboard.IsKeyDown(key) && _previousKeyboard.IsKeyUp(key))
                {
                    _usingGamepad = false;
                    break;
                }
            }

            if (_currentGamePad.IsConnected)
            {
                foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
                {
                    if (_currentGamePad.IsButtonDown(btn) && _previousGamePad.IsButtonUp(btn))
                    {
                        _usingGamepad = true;
                        break;
                    }
                }
            }
            if (_usingGamepad)
            {

                startcolor = Color.Gray;
                if (_currentGamePad.IsButtonDown(Buttons.A) && _previousGamePad.IsButtonUp(Buttons.A))
                {
                    Game1.ScreenManager.ChangeScreen(new Menu(Game1));

                }

            }
            else
            {
                if (_back.Contains(mPosition))
                    startcolor = Color.Gray;
                else
                    startcolor = Color.White;




            }

            if (_back.Contains(_currentMouse.X, _currentMouse.Y))
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released
                   )
                {
                    Game1.ScreenManager.ChangeScreen(new Menu(Game1));
                }
            }

        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(_font, "GGGGGGGG!", new Vector2(200, 350), Color.White);

            spriteBatch.Draw(_button, _back, startcolor);

            string backText = "Back to Menu";

            Vector2 backSize = _font.MeasureString(backText);
            Vector2 startPos = new Vector2(
                _back.X + (_back.Width - backSize.X) / 2,
                _back.Y + (_back.Height - backSize.Y) / 2
            );
            spriteBatch.DrawString(_font, backText, startPos, Color.White);

            spriteBatch.End();
        }
    }
}
