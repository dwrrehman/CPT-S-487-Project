using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal interface EntityFactory
    {
        Enemy CreateEnemy(Vector2 Position,Texture2D Texture, int Health,Movement movement,Fire fire);


        Player CreatePlayer(Texture2D Texture);

        Bullet CreateBullet(Texture2D Texture,Entity entity, Movement movement);

        Boss CreatBoss(Vector2 Position, Texture2D Texture, bool _isMidBoss, int Health,Movement movement, Fire fireke);
    }
}
