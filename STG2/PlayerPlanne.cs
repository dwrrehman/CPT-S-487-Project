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
    class PlayerPlanne : Character
    {
        private Texture2D _texture;

        public PlayerPlanne(Vector2 position,Texture2D texture, int health, int speed, direction direction) : base(position,texture.Width,texture.Height, health, speed,direction)
        {
            _texture = texture;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: _texture, destinationRectangle: new Rectangle((int)this.Position.X, (int)this.Position.Y, 100,100), color: Color.White);

        }
        public void Update()
        {
            this.Move();
        }

    }
}
