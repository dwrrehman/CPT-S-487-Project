using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.States
{
    internal class DeadState : IEntityState
    {
        private float _fadeTimer = 0.5f; // Time for fade-out effect

        public void Draw(Entity entity, SpriteBatch spriteBatch)
        {
            // Fade out effect
            float alpha = _fadeTimer / 0.5f; // Calculate transparency
            entity.DefaultDraw(spriteBatch, Color.White * alpha);
        }

        public void TakeDamage(Entity entity, int damage)
        {
            // Dead entities can't take more damage
        }

        public void Update(Entity entity, GameTime gameTime)
        {
            // Decrease fade timer
            _fadeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public IEntityState CheckTransition(Entity entity)
        {
            // Stay in dead state
            return this;
        }
    }
}