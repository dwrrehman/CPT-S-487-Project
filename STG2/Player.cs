using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STG2.States;
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

        private const float SpeedMultiplier = 1.75f; // 75% faster in fast mode

        public Player(Vector2 position, Texture2D texture, int health, int speed)
            : base(position, texture.Width, texture.Height, health, speed)
        {
            _texture = texture;
            // Player health
            Health = 50;
        }

        public override void DefaultDraw(SpriteBatch spriteBatch)
        {
            // Draw player
            spriteBatch.Draw(texture: _texture,
                destinationRectangle: new Rectangle(
                    (int)this.Position.X,
                    (int)this.Position.Y,
                    80, 
                    80),
                color: Color.White);
        }

        public override void DefaultDraw(SpriteBatch spriteBatch, Color color)
        {
            // Draw player with specified color
            spriteBatch.Draw(texture: _texture,
                destinationRectangle: new Rectangle(
                    (int)this.Position.X,
                    (int)this.Position.Y,
                    80, 
                    80),
                color: color);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            CurrentState.Draw(this, spriteBatch);

            // Debug: Draw hitbox outline
#if DEBUG
            // Red rectangle for hitbox
            Texture2D debugTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            debugTexture.SetData(new[] { Color.Red });

            spriteBatch.Draw(debugTexture, new Rectangle(
                (int)(Position.X + (80 - 60) / 2),
                (int)(Position.Y + (80 - 40) / 2),
                60,
                40),
                Color.Red * 0.5f);
#endif
        }

        public void Update(GameTime gameTime, List<Bullet> bullets, double Firerate)
        {
            // Call base update for state management
            base.Update(gameTime);

            // Apply movement with speed adjustment based on fast mode
            Vector2 moveDirection = InputManager.currentDirection;

            // Only apply speed multiplier if we're actually moving
            if (moveDirection != Vector2.Zero)
            {
                // Create a temporary speed multiplier
                float speedModifier = InputManager.IsFastMode ? SpeedMultiplier : 1.0f;

                // Normalize direction vector if it's not zero (to prevent diagonal speed boost)
                if (moveDirection.Length() > 0)
                    moveDirection.Normalize();

                // Move with adjusted speed
                this.Move(moveDirection * speedModifier);
            }
            else
            {
                this.Move(moveDirection); // No movement
            }

            // Fire bullets if attack button is pressed
            if (InputManager.Attack)
            {
                FireStrategy.Fire(this, gameTime, bullets, Firerate);
            }
        }
    }
}