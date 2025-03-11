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


    internal class Setting:MenuScreen
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
        Color upcolor,downcolor,leftcolor,rightcolor,attackcolor;
        private enum RebindAction { None, Up, Down, Left, Right, Attack }
        private RebindAction _waitingAction = RebindAction.None;
        public Setting(Game1 game1):base(game1) { }
        public override void Show()
        {
            base.Show();
            _background = Game1.Content.Load<Texture2D>("background3");
            _button = Game1.Content.Load<Texture2D>("button2");
            _font = Game1.Content.Load<SpriteFont>("Fonts");

            _up = new Rectangle(100, 200, _button.Width, _button.Height);
            _down = new Rectangle(100, 300, _button.Width, _button.Height);
            _left  = new Rectangle(100, 400, _button.Width, _button.Height);
            _right = new Rectangle(100, 500, _button.Width, _button.Height);
            _attack = new Rectangle(100, 600, _button.Width, _button.Height);
            _speed = 4;
            _keyList = LoadFromJson("setting.json");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            _previousKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();
            Point mPosition = _currentMouse.Position;

            if (_currentKeyboard.IsKeyDown(Keys.Escape))
            {
                Game1.ScreenManager.ChangeScreen(new Menu(Game1));

            }
            if (_up.Contains(mPosition))
            {
                upcolor = Color.Gray;
            }
            else
            {
                upcolor = Color.White;
            }
                
            if (_down.Contains(mPosition))
            {
                downcolor = Color.Gray;
            }
            else
            {
                downcolor = Color.White;
            }
               
            if (_left.Contains(mPosition))
            {
                leftcolor = Color.Gray;
            }
            else
            {
                leftcolor = Color.White;
            }
            if (_right.Contains(mPosition))
            {
                rightcolor = Color.Gray;
            }
            else
            {
                rightcolor = Color.White;
            }
            if (_attack.Contains(mPosition))
            {
                 attackcolor = Color.Gray;
            }
            else
            {
                attackcolor = Color.White;
            }
            if (_waitingAction == RebindAction.None)
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed)
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
                GamePadState pad = GamePad.GetState(PlayerIndex.One);

                foreach (Keys key in System.Enum.GetValues(typeof(Keys)))
                {
                    if (_currentKeyboard.IsKeyDown(key) && _previousKeyboard.IsKeyUp(key))
                    {
                        string actionName = _waitingAction.ToString();

                        SetKey(actionName, key.ToString());

                        SaveToJson(_keyList, "setting.json");

                        _waitingAction = RebindAction.None;
                        break;
                    }

                }
                //foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
                //{
                 //   if (pad.IsButtonDown(btn))
                  //  {
                  //      string actionName = _waitingAction.ToString();
                  //      SetKey(actionName, null,btn.ToString());
//
//                        SaveToJson(_keyList, "setting.json");
//                        _waitingAction = RebindAction.None;
//                        break;
 //                   }
   //             }
            }
        }
        private void SetKey(string name, string newKey)
        {
            foreach (var kb in _keyList.keyBinds)
            {
                if (kb.name == name)
                {
                    kb.key = newKey;
                    return;
                }
            }
            _keyList.keyBinds.Add(new KeyBind(name, newKey));
        }

     
        private KeyBindList LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new KeyBindList();
            }
            string json = File.ReadAllText(filePath);
            KeyBindList list = JsonSerializer.Deserialize<KeyBindList>(json);
            return list;
        }

        private void SaveToJson(KeyBindList list, string filePath)
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

            spriteBatch.DrawString(_font, upKey, new Vector2(_up.X + (_up.Width -upSize.X)/2,_up.Y+(_up.Height-upSize.Y)/2), Color.White);
            spriteBatch.DrawString(_font, downKey, new Vector2(_down.X + (_down.Width - downSize.X) / 2, _down.Y + (_down.Height - downSize.Y) / 2), Color.White);
            spriteBatch.DrawString(_font, leftKey, new Vector2(_left.X + (_left.Width - leftSize.X) / 2, _left.Y + (_left.Height - leftSize.Y) / 2), Color.White);
            spriteBatch.DrawString(_font, rightKey, new Vector2(_right.X + (_right.Width - rightSize.X) / 2, _right.Y + (_right.Height - rightSize.Y) / 2), Color.White);
            spriteBatch.DrawString(_font, attackKey, new Vector2(_attack.X + (_attack.Width - spaceSize.X) / 2, _attack.Y + (_attack.Height - spaceSize.Y) / 2), Color.White);



            spriteBatch.End();
        }
    }

}

