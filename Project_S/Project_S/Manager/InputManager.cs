using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class InputManager
    {
        // KeyPressed라는 이벤트를 정의
        public event Action<Player, char> KeyPressed;
        private ICommand _command;

        private static InputManager Inst;
        public static InputManager GetInstance()
        {
            return Inst ??= new InputManager();
        }

        public void HandleInput(Player player)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char inputChar = keyInfo.KeyChar;
            KeyPressed?.Invoke(player, inputChar);
        }

        public bool Init()
        {
            return true;
        }

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void ExecuteCommand()
        {
            _command?.Execute();
        }
    }
}
