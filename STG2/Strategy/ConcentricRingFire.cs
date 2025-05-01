using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace STG2.Strategy
{
    /// fires 360° ring every ringInterval seconds.
    internal class ConcentricRingFire : Fire
    {
        private readonly Texture2D _tex;
        private readonly int _bulletsPerRing;
        private readonly float _ringInterval;
        private readonly float _bulletSpeed;
        private double _timer;

        private readonly EntityFactory _factory = new RegularFactory();

        public ConcentricRingFire(Texture2D tex, int bulletsPerRing = 32, float ringInterval = 1.5f, float bulletSpeed = 3f)
        {
            _tex = tex;
            _bulletsPerRing = bulletsPerRing;
            _ringInterval = ringInterval;
            _bulletSpeed = bulletSpeed;
        }

        public void Fire(Entity shooter, GameTime gt, List<Bullet> bullets, double fireRate)
        {
            _timer += gt.ElapsedGameTime.TotalSeconds;
            if (_timer < _ringInterval) return;
            _timer -= _ringInterval;

            float step = MathF.Tau / _bulletsPerRing;
            for (int i = 0; i < _bulletsPerRing; i++)
            {
                float angle = i * step;
                Vector2 dir = new Vector2(MathF.Cos(angle), MathF.Sin(angle));

                var move = new RadialOutMovement(dir);
                Bullet b = _factory.CreateBullet(_tex, shooter, move);
                b.Speed = (int)_bulletSpeed;
                b.Tint = Color.Wheat;
                bullets.Add(b);
            }
        }
    }
}