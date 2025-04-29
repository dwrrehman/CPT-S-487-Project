using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace STG2

    internal class BrokenThreadFire : Fire
    {
        private readonly Texture2D _tex;
        private readonly float _spacing;   // seconds between pearls
        private readonly float _angleStep;// rad step per pearl
        private readonly float _angularVel;    // pearl wobble rate
        private double _timer;
        private float _angle; // running launch angle

        private readonly EntityFactory _factory = new RegularFactory();

        public BrokenThreadFire(Texture2D tex, float spacing = 0.08f, float angleStep  = 0.20f,
                                float angularVel = 0.015f)
        {
            _tex  = tex;
            _spacing  = spacing;
            _angleStep  = angleStep;
            _angularVel = angularVel;
        }

        public void Fire(Entity shooter, GameTime gt, List<Bullet> bullets, double fireRate)
        {
            _timer += gt.ElapsedGameTime.TotalSeconds;
            if (_timer < _spacing) return;

            _timer -= _spacing;
            _angle += _angleStep;

            Vector2 dir = new Vector2(MathF.Cos(_angle), MathF.Sin(_angle));
            var move = new ThreadPearlMovement(dir, _angularVel);

            Bullet b = _factory.CreateBullet(_tex, shooter, move);
            b.Speed  = 3;             // slower speed
            b.Tint = Color.Cyan;
            bullets.Add(b);
        }
    }
}
