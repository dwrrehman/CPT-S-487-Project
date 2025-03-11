using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    public abstract class MenuScreen
    {
        protected Game1 Game1;
        protected Texture2D _background;
        protected Vector2 _position;
        protected float _speed;

        public MenuScreen(Game1 game1)
        {
            Game1 = game1;
        }
        public virtual void Show() {
            _position = Vector2.Zero;        
        }
        public virtual void Update(GameTime gameTime) {
            _position.Y += _speed;
            if (_position.Y >= 850)
            {
                _position.Y -= 850;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            if (_background != null)
            {
                spriteBatch.Draw(_background, new Rectangle((int)_position.X, (int)_position.Y, 480, 850), Color.White);
                spriteBatch.Draw(_background, new Rectangle((int)_position.X, (int)_position.Y - 850, 480, 850), Color.White);
            }


        }
    }
}
