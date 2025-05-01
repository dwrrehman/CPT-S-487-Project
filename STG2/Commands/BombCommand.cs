using STG2.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace STG2.Commands
{

    /// clears bullets and damages every enemy on screen, invincible for short period
    internal class BombCommand : ICommand
    {
        private List<Bullet> _bullets;
        private List<Enemy> _enemies;
        private Player _player;

        public BombCommand(Player player, List<Bullet> bullets, List<Enemy> enemies)
        {
            _player = player;
            _bullets = bullets;
            _enemies = enemies;
        }

        public void Execute()
        {
            _bullets.Clear();

            foreach (var e in _enemies)
            {
                e.TakeDamage(100);
            }

            // short invincibility
            _player.CurrentState = new States.InvincibleState();
        }
    }
}