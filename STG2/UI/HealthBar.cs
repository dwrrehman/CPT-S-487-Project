using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class HealthBar
    {
        private Rectangle _backgroundRect;
        private Rectangle _foregroundRect;
        private Color _backgroundColor = Color.DarkGray;
        private Color _foregroundColor = Color.Green;
        private int _maxHealth;
        private int _currentHealth;

        // Initialize the health bar with player's max health and position
        public HealthBar(int maxHealth, Vector2 position, int width, int height)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;

            // Set up the background rectangle (empty bar)
            _backgroundRect = new Rectangle(
                (int)position.X,
                (int)position.Y,
                width,
                height);

            // Set up the foreground rectangle (filled bar)
            _foregroundRect = new Rectangle(
                (int)position.X,
                (int)position.Y,
                width,
                height);
        }

        // Update the health bar based on player's current health
        public void Update(int currentHealth)
        {
            _currentHealth = currentHealth;

            // Calculate the width of the health bar based on current health percentage
            float healthPercentage = (float)_currentHealth / _maxHealth;
            _foregroundRect.Width = (int)(_backgroundRect.Width * healthPercentage);

            // Change color based on health percentage
            if (healthPercentage > 0.6f)
            {
                _foregroundColor = Color.Green;
            }
            else if (healthPercentage > 0.3f)
            {
                _foregroundColor = Color.Yellow;
            }
            else
            {
                _foregroundColor = Color.Red;
            }
        }

        // Draw the health bar
        public void Draw(SpriteBatch spriteBatch)
        {
            // Create a 1x1 pixel texture for the health bar
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Draw background (empty bar)
            spriteBatch.Draw(pixel, _backgroundRect, _backgroundColor);

            // Draw foreground (filled bar)
            spriteBatch.Draw(pixel, _foregroundRect, _foregroundColor);
        }
    }
}