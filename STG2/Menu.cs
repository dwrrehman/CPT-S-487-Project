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
    public class Menu{

        private MouseState _currentMouse;
        private SpriteFont _font;
        private Texture2D _texture;
        private MouseState _previousMouse;

        public Vector2 Position { get; set; }
    
        public bool IsPressed { get; private set; }


        public string Text { get; set; }

        public Menu(Texture2D texture, SpriteFont font, Vector2 poistion, string text)
        {
            _texture = texture;
            _font = font;
            Position = poistion;
            Text = text;
        }

        public void Update(GameTime gameTime){
           _previousMouse = _currentMouse;
           _currentMouse = Mouse.GetState();
           if (new Rectangle((int)Position.X,(int)Position.Y,_texture.Width,_texture.Height).Contains(_currentMouse.X, _currentMouse.Y))
            {
                if (_currentMouse.LeftButton == ButtonState.Pressed &&_previousMouse.LeftButton == ButtonState.Released)
                {
                    IsPressed = true;
                }        
            }
         }
       
    

         public void Draw(SpriteBatch spriteBatch){

            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height), Color.White);

            Vector2 textSize = _font.MeasureString(Text);
            Vector2 buttonCenter = new Vector2(
                Position.X + _texture.Width / 2f,
                Position.Y + _texture.Height / 2f
            );
            Vector2 textPosition = buttonCenter - textSize / 2f;

            spriteBatch.DrawString(_font, Text, textPosition, Color.White);
        }
    }
}