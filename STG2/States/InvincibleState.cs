using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.States
{
    internal class InvincibleState : IEntityState
    {
        private float _invincibilityTimer = 2.0f; // 2 seconds of invincibility
        private float _flashTimer = 0.1f; // For blinking effect
        private bool _visible = true;

        public void Draw(Entity entity, SpriteBatch spriteBatch)
        {
            // Make the entity blink while invincible
            if (_visible)
            {
                entity.DefaultDraw(spriteBatch, Color.White * 0.7f); // Semi-transparent
            }
        }

        public void TakeDamage(Entity entity, int damage)
        {
            // No damage taken in invincible state
            Console.WriteLine("Entity is invincible! No damage taken.");
        }

        public void Update(Entity entity, GameTime gameTime)
        {
            // Decrease invincibility timer
            _invincibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Handle flashing effect
            _flashTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_flashTimer <= 0)
            {
                _visible = !_visible; // Toggle visibility
                _flashTimer = 0.1f; // Reset flash timer
            }
        }

        public IEntityState CheckTransition(Entity entity)
        {
            // When invincibility expires, go back to normal state
            if (_invincibilityTimer <= 0)
            {
                return new NormalState();
            }

            return this;
        }
    }
}