using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using STG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace STG2
{
    internal class Menu:MenuScreen{

        private MouseState _currentMouse;
        private SpriteFont _font;
        private MouseState _previousMouse;
        private Texture2D _button;

        private Rectangle _start;
        private Rectangle _setting;

        public Menu(Game1 game1) : base(game1)
        {

        }
        public override void Show()
        {
            base.Show();
            _background = Game1.Content.Load<Texture2D>("background3");
            _button = Game1.Content.Load<Texture2D>("button2");
            _font = Game1.Content.Load<SpriteFont>("Fonts");
            _start = new Rectangle(100, 300, _button.Width,_button.Height);
            _setting = new Rectangle(100, 500, _button.Width, _button.Height);
            _speed = 4;
        }
        public override void Update(GameTime gameTime){
            base.Update(gameTime);

            _previousMouse = _currentMouse;
           _currentMouse = Mouse.GetState();
            if (_start.Contains(_currentMouse.X, _currentMouse.Y))
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed
                   )
                {
                    Game1.ScreenManager.ChangeScreen(new GamePlay(Game1));
                }
            }

            if (_setting.Contains(_currentMouse.X, _currentMouse.Y))
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed
                   )
                {
                    Game1.ScreenManager.ChangeScreen(new Setting(Game1));

                }
            }
        }
       
    

         public override void Draw(GameTime gameTime,SpriteBatch spriteBatch){

            spriteBatch.Begin();
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_button, _start, Color.White);
            spriteBatch.Draw(_button, _setting, Color.White);

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