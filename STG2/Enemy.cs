using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using STG;

namespace STG2
{
    class Enemy : Character
    {
        private Texture2D _texture;
        private List<BulletE> _bullets;
        private double _fireRate; // Controls how often the enemy fires
        private double _timeSinceLastShot;
        private EnemyType _enemyType;

        public Enemy(Vector2 position, Texture2D texture, int health, int speed, direction direction, double fireRate, EnemyType enemyType)
            : base(position, texture.Width, texture.Height, health, speed, direction)
        {
            _texture = texture;
            _bullets = new List<BulletE>();
            _fireRate = fireRate;
            _timeSinceLastShot = 0;
            _enemyType = enemyType;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, 50, 50), Color.White);
            foreach (var bullet in _bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            Move();

            // Fire bullets periodically
            _timeSinceLastShot += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeSinceLastShot >= _fireRate)
            {
                Fire();
                _timeSinceLastShot = 0;
            }

            // Update bullets
            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].Update();
                if (_bullets[i].Position.Y > 850) // Remove bullets that leave the screen
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }
        }


        public override void Move()
        {
            if (_enemyType == EnemyType.Downward)
            {
                // Move straight down
                Position.Y += this.Speed;
            }
            else if (_enemyType == EnemyType.SideToSide)
            {
                // Move side to side
                Position.X += this.Speed;
                if (Position.X >= 400 || Position.X <= 0)
                {
                    Speed = -Speed; // Reverse direction at screen edges
                }
            }
        }


        private void Fire()
        {
            _bullets.Add(new BulletE(new Vector2(Position.X + Width / 2, Position.Y + Height), 5, Color.Red, direction.Down));
        }
    }
}