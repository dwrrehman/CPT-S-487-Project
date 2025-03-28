using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    public class ScreenManager
    {

        private Screen _MenuScreen;

        public void ChangeScreen(Screen menuScreen)
        {
          
            _MenuScreen = menuScreen;
            _MenuScreen.Show();
        }

        public void Update(GameTime gameTime)
        {
            _MenuScreen.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _MenuScreen.Draw(gameTime, spriteBatch);
        }
    }
}
