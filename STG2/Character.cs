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
    enum direction
    {
        Left,
        Right,
        Up,
        Down,
        UpLeft,    // New diagonal direction
        UpRight,   // New diagonal direction
        DownLeft,  // New diagonal direction
        DownRight, // New diagonal direction
        None       // New state for when no direction is active
    }

    abstract class Character
    {
        public Vector2 Position;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Health { get; set; }
        public int BaseSpeed { get; set; }  // Renamed from Speed to BaseSpeed
        public float SpeedMultiplier { get; set; } = 1.0f;  // New property for speed modes
        public direction Direction { get; set; }

        public Character(Vector2 position, int width, int height, int health, int speed, direction direction)
        {
            Position = position;
            Width = width;
            Height = height;
            Health = health;
            BaseSpeed = speed;
            Direction = direction;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public virtual void Move()
        {
            float currentSpeed = BaseSpeed * SpeedMultiplier;

            // For diagonal movement, reduce speed by 1/2
            // to maintain consistent velocity in all directions
            if (Direction == direction.UpLeft || Direction == direction.UpRight ||
                Direction == direction.DownLeft || Direction == direction.DownRight)
            {
                currentSpeed *= 0.707f;
            }

            switch (Direction)
            {
                case direction.Up:
                    Position.Y -= currentSpeed;
                    break;
                case direction.Down:
                    Position.Y += currentSpeed;
                    break;
                case direction.Left:
                    Position.X -= currentSpeed;
                    break;
                case direction.Right:
                    Position.X += currentSpeed;
                    break;
                case direction.UpLeft:
                    Position.Y -= currentSpeed;
                    Position.X -= currentSpeed;
                    break;
                case direction.UpRight:
                    Position.Y -= currentSpeed;
                    Position.X += currentSpeed;
                    break;
                case direction.DownLeft:
                    Position.Y += currentSpeed;
                    Position.X -= currentSpeed;
                    break;
                case direction.DownRight:
                    Position.Y += currentSpeed;
                    Position.X += currentSpeed;
                    break;
                case direction.None:
                    break;
            }

            // Boundary checking
            Position.X = MathHelper.Clamp(Position.X, 0, 400);
            Position.Y = MathHelper.Clamp(Position.Y, 0, 700);
        }

        // New method to set speed mode
        public virtual void SetSpeedMode(bool isFastMode)
        {
            SpeedMultiplier = isFastMode ? 2.0f : 1.0f; // 2x speed when in fast mode
        }
    }
}