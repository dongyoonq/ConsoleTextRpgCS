using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class MainState : GameState
    {
        private static MainState Inst;
        public static MainState GetInstance()
        {
            return Inst ??= new MainState();
        }

        private enum State
        {
            GameMode = 2,
            LoadGame = 3,
            Exit = 4
        }

        private int prevTop = 2;

        public override void Input()
        {
            // 메인 상태에서의 Input 처리
            
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W: case ConsoleKey.UpArrow:
                    InputManager.GetInstance().SetCommand(new MoveUpCommand());
                    break;
                case ConsoleKey.S: case ConsoleKey.DownArrow:
                    InputManager.GetInstance().SetCommand(new MoveDownCommand());
                    break;
                case ConsoleKey.Enter:
                    InputManager.GetInstance().SetCommand(null);
                    NextState(prevTop);
                    break;
                default:
                    InputManager.GetInstance().SetCommand(null);
                    break;
            }

            InputManager.GetInstance().ExecuteCommand();
        }

        public override void Update()
        {
            // 메인 상태에서의 Update 처리

            if (Console.CursorTop > (int)State.Exit)
            { prevTop = (int)State.Exit; return; }

            if (Console.CursorTop < (int)State.GameMode)
            { prevTop = (int)State.GameMode; return; }

            prevTop = Console.GetCursorPosition().Top;
        }

        public override void Render()
        {
            // 메인 상태에서의 Render 처리
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Cyan; 
            
            Console.WriteLine("[ Main State ]\n");

            DefaultRender(prevTop);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(9, prevTop);
        }

        private void DefaultRender(int prevTop)
        {

            if (prevTop == (int)State.GameMode)
            {
                Console.SetCursorPosition(0, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> Game Mode");
            }
            else
            {
                Console.SetCursorPosition(0, (int)State.GameMode);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Game Mode");
            }

            if (prevTop == (int)State.LoadGame)
            {
                Console.SetCursorPosition(0, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> Load Game");
            }
            else
            {
                Console.SetCursorPosition(0, (int)State.LoadGame);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Load Game");
            }

            if (prevTop == (int)State.Exit)
            {
                Console.SetCursorPosition(0, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> Exit");
            }
            else
            {
                Console.SetCursorPosition(0, (int)State.Exit);
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