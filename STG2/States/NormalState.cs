using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.States
{
    internal class NormalState : IEntityState
    {
        public void Draw(Entity entity, SpriteBatch spriteBatch)
        {
            // Regular drawing - delegate to entity's draw method
            entity.DefaultDraw(spriteBatch);
        }

        public void TakeDamage(Entity entity, int damage)
        {
            // In normal state, take full damage
            entity.Health -= damage;
            Console.WriteLine($"Entity took {damage} damage, health now: {entity.Health}");

            // If it's a player, transition to invincible state
            if (entity is Player && entity.Health > 0)
            {
                entity.CurrentState = new InvincibleState();
            }
            // If it's an enemy and still alive, transition to damaged state
            else if (entity is Enemy && entity.Health > 0)
            {
                entity.CurrentState = new DamagedState();
            }
            // If health drops to or below 0, transition to dead state
            else if (entity.Health <= 0)
            {
                entity.CurrentState = new DeadState();
            }
        }

        public void Update(Entity entity, GameTime gameTime)
        {
            // No special update behavior in normal state
        }

        public IEntityState CheckTransition(Entity entity)
        {
            // Stay in normal state
            return this;
        }
    }
}