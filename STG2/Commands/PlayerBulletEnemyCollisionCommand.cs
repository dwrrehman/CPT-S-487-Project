using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.Commands
{
    internal class PlayerBulletEnemyCollisionCommand : CollisionCommand
    {
        private Bullet bullet;
        private Enemy enemy;

        public PlayerBulletEnemyCollisionCommand(Bullet bullet, Enemy enemy)
            : base(bullet, enemy)
        {
            this.bullet = bullet;
            this.enemy = enemy;
        }

        protected override void HandleCollision()
        {
            // Player bullets do 10 damage (according to project-vision.txt)
            enemy.TakeDamage(10);

            // Remove the bullet after hit
            bullet.Health = 0;

            // Debug message
            Console.WriteLine("Player bullet hit enemy! Enemy health: " + enemy.Health);
        }
    }
}