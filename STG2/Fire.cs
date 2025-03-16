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
        private static Texture2D _bulletTexture;
        public RegularFire(Texture2D bulletTexture)
        {
            _bulletTexture = bulletTexture;
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
            }


        }
    }

    internal class EnemyFire : Fire
    {
        private static Texture2D _EnemyTexture;
        public EnemyFire(Texture2D bulletTexture)
        {
            _EnemyTexture = bulletTexture;
        }
        private double _timeSinceLastShot = 0.0;
        EntityFactory _entityFactory = new RegularFactory();

        void Fire.Fire(Entity entity, GameTime gameTime, List<Bullet> bullets,double Firerate)
        {
            _timeSinceLastShot += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeSinceLastShot >= Firerate)
            {
                _timeSinceLastShot = 0;
                Bullet bullet = _entityFactory.CreateBullet(_EnemyTexture, entity, new DownMovement());
                bullets.Add(bullet);
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
