using Microsoft.Xna.Framework;
using STG2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class CollisionManager
    {
        private Player player;
        private List<Enemy> enemies;
        private List<Bullet> bullets;

        public CollisionManager(Player player, List<Enemy> enemies, List<Bullet> bullets)
        {
            this.player = player;
            this.enemies = enemies;
            this.bullets = bullets;
        }

        public void CheckCollisions()
        {
            // Check player bullets colliding with enemies
            CheckPlayerBulletsWithEnemies();

            // Check enemy bullets colliding with player
            CheckEnemyBulletsWithPlayer();

            // Check player colliding with enemies
            CheckPlayerWithEnemies();
        }

        private void CheckPlayerBulletsWithEnemies()
        {
            // Find player bullets (those that move upward)
            var playerBullets = bullets.Where(b => b.MovementStrategy is UpMovement).ToList();

            foreach (var bullet in playerBullets)
            {
                foreach (var enemy in enemies)
                {
                    // Check if the bullet is within the general vicinity of the enemy
                    if (Math.Abs(bullet.Position.X - enemy.Position.X) < 100 &&
                        Math.Abs(bullet.Position.Y - enemy.Position.Y) < 100)
                    {
                        var command = new PlayerBulletEnemyCollisionCommand(bullet, enemy);
                        command.Execute();
                    }
                }
            }
        }


        private void CheckEnemyBulletsWithPlayer()
        {
            // Find enemy bullet
            var enemyBullets = bullets.Where(b => b is Bullet && !(((Bullet)b).MovementStrategy is UpMovement)).ToList();

            foreach (var bullet in enemyBullets)
            {
                // Debug output to confirm enemy bullets are being checked
                Console.WriteLine($"Checking enemy bullet at position {bullet.Position.X}, {bullet.Position.Y}");

                var command = new EnemyBulletPlayerCollisionCommand((Bullet)bullet, player);
                command.Execute();
            }
        }

        private void CheckPlayerWithEnemies()
        {
            foreach (var enemy in enemies)
            {
                var command = new EntityCollisionCommand(player, enemy);
                command.Execute();
            }
        }
    }
}