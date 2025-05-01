using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2.Character
{
    class Buff : Entity
    {
        public Texture2D BuffTexture;
        private bool _picked = false;          

        public BuffEffect Effect { get; }
        public Movement MovementBuff{ get; set; }

        public bool IsPicked => _picked;

        public Buff(Vector2 position, Texture2D _texture, BuffEffect effect) : base(position, _texture.Width,_texture.Height,1,2)
        {
            BuffTexture = _texture;
            Effect = effect;
            MovementBuff = new DownMovement();
        }

        public override void Update(GameTime gameTime)
        {
            if (_picked) return;
            MovementBuff.MoveStrategy(this); 



        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BuffTexture, Position, Color.White);

        }

        internal void Pickup(Player player)
        {

            if (_picked) return;
            _picked = true;
            Effect.Apply(player);
        }
    }
}
