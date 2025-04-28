using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
namespace STG2
{
    


    internal static class InputManager
    {
 
        private static Vector2 Direction;
        private static KeyBindList _keyList = new KeyBindList();
        public static bool Attack { get;  set; }
        public static bool Bomb { get; private set; }
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

        public static bool IsFastMode { get; private set; }

        public static void Update()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var gamePad = GamePad.GetState(PlayerIndex.One);
            Vector2 Kdirection = Vector2.Zero;
            Vector2 Gdirection = Vector2.Zero;
            _keyList = LoadFromJson("setting.json");

            // Check if shift key is pressed
            IsFastMode = keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift);

            // For gamepad, use LeftTrigger as the "fast mode" button
            if (gamePad.IsConnected)
            {
                if (gamePad.Triggers.Left > 0.5f)
                    IsFastMode = true;
            }

            string upStr = _keyList.GetKeyByName("Up");
            string downStr = _keyList.GetKeyByName("Down");
            string leftStr = _keyList.GetKeyByName("Left");
            string rightStr = _keyList.GetKeyByName("Right");
            string attackStr = _keyList.GetKeyByName("Attack");
            bool ifattack = false;
            bool ifpadattack = false;

            Keys upKey = (Keys)Enum.Parse(typeof(Keys), upStr);
            Keys downKey = (Keys)Enum.Parse(typeof(Keys), downStr);
            Keys leftKey = (Keys)Enum.Parse(typeof(Keys), leftStr);
            Keys rightKey = (Keys)Enum.Parse(typeof(Keys), rightStr);
            Keys attackKey = (Keys)Enum.Parse(typeof(Keys), attackStr);

            Keys bombKey   = Enum.Parse<Keys>(_keyList.GetKeyByName("Bomb"));


            if (keyboardState.IsKeyDown(upKey)) Kdirection.Y--;
            if (keyboardState.IsKeyDown(downKey)) Kdirection.Y++;
            if (keyboardState.IsKeyDown(leftKey)) Kdirection.X--;
            if (keyboardState.IsKeyDown(rightKey)) Kdirection.X++;
            if (keyboardState.IsKeyDown(attackKey)) ifattack = true;

            bool bombK = keyboardState.IsKeyDown(bombKey);

            if (gamePad.IsConnected)
            {
                _keyList = LoadFromJson("setting.json");

                string upPadStr = _keyList.GetpadByName("Up");
                string downPadStr = _keyList.GetpadByName("Down");
                string leftPadStr = _keyList.GetpadByName("Left");
                string rightPadStr = _keyList.GetpadByName("Right");
                string attackPadStr = _keyList.GetpadByName("Attack");
                Buttons upBtn = (Buttons)Enum.Parse(typeof(Buttons), upPadStr);
                Buttons downBtn = (Buttons)Enum.Parse(typeof(Buttons), downPadStr);
                Buttons leftBtn = (Buttons)Enum.Parse(typeof(Buttons), leftPadStr);
                Buttons rightBtn = (Buttons)Enum.Parse(typeof(Buttons), rightPadStr);
                Buttons attackBtn = (Buttons)Enum.Parse(typeof(Buttons), attackPadStr);
                Buttons bombBtn  = Enum.Parse<Buttons>(_keyList.GetpadByName("Bomb"));


                if (gamePad.IsButtonDown(upBtn)) Gdirection.Y--;
                if (gamePad.IsButtonDown(downBtn)) Gdirection.Y++;
                if (gamePad.IsButtonDown(leftBtn)) Gdirection.X--;
                if (gamePad.IsButtonDown(rightBtn)) Gdirection.X++;
                if (gamePad.IsButtonDown(attackBtn)) ifpadattack = true;

                bombK |= gamePad.IsButtonDown(bombBtn);                 // NEW (bomb)

            }

            bool finalAttack = ifattack || ifpadattack;

            Attack = finalAttack;

            Direction = Gdirection + Kdirection;

            Bomb = bombK;

        }


    }
}
