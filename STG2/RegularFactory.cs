using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace STG2
{
    internal class RegularFactory : EntityFactory
    {
        public  Boss CreatBoss(Vector2 Position, Texture2D Texture, int Health)
        {
            Boss boss = new Boss(
            position: Position,
            texture: Texture,
            health: Health,
            speed: 5

        );

            return boss;
        }

        public Bullet CreateBullet(Texture2D Texture,Entity entity, Movement movement)
        {
            Bullet bullet = new Bullet(
            position: new Vector2(entity.Position.X+(65/2f)-(40/2f), entity.Position.Y + (30/2f)-(40/2f)),
            texture: Texture,
            health: 0,
            speed: 8

        );

            bullet.MovementStrategy = movement;
            return bullet;
        }

        public Enemy CreateEnemy(Vector2 Position,Texture2D Texture,int Health,Movement movement)
        {
            Enemy enemy = new Enemy(
            position: Position,
            texture: Texture,
            health: Health,
            speed: 3

        );
            enemy.MovementStrategy = movement;
            return enemy;
        }
        

        public Player CreatePlayer(Texture2D texture)
        {
            Player player = new Player(
            position: new Vector2(200, 800),
            texture: texture,
            health: 5,
            speed: 5

        );
            
            return player;
        }
    }
}
