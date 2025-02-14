using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG;
using STG2.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class PlayerPlanne : Character
    {
        private Texture2D _texture;

        public PlayerPlanne(Vector2 position, Texture2D texture, int health, int speed, direction direction)
            : base(position, texture.Width, texture.Height, health, speed, direction)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, 100, 100),
                color: Color.White
            );
        }

        public void Update()
        {
            this.Move();
        }

        public PlayerBullet Shoot(Texture2D texture)
        {
            int bulletX = (int)(this.Position.X + (40 / 2f));
            int bulletY = (int)(this.Position.Y);
            PlayerBullet playerBullet = new PlayerBullet(
                texture,
                new Vector2(bulletX, bulletY),
                texture.Width,
                texture.Height,
                0,
                15,
                direction.Up,
                5
            );
            return playerBullet;
        }
    }
}