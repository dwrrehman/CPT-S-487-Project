using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace STG2
{
    internal class Menu : Screen
    {

        private MouseState _currentMouse;
        private SpriteFont _font;
        private MouseState _previousMouse;
        private Texture2D _button;
        private KeyboardState _currentKeyboard;
        private KeyboardState _previousKeyboard;
        private GamePadState _currentGamePad;
        private GamePadState _previousGamePad;
        private Rectangle _start;
        private Rectangle _setting;
        private bool _usingGamepad = false;

        private int index = 0;
        Color startcolor, settingcolor;
        private Rectangle[] _padbind;
        public Menu(Game1 game1) : base(game1)
        {

        }
        public override void Show()
        {
            base.Show();
            _background = Game1.Content.Load<Texture2D>("jojo3");
            _button = Game1.Content.Load<Texture2D>("button2");
            _font = Game1.Content.Load<SpriteFont>("Fonts");
            _start = new Rectangle(100, 300, _button.Width, _button.Height);
            _setting = new Rectangle(100, 500, _button.Width, _button.Height);
            _padbind = new Rectangle[] { _start, _setting };
            var mouse = Mouse.GetState();
            _previousMouse = mouse;
            _currentMouse = mouse;


            var gp = GamePad.GetState(PlayerIndex.One);
            _previousGamePad = gp;
            _currentGamePad = gp;
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
                if (_currentGamePad.IsButtonDown(Buttons.DPadDown) && _previousGamePad.IsButtonUp(Buttons.DPadDown) || _currentGamePad.IsButtonDown(Buttons.LeftThumbstickDown) && _previousGamePad.IsButtonUp(Buttons.LeftThumbstickDown))
                {
                    index++;
                    if (index >= _padbind.Length)
                        index = 0;
                }
                else if (_currentGamePad.IsButtonDown(Buttons.DPadUp) && _previousGamePad.IsButtonUp(Buttons.DPadUp) || _currentGamePad.IsButtonDown(Buttons.LeftThumbstickUp) && _previousGamePad.IsButtonUp(Buttons.LeftThumbstickUp))
                {
                    index--;
                    if (index < 0)
                        index = _padbind.Length - 1;
                }
                if (index == 0)
                    startcolor = Color.Gray;
                else
                    startcolor = Color.White;
                if (index == 1)
                    settingcolor = Color.Gray;
                else
                    settingcolor = Color.White;

                if (_currentGamePad.IsButtonDown(Buttons.A) && _previousGamePad.IsButtonUp(Buttons.A))
                {
                    if (index == 0)
                    {
                        Game1.ScreenManager.ChangeScreen(new GamePlay(Game1));
                    }
                    else if (index == 1)
                    {
                        Game1.ScreenManager.ChangeScreen(new Setting(Game1));
                    }
                }

            }
            else
            {
                if (_start.Contains(mPosition))
                    startcolor = Color.Gray;
                else
                    startcolor = Color.White;

                if (_setting.Contains(mPosition))
                    settingcolor = Color.Gray;
                else
                    settingcolor = Color.White;



            }



            if (_start.Contains(_currentMouse.X, _currentMouse.Y))
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released
                   )
                {
                    Game1.ScreenManager.ChangeScreen(new GamePlay(Game1));
                }
            }

            if (_setting.Contains(_currentMouse.X, _currentMouse.Y))
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released
                   )
                {
                    Game1.ScreenManager.ChangeScreen(new Setting(Game1));

                }
            }
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_button, _start, startcolor);
            spriteBatch.Draw(_button, _setting, settingcolor);

            string startText = "Start";
            string settingText = "Setting";

            Vector2 startSize = _font.MeasureString(startText);
            Vector2 startPos = new Vector2(
                _start.X + (_start.Width - startSize.X) / 2,
                _start.Y + (_start.Height - startSize.Y) / 2
            );
            spriteBatch.DrawString(_font, startText, startPos, Color.White);

            Vector2 settingSize = _font.MeasureString(settingText);
            Vector2 settingPos = new Vector2(
                _setting.X + (_setting.Width - settingSize.X) / 2,
                _setting.Y + (_setting.Height - settingSize.Y) / 2
            );

            spriteBatch.DrawString(_font, settingText, settingPos, Color.White);
            spriteBatch.End();
        }
    }
}