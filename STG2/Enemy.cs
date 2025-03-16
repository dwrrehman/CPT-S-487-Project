using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace STG2
{
    class Enemy : Entity
    {
        private Texture2D _texture;
        public Movement MovementStrategy { get; set; }

        public Fire FireStrategy { get; set; }

        public Enemy(Vector2 position, Texture2D texture, int health, int speed)
            : base(position, texture.Width, texture.Height, health, speed)
        {
            _texture = texture;
        
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, 70, 70), Color.White);

        }

        public void Update(GameTime gameTime, List<Bullet> bullets,double FireRate)
        {


            MovementStrategy.MoveStrategy(this);
            FireStrategy.Fire(this,gameTime,bullets,FireRate);
            // Update bullets

        }






    }
}