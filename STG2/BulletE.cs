using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG;

namespace STG2
{
    class BulletE
    {
        public Vector2 Position;
        public int Speed;
        private Color _color;
        private direction _direction;

        public BulletE(Vector2 position, int speed, Color color, direction direction)
        {
            Position = position;
            Speed = speed;
            _color = color;
            _direction = direction;
        }

        public void Update()
        {
            if (_direction == direction.Up)
                Position.Y -= Speed;
            else if (_direction == direction.Down)
                Position.Y += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.PixelTexture, new Rectangle((int)Position.X, (int)Position.Y, 5, 10), _color);
        }
    }
}