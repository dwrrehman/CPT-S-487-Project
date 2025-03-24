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
    internal class Boss : Enemy
    {
        private Texture2D _texture;
        private bool _isMidBoss;

        public Boss(Vector2 position, Texture2D texture, int health, int speed)
            : base(position, texture, health, speed)
        {
            _texture = texture;

            // Determine if this is mid boss or final boss based on the texture
            _isMidBoss = texture.Name.Contains("Enemy1");

            // Set health based on boss type:
            // Mid Boss: 150 HP (15 player bullets)
            // Final Boss: 300 HP (30 player bullets)
            Health = _isMidBoss ? 150 : 300;
        }
        public bool IsMidBoss => _isMidBoss;

        public override void DefaultDraw(SpriteBatch spriteBatch)
        {
            // Draw boss at a larger size to distinguish from regular enemies
            spriteBatch.Draw(_texture, new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                _isMidBoss ? 100 : 150, 
                _isMidBoss ? 100 : 150),
                Color.White);
        }

        public override void DefaultDraw(SpriteBatch spriteBatch, Color color)
        {
            // Draw boss with specified color
            spriteBatch.Draw(_texture, new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                _isMidBoss ? 100 : 150,
                _isMidBoss ? 100 : 150),
                color);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Let the current state handle drawing
            CurrentState.Draw(this, spriteBatch);

            // Debug hitbox
#if DEBUG
            Texture2D debugTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            debugTexture.SetData(new[] { Color.Red });

            spriteBatch.Draw(debugTexture, new Rectangle(
                (int)(Position.X + 20),
                (int)(Position.Y + 20),
                _isMidBoss ? 60 : 110,
                _isMidBoss ? 60 : 110),
                Color.Red * 0.5f);
#endif
        }
    }
}