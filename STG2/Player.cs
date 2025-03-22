using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class Player : Entity
    {
        private Texture2D _texture;
        public Fire FireStrategy { get; set; }

        private const float SpeedMultiplier = 1.75f; // 75% faster in fast mode

        public Player(Vector2 position,Texture2D texture, int health, int speed) : base(position,texture.Width,texture.Height, health, speed)
        {
            _texture = texture;

        }
      
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: _texture, destinationRectangle: new Rectangle((int)this.Position.X, (int)this.Position.Y, 80,80), color: Color.White);

        }
        public void Update(GameTime gameTime, List<Bullet> bullets, double Firerate)
        {
            // Apply movement with speed adjustment based on fast mode
            Vector2 moveDirection = InputManager.currentDirection;

            // Only apply speed multiplier if we're actually moving
            if (moveDirection != Vector2.Zero)
            {
                // Create a temporary speed multiplier
                float speedModifier = InputManager.IsFastMode ? SpeedMultiplier : 1.0f;

                // Normalize direction vector if it's not zero (to prevent diagonal speed boost)
                if (moveDirection.Length() > 0)
                    moveDirection.Normalize();

                // Move with adjusted speed
                this.Move(moveDirection * speedModifier);
            }
            else
            {
                this.Move(moveDirection); // No movement
            }

            // Fire bullets if attack button is pressed
            if (InputManager.Attack)
            {
                FireStrategy.Fire(this, gameTime, bullets, Firerate);
            }
        }



    }
}
