using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class StartScene : Scene
    {
        private int prevTop = 6;
        bool Up = false;
        bool ArrowKeyDown = false;

        private enum State
        {
            CreatePlayer = 6,
            Back = 8,
            Exit = 10
        }

        private static StartScene Inst;
        public new static StartScene GetInstance()
        {
            sceneName = "StartScene";
            return Inst ??= new StartScene();
        }

        public override void Input()
        {
            Console.SetCursorPosition(0, prevTop);

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    ArrowKeyDown = true;
                    InputManager.GetInstance().SetCommand(new MoveUpCommand());
                    Up = true;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    ArrowKeyDown = true;
                    InputManager.GetInstance().SetCommand(new MoveDownCommand());
                    Up = false;
                    break;
                case ConsoleKey.Enter:
                    ArrowKeyDown = false;
                    InputManager.GetInstance().SetCommand(null);
                    break;
                default:
                    InputManager.GetInstance().SetCommand(null);
                    ArrowKeyDown = false;
                    break;
            }

            InputManager.GetInstance().ExecuteCommand();

            //InputManager.GetInstance().HandleInput(tester);
        }

        public override void Update()
        {
            if (ArrowKeyDown)
            {
                if (Up)
                    prevTop = Console.GetCursorPosition().Top - 1;
                else
                    prevTop = Console.GetCursorPosition().Top + 1;
            }

            if (prevTop > (int)State.Exit)
            { prevTop = (int)State.Exit; return; }

            if (prevTop < (int)State.CreatePlayer)
            { prevTop = (int)State.CreatePlayer; return; }

        }

        public override void Render()
        {
            Console.Clear();
            Show();
        }

        protected override void Show()
        {
            DefaultRender();
            //UiManager.GetInstance().Show();
        }

        private void DefaultRender()
        {
            Console.SetCursorPosition(17, 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("새로운 플레이어를 생성하세요 !");

            if (prevTop == (int)State.CreatePlayer)
            {
                Console.SetCursorPosition(25, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> 생성한다 ");
            }
            else
            {
                Console.SetCursorPosition(25, (int)State.CreatePlayer);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 생성한다 ");
            }

            if (prevTop == (int)State.Back)
            {
                Console.SetCursorPosition(25, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> 돌아간다 ");
            }
            else
            {
                Console.SetCursorPosition(25, (int)State.Back);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 돌아간다 ");
            }

            if (prevTop == (int)State.Exit)
            {
                Console.SetCursorPosition(28, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("> 종료");
            }
            else
            {
                Console.SetCursorPosition(28, (int)State.Exit);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("종료");
            }
        }
    }
}
