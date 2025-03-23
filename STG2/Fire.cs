using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal interface Fire
    {
        void Fire(Entity entity, GameTime gameTime, List<Bullet> bullets, double Firerate);
    }

    internal class RegularFire : Fire
    {
        private Texture2D _bulletTexture;  

        public RegularFire(Texture2D bulletTexture)
        {
            _bulletTexture = bulletTexture;
            // Debug output to confirm texture loading
            Console.WriteLine($"Player bullet texture loaded: {_bulletTexture != null}");
        }
        private double _timeSinceLastShot = 0.0;
        EntityFactory _entityFactory = new RegularFactory();

        void Fire.Fire(Entity entity, GameTime gameTime, List<Bullet> bullets, double Firerate)
        {
            _timeSinceLastShot += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeSinceLastShot >= Firerate)
            {
                _timeSinceLastShot = 0;
                Bullet bullet = _entityFactory.CreateBullet(_bulletTexture, entity, new UpMovement());
                bullets.Add(bullet);

                // Debug message
                Console.WriteLine($"Player fired bullet. Current bullets: {bullets.Count}");
            }
        }
    }


    internal class EnemyFire : Fire
    {
        private Texture2D _EnemyTexture; 

        public EnemyFire(Texture2D bulletTexture)
        {
            _EnemyTexture = bulletTexture;
            // Debug output to confirm texture loading
            Console.WriteLine($"Enemy bullet texture loaded: {_EnemyTexture != null}");
        }
        private double _timeSinceLastShot = 0.0;
        EntityFactory _entityFactory = new RegularFactory();

        void Fire.Fire(Entity entity, GameTime gameTime, List<Bullet> bullets, double Firerate)
        {
            _timeSinceLastShot += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeSinceLastShot >= Firerate)
            {
                _timeSinceLastShot = 0;
                Bullet bullet = _entityFactory.CreateBullet(_EnemyTexture, entity, new DownMovement());
                bullets.Add(bullet);

                // Debug message
                Console.WriteLine($"Enemy fired bullet. Current bullets: {bullets.Count}");
            }
        }
    }

    internal class ShotGun : Fire
    {

        public void Fire(Entity entity, GameTime gameTime, List<Bullet> bullets, double Firerate)
        {
            throw new NotImplementedException();
        }
    }
}
