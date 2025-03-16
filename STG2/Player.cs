using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class Player : Entity
    {
        private Texture2D _texture;
        public Fire FireStrategy { get; set; }

        public Player(Vector2 position,Texture2D texture, int health, int speed) : base(position,texture.Width,texture.Height, health, speed)
        {
            _texture = texture;

        }
      
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: _texture, destinationRectangle: new Rectangle((int)this.Position.X, (int)this.Position.Y, 80,80), color: Color.White);

        }
        public void Update(GameTime gameTime, List<Bullet> bullets,double Firerate)
        {
            this.Move(InputManager.currentDirection);
            if (InputManager.Attack)
            {
                FireStrategy.Fire(this, gameTime, bullets,Firerate);

            }

        }

      

    }
}
