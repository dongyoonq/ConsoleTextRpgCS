using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class InputManager
    {
        // KeyPressed라는 이벤트를 정의
        public event Action<char> KeyPressed;

        private static InputManager Inst;
        public static InputManager GetInstance()
        {
            return Inst ??= new InputManager();
        }

        public void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char inputChar = keyInfo.KeyChar;
                KeyPressed?.Invoke(inputChar);
            }
        }

        public bool Init()
        {
            return true;
        }
    }
}
