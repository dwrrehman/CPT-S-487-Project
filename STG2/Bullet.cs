using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class Bullet : Character
    {
        public int attack {  get; set; }
        public Bullet(Vector2 position, int width, int height, int health, int speed, direction direction, int attack) : base(position, width, height, health, speed, direction)
        {
            this.attack = attack;
        }

        public override void Draw(SpriteBatch spriteBatch)
  {        }
    }
}
