using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.States
{
    internal class DamagedState : IEntityState
    {
        private float _stateTimer = 0.5f; // Half-second in damaged state

        public void Draw(Entity entity, SpriteBatch spriteBatch)
        {
            // Flash the entity by changing color
            entity.DefaultDraw(spriteBatch, Color.Red);
        }

        public void TakeDamage(Entity entity, int damage)
        {
            // Take damage in damaged state
            entity.Health -= damage;

            // Reset the damaged timer
            _stateTimer = 0.5f;

            // If health drops to or below 0, transition to dead state
            if (entity.Health <= 0)
            {
                entity.CurrentState = new DeadState();
            }
        }

        public void Update(Entity entity, GameTime gameTime)
        {
            // Decrease timer
            _stateTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public IEntityState CheckTransition(Entity entity)
        {
            // When timer expires, go back to normal state
            if (_stateTimer <= 0)
            {
                return new NormalState();
            }

            return this;
        }
    }
}