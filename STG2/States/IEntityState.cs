using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.States
{
    internal interface IEntityState
    {
        // Handle updating the entity in this state
        void Update(Entity entity, GameTime gameTime);

        // Handle drawing the entity in this state
        void Draw(Entity entity, SpriteBatch spriteBatch);

        // Handle damage taken in this state
        void TakeDamage(Entity entity, int damage);

        // Check if the entity should transition to another state
        IEntityState CheckTransition(Entity entity);
    }
}