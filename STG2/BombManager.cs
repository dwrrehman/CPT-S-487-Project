using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace STG2
{
    internal class BombManager
    {
        private readonly GraphicsDevice _gd;
        private readonly Texture2D _white;
        private readonly SpriteFont _font;

        private float _flashTimer = 0f; // background flash
        private float _textTimer = 0f; // “BOMB!!” text

        public BombManager(GraphicsDevice gd, SpriteFont font)
        {
            _gd = gd;
            _font = font;

            _white = new Texture2D(_gd, 1, 1);
            _white.SetData(new[] { Color.White });
        }

        public void Trigger(float flashSecs = 0.2f, float textSecs = 0.5f)
        {
            _flashTimer = flashSecs;
            _textTimer = textSecs;
        }

        public void Update(GameTime gt)
        {
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;
            if (_flashTimer > 0) _flashTimer -= dt;
            if (_textTimer  > 0) _textTimer -= dt;
        }

        public void Draw(SpriteBatch sb)
        {
            // white flash
            if (_flashTimer > 0)
            {
                sb.Draw(_white,
                        new Rectangle(0, 0,
                                      _gd.Viewport.Width,
                                      _gd.Viewport.Height),
                        Color.White * 0.7f);
            }

            // “BOMB!!” text
            if (_textTimer > 0 && _font != null)
            {
                const string txt = "BOMB!!";
                Vector2 size   = _font.MeasureString(txt);
                Vector2 centre = new Vector2(
                                   (_gd.Viewport.Width  - size.X) * 0.5f,
                                   (_gd.Viewport.Height - size.Y) * 0.4f);

                sb.DrawString(_font, txt, centre, Color.Yellow);
            }
        }
    }
}
