using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class Bullet:Entity
    {

        private  Texture2D _texture;
        public Movement MovementStrategy { get; set; }
        public Color Tint { get; set; } = Color.White;

        public Bullet(Vector2 position, Texture2D texture, int health, int speed)
            : base(position, texture.Width, texture.Height, health, speed)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Check if this is a player bullet (moving up) or enemy bullet
            if (MovementStrategy is UpMovement)
            {
                // Player bullet (missile) - MAKE IT LARGER AND BRIGHTER
                spriteBatch.Draw(_texture, new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    40, // Increased from 30
                    50), // Increased from 40
                    Color.White);
            }
            else
            {
                // Enemy bullet (eb) - MAKE IT LARGER AND BRIGHTER
                spriteBatch.Draw(_texture, new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    30, // Increased from 20
                    50), // Increased from 40
                    Tint); // Changed to RED for better visibility
            }

            // Debug: Draw hitbox outline
#if DEBUG
            // Red rectangle for hitbox
            Texture2D debugTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            debugTexture.SetData(new[] { Color.Red });

            if (MovementStrategy is UpMovement)
            {
                // Player bullet hitbox
                spriteBatch.Draw(debugTexture, new Rectangle(
                    (int)(Position.X + 15),
                    (int)(Position.Y + 10),
                    10,
                    20),
                    Color.Red * 0.5f);
            }
            else
            {
                // Enemy bullet hitbox
                spriteBatch.Draw(debugTexture, new Rectangle(
                    (int)(Position.X + 15),
                    (int)(Position.Y + 10),
                    10,
                    30),
                    Color.Red * 0.5f);
            }
#endif
        }
        public void Update()
        {
            // Call the movement strategy to update position
            MovementStrategy.MoveStrategy(this);

            // Debug output
            Console.WriteLine($"Bullet at position: {Position.X}, {Position.Y}, Type: {(MovementStrategy is UpMovement ? "Player" : "Enemy")}");
        }

    }
}
