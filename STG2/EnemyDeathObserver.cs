using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    interface IEnemyDeathObserver
    {
        void OnEnemyDied(Enemy enemy);
    }
}
