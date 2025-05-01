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

        private Screen _currentScreen;

        public void ChangeScreen(Screen screen)
        {

            _currentScreen = screen;
            _currentScreen.Show();
        }

        public void Update(GameTime gameTime)
        {
            _currentScreen.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(gameTime, spriteBatch);
        }
    }
}
