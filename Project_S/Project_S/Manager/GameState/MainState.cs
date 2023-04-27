using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class MainState : GameState
    {
        private int prevLeft = 10;
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
                default:
                    InputManager.GetInstance().SetCommand(null);
                    break;
            }

            InputManager.GetInstance().ExecuteCommand();
        }

        public override void Update()
        {
            // 메인 상태에서의 Update 처리

            if (Console.CursorTop > 4)
            { prevTop = 4; return; }

            if (Console.CursorTop < 2)
            { prevTop = 2; return; }

            prevLeft = Console.GetCursorPosition().Left;
            prevTop = Console.GetCursorPosition().Top;
        }

        public override void Render()
        {
            // 메인 상태에서의 Render 처리
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Main State\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (prevTop == 2)
            {
                Console.SetCursorPosition(0, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Mode");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else { Console.SetCursorPosition(0, 2); Console.WriteLine("Game Mode"); }
            if (prevTop == 3)
            {
                Console.SetCursorPosition(0, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Load Game");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else { Console.SetCursorPosition(0, 3); Console.WriteLine("Load Game"); }
            if (prevTop == 4)
            {
                Console.SetCursorPosition(0, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exit");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else { Console.SetCursorPosition(0, 4); Console.WriteLine("Exit"); }
            Console.SetCursorPosition(10, prevTop);
        }
    }
}
