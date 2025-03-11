using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using STG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.IO;
namespace STG2
{
    


    internal static class InputManager
    {
        private static MouseState currentmouseState;
        private static KeyboardState currentKeyboardState;
        private static GamePadState currentGamepad;
        private static Vector2 Direction;
        private static KeyBindList _keyList = new KeyBindList();

        public static Vector2 currentDirection=>Direction;
        private static KeyBindList LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new KeyBindList();
            }
            string json = File.ReadAllText(filePath);
            KeyBindList list = JsonSerializer.Deserialize<KeyBindList>(json);
            return list;
        }
        public static void Update()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var gamePad = GamePad.GetState(PlayerIndex.One);
            Vector2 Kdirection = Vector2.Zero;
            _keyList = LoadFromJson("setting.json");
            string upStr = _keyList.GetKeyByName("Up");
            string downStr = _keyList.GetKeyByName("Down");
            string leftStr = _keyList.GetKeyByName("Left");
            string rightStr = _keyList.GetKeyByName("Right");
            string attackStr = _keyList.GetKeyByName("Attack");

            Keys upKey = (Keys)Enum.Parse(typeof(Keys), upStr);
            Keys downKey = (Keys)Enum.Parse(typeof(Keys), downStr);
            Keys leftKey = (Keys)Enum.Parse(typeof(Keys), leftStr);
            Keys rightKey = (Keys)Enum.Parse(typeof(Keys), rightStr);

            if (keyboardState.IsKeyDown(upKey)) Kdirection.Y--;
            if (keyboardState.IsKeyDown(downKey)) Kdirection.Y++;
            if (keyboardState.IsKeyDown(leftKey)) Kdirection.X--;
            if (keyboardState.IsKeyDown(rightKey)) Kdirection.X++;

            Vector2 Gdirection = Vector2.Zero;
            if (gamePad.IsConnected)
            {
                Gdirection = gamePad.ThumbSticks.Left;
                Gdirection.Y = -Gdirection.Y;
            }
            Direction = Gdirection + Kdirection;
        }


    }
}
