using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace STG2
{

    internal class MultiFire : Fire
    {
        private readonly Fire[] _strategies;
        public MultiFire(params Fire[] strategies) => _strategies = strategies;

        public void Fire(Entity e, GameTime gt,
                         List<Bullet> bullets, double fireRate)
        {
            foreach (var f in _strategies)
                f.Fire(e, gt, bullets, fireRate);
        }
    }
}
