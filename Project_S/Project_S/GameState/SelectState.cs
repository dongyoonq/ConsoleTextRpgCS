using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class SelectState : GameState
    {
        Key keyDown;

        public GameState prevState;
        private int prevTop = 22;
        private int prevLeft = 20;
        private bool select = false;

        private static SelectState Inst;
        public static SelectState GetInstance()
        {
            return Inst ??= new SelectState();
        }

        private enum Key
        {
            Left, Right, Enter, Default
        }

        private enum State
        {
            Yes = 20, No = 30,
        }

        public override void Input()
        {
            Console.CursorLeft = prevLeft;
            Console.CursorTop = prevTop;

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    InputManager.GetInstance().SetCommand(new CursorMoveRightCommand());
                    keyDown = Key.Right;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    InputManager.GetInstance().SetCommand(new CursorMoveLeftCommand());
                    keyDown = Key.Left;
                    break;
                case ConsoleKey.Enter:
                    InputManager.GetInstance().SetCommand(null);
                    keyDown = Key.Enter;
                    break;
                default:
                    InputManager.GetInstance().SetCommand(null);
                    keyDown = Key.Default;
                    break;
            }

            InputManager.GetInstance().ExecuteCommand();
        }

        public override void Update()
        {
            switch (keyDown)
            {
                case Key.Right:
                    prevLeft = Console.GetCursorPosition().Left + 9;
                    break;
                case Key.Left:
                    prevLeft = Console.GetCursorPosition().Left - 9;
                    break;
                case Key.Enter:
                    NextState(prevLeft);
                    select = true;
                    return;
                default:
                    break;
            }

            if (Console.CursorLeft > (int)State.No)
            { prevLeft = (int)State.No; return; }

            if (Console.CursorLeft < (int)State.Yes)
            { prevLeft = (int)State.Yes; return; }
        }

        public override void Render()
        {
            if(select)
            {
                select = false;
                return;
            }

            Console.Clear();
            prevState.Render();
            DefaultRender();
        }

        public void DefaultRender()
        {
            Console.SetCursorPosition(prevLeft, prevTop);
            
            if (prevLeft == (int)State.Yes)
            {
                Console.SetCursorPosition((int)State.Yes, prevTop);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("▶ 네");
            }
            else
            {
                Console.SetCursorPosition((int)State.Yes, prevTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("네");
            }
            
            if (prevLeft == (int)State.No)
            {
                Console.SetCursorPosition((int)State.No - 3, prevTop);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("▶ 아니오");
            }
            else
            {
                Console.SetCursorPosition((int)State.No, prevTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("아니오");
            }
        }

        private void NextState(int prevLeft)
        {
            switch (prevLeft)
            {
                case (int)State.Yes:
                    InventoryUI.GetInstance().CompleteSelect = 1;
                    ReturnState();
                    break;
                case (int)State.No:
                    InventoryUI.GetInstance().CompleteSelect = -1;
                    ReturnState();
                    break;
                default:
                    break;
            }
        }

        public void ReturnState()
        {
            Console.Clear();
            Game.GetInstance().ChangeState(prevState);

            keyDown = Key.Default;
            prevTop = 22;
            prevLeft = 20;
        }
    }
}