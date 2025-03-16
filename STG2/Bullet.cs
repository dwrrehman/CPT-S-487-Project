using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class Bullet:Entity
    {

        private  Texture2D _texture;
        public Movement MovementStrategy { get; set; }

        public Bullet(Vector2 position, Texture2D texture, int health, int speed)
            : base(position, texture.Width, texture.Height, health, speed)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, 40, 40), Color.White);
        
        }
        public void Update()
        {

            MovementStrategy.MoveStrategy(this);
            // Update bullets

        }

    }
}
