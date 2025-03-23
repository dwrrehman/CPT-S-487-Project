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
            // Draw enemy
            spriteBatch.Draw(_texture, new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                70, 
                70),
                Color.White);

            // Debug: Draw hitbox outline
#if DEBUG
            // Red rectangle for hitbox
            Texture2D debugTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            debugTexture.SetData(new[] { Color.Red });

            spriteBatch.Draw(debugTexture, new Rectangle(
                (int)(Position.X + 15),
                (int)(Position.Y + 15),
                40,
                40),
                Color.Red * 0.5f);
#endif
        }

        public void Update(GameTime gameTime, List<Bullet> bullets,double FireRate)
        {


            MovementStrategy.MoveStrategy(this);
            FireStrategy.Fire(this,gameTime,bullets,FireRate);
            // Update bullets

        }






    }
}