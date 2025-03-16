using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class Boss:Entity
    {
        private Texture2D _texture;


        public Boss(Vector2 position, Texture2D texture, int health, int speed)
            : base(position, texture.Width, texture.Height, health, speed)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, 50, 50), Color.White);
            //foreach (var bullet in _bullets)
            //{
            //    bullet.Draw(spriteBatch);
            //}
        }

        public void Update(GameTime gameTime)
        {


            // Fire bullets periodically
          

            // Update bullets

        }






    }
}

