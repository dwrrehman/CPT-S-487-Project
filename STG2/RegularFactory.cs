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
        public Boss CreatBoss(Vector2 Position, Texture2D Texture,bool _isMidBoss, int Health, Movement movement,Fire fire)
        {
            Boss boss = new Boss(
                position: Position,
                texture: Texture,
                isMidBoss: _isMidBoss,
                health: Health,
                speed: 3 
            );
            boss.MovementStrategy = movement;
            boss.FireStrategy = fire;

            return boss;
        }

        public Bullet CreateBullet(Texture2D Texture, Entity entity, Movement movement)
            {
            // Calculate bullet position based on entity center
            Vector2 bulletPosition;

            if (movement is UpMovement || movement is UpLeftMovement || movement is UpRightMovement)
            {
                // Player bullets shoot from top center of player's hitbox
                bulletPosition = new Vector2(
                    entity.Position.X + 20, 
                    entity.Position.Y); 
            }
            else
            {
                // Enemy bullets shoot from bottom center of enemy
                float offsetX = (entity.Width - Texture.Width) / 2;

                bulletPosition = new Vector2(
                    entity.Position.X + offsetX, 
                    entity.Position.Y + entity.Height); 
            }

            Bullet bullet = new Bullet(
                position: bulletPosition,
                texture: Texture,
                health: 1,
                speed: 8
            );

            bullet.MovementStrategy = movement;

            // Add debug output
            Console.WriteLine($"Created bullet at {bulletPosition.X}, {bulletPosition.Y}, Moving: {(movement is UpMovement ? "Up" : "Down")}");

            return bullet;
        }

        public Enemy CreateEnemy(Vector2 Position,Texture2D Texture,int Health,Movement movement,Fire fire)
        {
            Enemy enemy = new Enemy(
            position: Position,
            texture: Texture,
            health: Health,
            speed: 3

        );
            enemy.MovementStrategy = movement;
            enemy.FireStrategy = fire;
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
