using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.Content
{
    internal class PlayerBullet : Bullet
    {
        private Texture2D _texture;
        public PlayerBullet(Texture2D texture,Vector2 position, int width, int height, int health, int speed, direction direction, int attack) : base(position, width, height, health, speed, direction, attack)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: _texture, destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, 50, 50),color: Color.White
            );
        }

        
    }
}
