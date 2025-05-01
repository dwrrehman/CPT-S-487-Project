using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG2.States;

namespace STG2
{
    abstract class Entity
    {
        public Vector2 Position;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }


        // Add state pattern support
        public IEntityState CurrentState { get; set; }

        public Entity(Vector2 position, int width, int height, int health, int speed)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.Health = health;
            this.Speed = speed;

            // Initialize with normal state
            CurrentState = new NormalState();
        }

        // Method for state to call for drawing
        public virtual void DefaultDraw(SpriteBatch spriteBatch)
        {
            // Default drawing behavior - implement in subclasses
        }

        // Overload that allows for color changes
        public virtual void DefaultDraw(SpriteBatch spriteBatch, Color color)
        {
            // Default drawing with color - implement in subclasses
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void Update(GameTime gameTime)
        {
            // Update the current state
            CurrentState.Update(this, gameTime);

            // Check if state should change
            IEntityState newState = CurrentState.CheckTransition(this);
            if (newState != CurrentState)
            {
                CurrentState = newState;
            }
        }

        public virtual void TakeDamage(int damage)
        {
            // Delegate damage handling to current state
            CurrentState.TakeDamage(this, damage);
        }

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