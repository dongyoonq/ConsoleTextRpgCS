using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class EquipmentUI : UI
    {
        public int prevTop;
        public int prevLeft;

        const int tileStartXSize = 16;
        const int tileStartYSize = 4;
        const int tileEndYSize = 16;
        Key keyDown;

        bool startflag = false;
        public bool itemUseFlag = false;
        public bool selection = false;
        public int CompleteSelect = 0;

        private Item currPosItem;

        public enum Key
        {
            Up, Down,
            Enter, Back, Delete, Default
        }

        private static EquipmentUI Inst;
        public new static EquipmentUI GetInstance()
        {
            uiName = "Equipment";

            return Inst ??= new EquipmentUI();
        }

        public override bool Init()
        {
            return SetMap(StringToChar(LoadFileToStringMap())) ? true : false;
        }

        public override void Input()
        {
            Console.CursorLeft = prevLeft;
            Console.CursorTop = prevTop;

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    InputManager.GetInstance().SetCommand(new CursorMoveUpCommand());
                    keyDown = Key.Up;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    InputManager.GetInstance().SetCommand(new CursorMoveDownCommand());
                    keyDown = Key.Down;
                    break;
                case ConsoleKey.Enter:
                    InputManager.GetInstance().SetCommand(null);
                    keyDown = Key.Enter;
                    break;
                case ConsoleKey.Backspace:
                    Console.Clear();
                    prevLeft = tileStartXSize; prevTop = tileStartYSize;
                    Game.GetInstance().ChangeState(UiState.GetInstance().prevState);
                    keyDown = Key.Back;
                    break;
                case ConsoleKey.Delete:
                    keyDown = Key.Delete;
                    break;
                default:
                    keyDown = Key.Default;
                    InputManager.GetInstance().SetCommand(null);
                    break;
            }

            InputManager.GetInstance().ExecuteCommand();
        }

        public override void Update()
        {
            switch (keyDown)
            {
                case Key.Up:
                    prevTop = Console.GetCursorPosition().Top - 1;
                    break;
                case Key.Down:
                    prevTop = Console.GetCursorPosition().Top + 1;
                    break;
                case Key.Enter:
                    itemUseFlag = true;
                    break;
                case Key.Delete:
                    return;
                default:
                    return;
            }

        }

        public override void Render()
        {
            Console.Clear();

            Show();
        }

        protected override void Show()
        {
            ShowTileMap();
            DefaultRender();
        }

        private void DefaultRender()
        {
            Console.SetCursorPosition(24, 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ 인벤토리 창 ]");

            int y = tileStartYSize, x = tileStartXSize;
            foreach (var item in currPlayer.inventory.list)
            {
                Console.SetCursorPosition(x, y);
                if (prevLeft == x && prevTop == y)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{item.name}");
                    currPosItem = item;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(item.name);
                }
                y += 2;

                if (y > tileEndYSize)
                {
                    y = 4; x += 12;
                }
            }

            ExplanationItem();

            Console.SetCursorPosition(22, tileEndYSize + 4);
            Console.ForegroundColor = ConsoleColor.White;

            if (currPlayer.inventory.list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("가방이 비었습니다.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(21, tileEndYSize + 6);
                Console.WriteLine("BackSpace : 돌아가기");
                return;
            }

            Console.SetCursorPosition(14, tileEndYSize + 4);
            Console.ForegroundColor = ConsoleColor.White;

            if (!itemUseFlag && !selection)
            {
                Console.WriteLine("Enter　:　사용하기　　　　방향키 : 이동");
                Console.SetCursorPosition(14, tileEndYSize + 5);
                Console.WriteLine("Del　　:  버리기");
                Console.SetCursorPosition(14, tileEndYSize + 7);
                Console.WriteLine("BackSpace　:　돌아가기");

                Console.SetCursorPosition(prevLeft, prevTop);
            }
            else if (itemUseFlag && !selection)
            {
                Console.SetCursorPosition(17, tileEndYSize + 4);
                Console.ForegroundColor = ConsoleColor.White;

                selection = true;

                if (currPosItem is Equipment)
                    Console.WriteLine($"{currPosItem.name}를 장착 하시겠습니까?");
                else
                    Console.WriteLine($"{currPosItem.name}를 사용 하시겠습니까?");

                if (selection)
                {
                    Selection();
                }
            }
            else if (!itemUseFlag && selection)
            {
                Console.SetCursorPosition(17, tileEndYSize + 4);
                Console.ForegroundColor = ConsoleColor.White;

                if (currPosItem is Equipment)
                    Console.WriteLine($"{currPosItem.name}를 장착 하시겠습니까?");
                else
                    Console.WriteLine($"{currPosItem.name}를 사용 하시겠습니까?");
            }
            else if (selection && itemUseFlag && CompleteSelect > 0)
            {
                Console.SetCursorPosition(17, tileEndYSize + 4);
                Console.ForegroundColor = ConsoleColor.White;

                if (currPosItem is Equipment)
                {
                    currPlayer.Equip(currPosItem as Equipment);
                }
                else
                {
                    Console.WriteLine($"{currPosItem.name}를 사용 했습니다.");
                    Console.SetCursorPosition(17, tileEndYSize + 5);
                    currPlayer.useItem(currPosItem);
                }

                selection = false;
                itemUseFlag = false;
                CompleteSelect = 0;
                prevLeft = tileStartXSize; prevTop = tileStartYSize;
                System.Threading.Thread.Sleep(3000);
                Render();
                return;
            }
            else if (selection && itemUseFlag && CompleteSelect < 0)
            {
                Console.WriteLine("Enter : 사용하기     방향키 : 이동");
                Console.SetCursorPosition(14, tileEndYSize + 5);
                Console.WriteLine("BackSpace : 돌아가기");

                selection = false;
                itemUseFlag = false;
                CompleteSelect = 0;

            }
            else if (selection && itemUseFlag && CompleteSelect == 0)
            {
                Console.SetCursorPosition(17, tileEndYSize + 4);
                Console.ForegroundColor = ConsoleColor.White;

                if (currPosItem is Equipment)
                    Console.WriteLine($"{currPosItem.name}를 장착 하시겠습니까?");
                else
                    Console.WriteLine($"{currPosItem.name}를 사용 하시겠습니까?");
            }

            Console.SetCursorPosition(prevLeft, prevTop);
        }

        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileUI\Equipment.txt");

            string strMap = "";
            strMap = file.ReadToEnd();

            file.Close();

            return strMap;
        }

        protected override void ResetMap()
        {

        }

        private void Selection()
        {
            SelectState.GetInstance().prevState = this;
            SelectState.GetInstance().DefaultRender();
            Game.GetInstance().ChangeState(SelectState.GetInstance());
        }

        private void ExplanationItem()
        {
            if (currPlayer.inventory.list.Count == 0) { return; }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(60, tileStartYSize);
            currPosItem.Explain(60, tileStartYSize);

        }
    }
}
