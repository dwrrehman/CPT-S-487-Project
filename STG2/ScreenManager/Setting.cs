using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace STG2
{


    internal class Setting:Screen
    {

        private Texture2D _button;
        private Rectangle _up;
        private Rectangle _down;
        private Rectangle _left;
        private Rectangle _right;
        private Rectangle _attack;
        private SpriteFont _font;
        private KeyBindList _keyList = new KeyBindList();
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        private KeyboardState _currentKeyboard;
        private KeyboardState _previousKeyboard;
        private GamePadState _currentGamePad;
        private GamePadState _previousGamePad;
        private bool _usingGamepad = false;
        Color upcolor,downcolor,leftcolor,rightcolor,attackcolor;
        private enum RebindAction { None, Up, Down, Left, Right, Attack }
        private RebindAction _waitingAction = RebindAction.None;
        private int index = 0;
        private Rectangle[] _padbind;
        public Setting(Game1 game1):base(game1) { }
        public override void Show()
        {
            base.Show();
            _background = Game1.Content.Load<Texture2D>("jojo3");
            _button = Game1.Content.Load<Texture2D>("button2");
            _font = Game1.Content.Load<SpriteFont>("Fonts");

            _up = new Rectangle(100, 50, _button.Width, _button.Height);
            _down = new Rectangle(100, 200, _button.Width, _button.Height);
            _left  = new Rectangle(100, 350, _button.Width, _button.Height);
            _right = new Rectangle(100, 500, _button.Width, _button.Height);
            _attack = new Rectangle(100, 650, _button.Width, _button.Height);
            _padbind = new Rectangle[] { _up,_down,_left,_down,_attack};
            _speed = 4;
            _keyList = LoadFromJson("setting.json");
            _currentMouse = Mouse.GetState();
            _previousMouse = _currentMouse;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            _previousKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();
            Point mPosition = _currentMouse.Position;
            _previousGamePad = _currentGamePad;
            _currentGamePad = GamePad.GetState(PlayerIndex.One);

            if (_currentKeyboard.IsKeyDown(Keys.Escape) || _currentGamePad.IsButtonDown(Buttons.Start))
            {
                Game1.ScreenManager.ChangeScreen(new Menu(Game1));

            }
         
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
            if (_waitingAction == RebindAction.None && _usingGamepad)
            {
                if (_currentGamePad.IsButtonDown(Buttons.DPadDown) && _previousGamePad.IsButtonUp(Buttons.DPadDown)|| _currentGamePad.IsButtonDown(Buttons.LeftThumbstickDown) && _previousGamePad.IsButtonUp(Buttons.LeftThumbstickDown))
                {
                    index++;
                    if (index >= _padbind.Length)
                        index = 0;
                }
                else if (_currentGamePad.IsButtonDown(Buttons.DPadUp) && _previousGamePad.IsButtonUp(Buttons.DPadUp)|| _currentGamePad.IsButtonDown(Buttons.LeftThumbstickUp) && _previousGamePad.IsButtonUp(Buttons.LeftThumbstickUp))
                {
                    index--;
                    if (index < 0)
                        index = _padbind.Length - 1;
                }

                if (_currentGamePad.IsButtonDown(Buttons.A) && _previousGamePad.IsButtonUp(Buttons.A))
                {
                    switch (index)
                    {
                        case 0: _waitingAction = RebindAction.Up; break;
                        case 1: _waitingAction = RebindAction.Down; break;
                        case 2: _waitingAction = RebindAction.Left; break;
                        case 3: _waitingAction = RebindAction.Right; break;
                        case 4: _waitingAction = RebindAction.Attack; break;
                    }
                }
            }


            if (_usingGamepad)
            {
                if (index == 0)
                    upcolor = Color.Gray;
                else
                    upcolor = Color.White;
                if (index == 1)
                    downcolor = Color.Gray;
                else
                    downcolor = Color.White;
                if (index == 2)
                    leftcolor = Color.Gray;
                else
                    leftcolor = Color.White;
                if (index == 3)
                    rightcolor = Color.Gray;
                else
                    rightcolor = Color.White;
                if (index == 4)
                    attackcolor = Color.Gray;
                else
                    attackcolor = Color.White;
            }
            else
            {
                if (_up.Contains(mPosition))
                    upcolor = Color.Gray;
                else
                    upcolor = Color.White;

                if (_down.Contains(mPosition))
                    downcolor = Color.Gray;
                else
                    downcolor = Color.White;

                if (_left.Contains(mPosition))
                    leftcolor = Color.Gray;
                else
                    leftcolor = Color.White;

                if (_right.Contains(mPosition))
                    rightcolor = Color.Gray;
                else
                    rightcolor = Color.White;

                if (_attack.Contains(mPosition))
                    attackcolor = Color.Gray;
                else
                    attackcolor = Color.White;
            }

            if (_waitingAction == RebindAction.None)
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released)
                {

                    if (_up.Contains(mPosition))
                        _waitingAction = RebindAction.Up;
                    else if (_down.Contains(mPosition))
                        _waitingAction = RebindAction.Down;
                    else if (_left.Contains(mPosition))
                        _waitingAction = RebindAction.Left;
                    else if (_right.Contains(mPosition))
                        _waitingAction = RebindAction.Right;
                    else if (_attack.Contains(mPosition))
                        _waitingAction = RebindAction.Attack;
                }
            }
            else
            {

                foreach (Keys key in System.Enum.GetValues(typeof(Keys)))
                {
                    if (_currentKeyboard.IsKeyDown(key) && _previousKeyboard.IsKeyUp(key))
                    {
                        string actionName = _waitingAction.ToString();

                        SetKey(actionName,key.ToString());


                        SaveToJson(_keyList, "setting.json");

                        _waitingAction = RebindAction.None;
                        break;
                    }

                }
                foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
                {
                    if (_currentGamePad.IsButtonDown(btn) && _previousGamePad.IsButtonUp(btn) && btn !=Buttons.A)
                    {
                        string actionName = _waitingAction.ToString();
                        SetGamepad(actionName, btn.ToString());

                        SaveToJson(_keyList, "setting.json");
                        _waitingAction = RebindAction.None;
                        break;
                    }
                }
            }
        }
        private void SetKey(string actionName, string newKey)
        {
            foreach (var bind in _keyList.keyBinds)
            {
                if (bind.name == actionName)
                {
                    bind.key = newKey;
                    break; 
                }
            }


        }

        private void SetGamepad(string actionName, string newGamepad)
        {
            foreach (var bind in _keyList.keyBinds)
            {
                if (bind.name == actionName)
                {
                    bind.gamepad = newGamepad;
                    break;
                }
            }

        }



        private static KeyBindList LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new KeyBindList();
            }
            string json = File.ReadAllText(filePath);
            KeyBindList list = JsonSerializer.Deserialize<KeyBindList>(json);
            return list;
        }

        private static void SaveToJson(KeyBindList list, string filePath)
        {
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }




        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_button, _up, upcolor);
            spriteBatch.Draw(_button, _down, downcolor);
            spriteBatch.Draw(_button, _left, leftcolor);
            spriteBatch.Draw(_button, _right, rightcolor);
            spriteBatch.Draw(_button, _attack, attackcolor);

            if (_usingGamepad)
            {
                string upKey = _keyList.GetpadByName("Up");
                string downKey = _keyList.GetpadByName("Down");
                string leftKey = _keyList.GetpadByName("Left");
                string rightKey = _keyList.GetpadByName("Right");
                string attackKey = _keyList.GetpadByName("Attack");
                Vector2 upSize = _font.MeasureString(upKey);
                Vector2 downSize = _font.MeasureString(downKey);
                Vector2 leftSize = _font.MeasureString(leftKey);
                Vector2 rightSize = _font.MeasureString(rightKey);
                Vector2 spaceSize = _font.MeasureString(attackKey);

                spriteBatch.DrawString(_font, upKey, new Vector2(_up.X + (_up.Width - upSize.X) / 2, _up.Y + (_up.Height - upSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, downKey, new Vector2(_down.X + (_down.Width - downSize.X) / 2, _down.Y + (_down.Height - downSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, leftKey, new Vector2(_left.X + (_left.Width - leftSize.X) / 2, _left.Y + (_left.Height - leftSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, rightKey, new Vector2(_right.X + (_right.Width - rightSize.X) / 2, _right.Y + (_right.Height - rightSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, attackKey, new Vector2(_attack.X + (_attack.Width - spaceSize.X) / 2, _attack.Y + (_attack.Height - spaceSize.Y) / 2), Color.White);
            }
            else
            {
                string upKey = _keyList.GetKeyByName("Up");
                string downKey = _keyList.GetKeyByName("Down");
                string leftKey = _keyList.GetKeyByName("Left");
                string rightKey = _keyList.GetKeyByName("Right");
                string attackKey = _keyList.GetKeyByName("Attack");
                Vector2 upSize = _font.MeasureString(upKey);
                Vector2 downSize = _font.MeasureString(downKey);
                Vector2 leftSize = _font.MeasureString(leftKey);
                Vector2 rightSize = _font.MeasureString(rightKey);
                Vector2 spaceSize = _font.MeasureString(attackKey);

                spriteBatch.DrawString(_font, upKey, new Vector2(_up.X + (_up.Width - upSize.X) / 2, _up.Y + (_up.Height - upSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, downKey, new Vector2(_down.X + (_down.Width - downSize.X) / 2, _down.Y + (_down.Height - downSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, leftKey, new Vector2(_left.X + (_left.Width - leftSize.X) / 2, _left.Y + (_left.Height - leftSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, rightKey, new Vector2(_right.X + (_right.Width - rightSize.X) / 2, _right.Y + (_right.Height - rightSize.Y) / 2), Color.White);
                spriteBatch.DrawString(_font, attackKey, new Vector2(_attack.X + (_attack.Width - spaceSize.X) / 2, _attack.Y + (_attack.Height - spaceSize.Y) / 2), Color.White);

            }



            spriteBatch.End();
        }
    }

}

