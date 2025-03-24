using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.Commands
{
    internal class EntityCollisionCommand : CollisionCommand
    {
        public EntityCollisionCommand(Entity entity1, Entity entity2)
            : base(entity1, entity2)
        {
        }

        protected override void HandleCollision()
        {
            // If entity1 is player and entity2 is enemy, damage player
            if (entity1 is Player && entity2 is Enemy)
            {
                entity1.TakeDamage(10);
               // Console.WriteLine("Player collided with enemy! Player health: " + entity1.Health);
            }
            // If entity1 is enemy and entity2 is player, damage player
            else if (entity1 is Enemy && entity2 is Player)
            {
                entity2.TakeDamage(10);
                //Console.WriteLine("Player collided with enemy! Player health: " + entity2.Health);
            }
        }
    }
}