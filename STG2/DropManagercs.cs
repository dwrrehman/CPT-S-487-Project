using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using STG2.Character;
using STG2.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
  
        internal class DropManagercs : IEnemyDeathObserver
        {
            private  List<Buff> _buffList;
            private  Texture2D _bombIcon;
            private  Texture2D _spreadIcon;
        private Texture2D _bullet;

            public DropManagercs(List<Buff> buffList, ContentManager content)
            {
                _buffList = buffList;
                _bombIcon = content.Load<Texture2D>("4");       
                _spreadIcon = content.Load<Texture2D>("4");
                _bullet  = content.Load<Texture2D>("missile");
        }

            public void OnEnemyDied(Enemy enemy)
            {
                Buff buff = null;
                Buff buff2 = null;

            buff = new Buff(
                            position: enemy.Position,
                            _texture: _bombIcon,
                            effect: new AddBombs(1) 
                        );

          
             buff2 = new Buff(
                            position: enemy.Position,
                            _texture: _spreadIcon,
                            effect: new ChangeAttack(new ShotGun(_bullet))
                        );
              

             _buffList.Add(buff);
             _buffList.Add(buff2);
        }
        }
    }

