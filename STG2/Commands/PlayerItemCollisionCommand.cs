using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STG2.Character;
namespace STG2.Commands
{
    class PlayerItemCollisionCommand : ICommand
    {
        private Player _player;
        private Buff _buff;

        public PlayerItemCollisionCommand(Player player, Buff buff)
        {
            _player = player;
            _buff = buff;
        }

        public void Execute()
        {
            _buff.Pickup(_player);
        }
    }
}
