using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.Commands
{
    internal class EnemyBulletPlayerCollisionCommand : CollisionCommand
    {
        private Bullet bullet;
        private Player player;

        public EnemyBulletPlayerCollisionCommand(Bullet bullet, Player player)
            : base(bullet, player)
        {
            this.bullet = bullet;
            this.player = player;
        }

        protected override void HandleCollision()
        {
            // Enemy bullets do 10 damage to player
            player.TakeDamage(10);

            // Remove the bullet after hit
            bullet.Health = 0;

            // Debug message
            //Console.WriteLine("Enemy bullet hit player! Player health: " + player.Health);
        }
    }
}