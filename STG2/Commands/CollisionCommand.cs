using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.Commands
{
    internal abstract class CollisionCommand : ICommand
    {
        protected Entity entity1;
        protected Entity entity2;

        public CollisionCommand(Entity entity1, Entity entity2)
        {
            this.entity1 = entity1;
            this.entity2 = entity2;
        }

        public virtual void Execute()
        {
            // Check if a collision occurred
            if (CheckCollision())
            {
                HandleCollision();
            }
        }

        // Simple rectangular collision detection
        protected bool CheckCollision()
        {
            // For player (plane: 237 x 120)
            Rectangle hitbox1;
            Rectangle hitbox2;

            if (entity1 is Player)
            {
                // Center the hitbox on the player
                hitbox1 = new Rectangle(
                    (int)(entity1.Position.X + (80 - 60) / 2), 
                    (int)(entity1.Position.Y + (80 - 40) / 2), 
                    60, 
                    40);
            }
            else if (entity1 is Bullet)
            {
                Bullet bullet = (Bullet)entity1;
                
                if (bullet.MovementStrategy is UpMovement)
                {
                    // Player bullet (missile: 444 x 685 - using much smaller hitbox)
                    hitbox1 = new Rectangle(
                        (int)(entity1.Position.X + 15),
                        (int)(entity1.Position.Y + 10),
                        10, 
                        20);
                }
                else
                {
                    // Enemy bullet (eb: 40 x 136)
                    hitbox1 = new Rectangle(
                        (int)(entity1.Position.X + 15),
                        (int)(entity1.Position.Y + 10),
                        10, 
                        30);
                }
            }
            else if (entity1 is Enemy)
            {
                // Enemy hitbox (generic)
                hitbox1 = new Rectangle(
                    (int)(entity1.Position.X + 15),
                    (int)(entity1.Position.Y + 15),
                    40, 
                    40);
            }
            else
            {
                // Generic entity
                hitbox1 = new Rectangle(
                    (int)entity1.Position.X,
                    (int)entity1.Position.Y,
                    entity1.Width,
                    entity1.Height);
            }

            // Do the same for entity2
            if (entity2 is Player)
            {
                hitbox2 = new Rectangle(
                    (int)(entity2.Position.X + (80 - 60) / 2),
                    (int)(entity2.Position.Y + (80 - 40) / 2),
                    60,
                    40);
            }
            else if (entity2 is Bullet)
            {
                Bullet bullet = (Bullet)entity2;
                // Check if this is a player bullet (moving upward)
                if (bullet.MovementStrategy is UpMovement)
                {
                    hitbox2 = new Rectangle(
                        (int)(entity2.Position.X + 15),
                        (int)(entity2.Position.Y + 10),
                        10,
                        20);
                }
                else
                {
                    hitbox2 = new Rectangle(
                        (int)(entity2.Position.X + 15),
                        (int)(entity2.Position.Y + 10),
                        10,
                        30);
                }
            }
            else if (entity2 is Enemy)
            {
                hitbox2 = new Rectangle(
                    (int)(entity2.Position.X + 15),
                    (int)(entity2.Position.Y + 15),
                    40,
                    40);
            }
            else
            {
                hitbox2 = new Rectangle(
                    (int)entity2.Position.X,
                    (int)entity2.Position.Y,
                    entity2.Width,
                    entity2.Height);
            }

            // Check if the hitboxes intersect
            return hitbox1.Intersects(hitbox2);
        }

        protected abstract void HandleCollision();
    }
}