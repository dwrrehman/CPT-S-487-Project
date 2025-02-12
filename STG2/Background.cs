using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG;

namespace STG2
{
    class Background : Character
    {
        private Texture2D _texture;



        public Background(Vector2 position, int speed, Texture2D texture): base(position, texture.Width, texture.Height, 0, speed, direction.Down)
        {
            _texture = texture;
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {


            this.Position.Y += this.Speed;
            if(this.Position.Y >=850)
            {
                this.Position.Y -= 850;
            }
            spriteBatch.Draw(texture: _texture,destinationRectangle: new Rectangle((int)this.Position.X, (int)this.Position.Y, 480,850),color: Color.White);
            spriteBatch.Draw(texture: _texture, destinationRectangle: new Rectangle((int)this.Position.X, (int)this.Position.Y-850, 480, 850), color: Color.White);



        }


    }
}
