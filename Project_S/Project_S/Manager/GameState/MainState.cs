using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class MainState : GameState
    {
        private int prevTop = 6;
        bool Up = false;
        bool ArrowKeyDown = false;

        private static MainState Inst;
        public static MainState GetInstance()
        {
            return Inst ??= new MainState();
        }

        private enum State
        {
            GameMode = 6,
            LoadGame = 8,
            Exit = 10
        }

        public override void Input()
        {
            // 메인 상태에서의 Input 처리
            Console.SetCursorPosition(0, prevTop);

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W: case ConsoleKey.UpArrow:
                    InputManager.GetInstance().SetCommand(new MoveUpCommand());
                    ArrowKeyDown = true;
                    Up = true;
                    break;
                case ConsoleKey.S: case ConsoleKey.DownArrow:
                    InputManager.GetInstance().SetCommand(new MoveDownCommand());
                    ArrowKeyDown = true;
                    Up = false;
                    break;
                case ConsoleKey.Enter:
                    InputManager.GetInstance().SetCommand(null);
                    ArrowKeyDown = false;
                    NextState(prevTop);
                    break;
                default:
                    InputManager.GetInstance().SetCommand(null);
                    ArrowKeyDown = false;
                    break;
            }

            InputManager.GetInstance().ExecuteCommand();
        }

        public override void Update()
        {
            // 메인 상태에서의 Update 처리
            if(ArrowKeyDown)
            {
                if (Up)
                    prevTop = Console.GetCursorPosition().Top - 1;
                else
                    prevTop = Console.GetCursorPosition().Top + 1;
            }

            if (Console.CursorTop > (int)State.Exit)
            { prevTop = (int)State.Exit; return; }

            if (Console.CursorTop < (int)State.GameMode)
            { prevTop = (int)State.GameMode; return; }
        }

        public override void Render()
        {
            // 메인 상태에서의 Render 처리
            Console.Clear();

            DefaultRender();
        }

        private void DefaultRender()
        {
            Console.SetCursorPosition(17, 3);
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("★ Main Menu ★\n");

            if (prevTop == (int)State.GameMode)
            {
                Console.SetCursorPosition(19, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> Game Mode");
            }
            else
            {
                Console.SetCursorPosition(19, (int)State.GameMode);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Game Mode");
            }

            if (prevTop == (int)State.LoadGame)
            {
                Console.SetCursorPosition(19, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> Load Game");
            }
            else
            {
                Console.SetCursorPosition(19, (int)State.LoadGame);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Load Game");
            }

            if (prevTop == (int)State.Exit)
            {
                Console.SetCursorPosition(19, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> Exit");
            }
            else
            {
                Console.SetCursorPosition(19, (int)State.Exit);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Exit");
            }
        }

        private void NextState(int prevTop)
        {
            switch(prevTop)
            {
                case (int)State.GameMode:
                    GameModeState.GetInstance().SetScene("StartScene");
                    Console.Clear();
                    Core.GetInstance().ChangeState(GameModeState.GetInstance());
                    break;
                case (int)State.LoadGame:
                    Core.GetInstance().ChangeState(null);
                    break;
                case (int)State.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}