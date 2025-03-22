using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace STG2
{
    

    abstract class Entity
    {

        public Vector2 Position;
        public int Width { get; set; }

        public int Height { get; set; }

        public int Health { get; set; }

        public int Speed { get; set; }


        public Entity(Vector2 position, int width, int height, int health, int speed)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.Health = health;
            this.Speed = speed;

        }

     

        public abstract void Draw(SpriteBatch spriteBatch);


        public virtual void Move(Vector2 dir)
        {
            // The direction vector now contains the speed multiplier
            this.Position += dir * this.Speed;

            // Boundary checks remain the same
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
                Position.Y = 0;
            }
            if (Position.Y >= 700)
            {
                Position.Y = 700;
            }
        }


    }
}
