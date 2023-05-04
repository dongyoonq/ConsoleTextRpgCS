using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class InputManager
    {
        // KeyPressed라는 이벤트를 정의
        public event Action<Player, char, GameState> PlayerKeyPressed;
        private ICommand _command;

        private static InputManager Inst;
        public static InputManager GetInstance()
        {
            return Inst ??= new InputManager();
        }

        public bool Init()
        {
            return true;
        }

        // 플레이어 입력 핸들러
        public void PlayerInputHandle(Player player, GameState gameState)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char inputChar = keyInfo.KeyChar;
            PlayerKeyPressed?.Invoke(player, inputChar, gameState);
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
