using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    interface BuffEffect { 
        void Apply(Player player); 
    }

    class ChangeAttack : BuffEffect
    {
        private  Fire _newAttack;
        public ChangeAttack(Fire newAttack) => _newAttack = newAttack;

        public void Apply(Player player)
        {
            player.FireStrategy = _newAttack;
        }
    }
    class AddBombs : BuffEffect
    {
        private int _count;
        public AddBombs(int count) => _count = count;

        public void Apply(Player player)
        {
            player.Bombs += _count;
        }
    }
}
