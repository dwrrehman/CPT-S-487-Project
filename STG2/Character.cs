using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace STG
{
    enum direction { Left, Right, Up, Down }
    enum EnemyType { Downward, SideToSide }

    abstract class Character
    {

        public Vector2 Position;
        public int Width { get; set; }

        public int Height { get; set; }

        public int Health { get; set; }

        public int Speed { get; set; }

        public direction Direction { get; set; }

        public Character(Vector2 position, int width, int height, int health, int speed, direction direction)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.Health = health;
            this.Speed = speed;
            this.Direction = direction;

        }

     

        public abstract void Draw(SpriteBatch spriteBatch);
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        }

        public virtual void Move()
        {
            switch (this.Direction)
            {
                case direction.Up:
                    Position.Y -= this.Speed;
                    break;

                case direction.Down:
                    Position.Y += this.Speed;
                    break ;

                case direction.Left:
                    Position.X -= this.Speed;
                    break;

                case direction.Right:
                     Position.X += this.Speed;
                    break ;
            }
            if (Position.X < 0)
            {
                Position.X = 0;
            }
            if (Position.X >= 400)
            {
                Position.X = 400;
            }
            if (Position.Y < 0)
            {
                Position.Y= 0;
            }
            if (Position.Y >= 700){
                Position.Y = 700;
            }
        }


    }
}
