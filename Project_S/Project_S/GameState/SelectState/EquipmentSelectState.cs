using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class EquipmentSelectState : SelectState
    {
        Key keyDown;

        private int prevTop = 27;
        private int prevLeft = 35;
        private bool select = false;

        private static EquipmentSelectState Inst;
        public static EquipmentSelectState GetInstance()
        {
            return Inst ??= new EquipmentSelectState();
        }

        private enum Key
        {
            Left, Right, Enter, Default
        }

        private enum State
        {
            Yes = 35, No = 45,
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
            if (select)
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
                Console.SetCursorPosition((int)State.Yes - 3, prevTop);
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
                    if (EquipmentUI.GetInstance().UnEquipFlag)
                    {
                        EquipmentUI.GetInstance().CompleteUnEquipSelect = 1;
                        ReturnState();
                    }
                    else if (EquipmentUI.GetInstance().EquipFlag)
                        NextState();
                    break;
                case (int)State.No:
                    if (EquipmentUI.GetInstance().UnEquipFlag)
                    {
                        EquipmentUI.GetInstance().CompleteUnEquipSelect = -1;
                        ReturnState();
                    }
                    else if (EquipmentUI.GetInstance().EquipFlag)
                        EquipmentUI.GetInstance().CompleteEquipSelect = -1;
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
            prevTop = 27;
            prevLeft = 35;
        }

        public void NextState()
        {
            Console.Clear();
            // UI의 전 상태를 현재상태로 변경하고
            UiState.GetInstance().prevState = this;
            // UI를 Inventory로 변경
            UiState.GetInstance().SetUi("Inventory");
            Game.GetInstance().ChangeState(UiState.GetInstance());
            // Inventory 시작위치를 설정
            InventoryUI.GetInstance().prevLeft = InventoryUI.tileStartXSize;
            InventoryUI.GetInstance().prevTop = InventoryUI.tileStartYSize;
        }
    }
}