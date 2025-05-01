using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG2.States;
using System.Collections.Generic;

namespace STG2
{
    class Enemy : Entity
    {
        private Texture2D _texture;
        public Movement MovementStrategy { get; set; }
        public Fire FireStrategy { get; set; }

        private List<IEnemyDeathObserver> _observers = new();
        public void RegisterObserver(IEnemyDeathObserver obs) => _observers.Add(obs);

        // Flag to track enemy type (A or B)
        private bool _isTypeA;

        public Enemy(Vector2 position, Texture2D texture, int health, int speed)
            : base(position, texture.Width, texture.Height, health, speed)
        {
            _texture = texture;

            // Determine if this is Type A or B based on texture (assuming Enemy1 is Type A)
            _isTypeA = texture.Name.Contains("Enemy1");

            // Set health based on enemy type:
            // Type A: 20 HP (2 player bullets)
            // Type B: 30 HP (3 player bullets)
            Health = _isTypeA ? 20 : 30;
        }
        public void Die()
        {
            NotifyDeath();
        }

        private void NotifyDeath()
        {
            foreach (var obs in _observers)
                obs.OnEnemyDied(this);
        }
        public override void DefaultDraw(SpriteBatch spriteBatch)
        {
            // Draw enemy
            spriteBatch.Draw(_texture, new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                90, 
                90),
                Color.White);
        }

        public override void DefaultDraw(SpriteBatch spriteBatch, Color color)
        {
            // Draw enemy with specified color
            spriteBatch.Draw(_texture, new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                90, 
                90),
                color);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Let the current state handle drawing
            CurrentState.Draw(this, spriteBatch);

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

        public void Update(GameTime gameTime, List<Bullet> bullets, double FireRate)
        {
            // Call base update for state management
            base.Update(gameTime);

            // Only move and fire if not in dead state
            if (!(CurrentState is DeadState))
            {
                MovementStrategy.MoveStrategy(this);
                FireStrategy.Fire(this, gameTime, bullets, FireRate);
            }
        }
    }
}