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
        public int prevTop = (int)ItemPos.Weapon;

        const int tileStartYSize = 4;
        const int tileEndYSize = 16;
        Key keyDown = Key.Default;

        private enum ItemPos
        {
            Weapon = 4,
            Armor = 6,
            Bottom = 8,
            Glove = 10,
            Shoes = 12,
        }

        public bool UnEquipFlag = false;
        public bool EquipFlag = false;
        public bool UnEquipSelection = false;
        public bool EquipSelection = false;

        public int CompleteUnEquipSelect = 0;
        public int CompleteEquipSelect = 0;

        private Equipment currPosItem;
        public Item.ItemType currPosType;
        public GameState tempState;

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
            //Console.CursorLeft = prevLeft;
            //Console.CursorTop = prevTop;

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
                    prevTop = tileStartYSize;
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
                    if (currPosItem != null)
                        UnEquipFlag = true;
                    else
                        EquipFlag = true;
                    break;
                case Key.Delete:
                    return;
                default:
                    return;
            }

            if (prevTop <= tileStartYSize)
                prevTop = tileStartYSize;
            else if (prevTop >= (int)ItemPos.Shoes)
                prevTop = (int)ItemPos.Shoes;
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
            Console.SetCursorPosition(38, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 장비 창 ]");

            // 장비 정보를 출력하는 조건문
            if (currPlayer != null)
            {
                ////////////////////////////////////////////////////////////////////////////
                Console.SetCursorPosition(10, (int)ItemPos.Weapon);

                if (prevTop == (int)ItemPos.Weapon)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Weapon))
                        currPosItem = currPlayer.wearingEquip[Item.ItemType.Weapon];
                    else
                        currPosItem = null;
                    currPosType = Item.ItemType.Weapon;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("▶ 무기 : ");

                if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Weapon))
                {
                    if (prevTop == (int)ItemPos.Weapon)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(currPlayer.wearingEquip[Item.ItemType.Weapon].name);
                }
                else
                    Console.WriteLine(string.Empty);
                ////////////////////////////////////////////////////////////////////////////
                Console.SetCursorPosition(10, (int)ItemPos.Armor);

                if (prevTop == (int)ItemPos.Armor)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Armor))
                        currPosItem = currPlayer.wearingEquip[Item.ItemType.Armor];
                    else
                        currPosItem = null;
                    currPosType = Item.ItemType.Armor;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("▶ 갑옷 : ");

                if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Armor))
                {
                    if (prevTop == (int)ItemPos.Armor)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(currPlayer.wearingEquip[Item.ItemType.Armor].name);
                }
                else
                    Console.WriteLine(string.Empty);
                ////////////////////////////////////////////////////////////////////////////
                Console.SetCursorPosition(10, (int)ItemPos.Bottom);

                if (prevTop == (int)ItemPos.Bottom)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Bottom))
                        currPosItem = currPlayer.wearingEquip[Item.ItemType.Bottom];
                    else
                        currPosItem = null;
                    currPosType = Item.ItemType.Bottom;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("▶ 하의 : ");

                if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Bottom))
                {
                    if (prevTop == (int)ItemPos.Bottom)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(currPlayer.wearingEquip[Item.ItemType.Bottom].name);
                }
                else
                    Console.WriteLine(string.Empty);
                ////////////////////////////////////////////////////////////////////////////
                Console.SetCursorPosition(10, (int)ItemPos.Glove);

                if (prevTop == (int)ItemPos.Glove)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Glove))
                        currPosItem = currPlayer.wearingEquip[Item.ItemType.Glove];
                    else
                        currPosItem = null;
                    currPosType = Item.ItemType.Glove;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("▶ 장갑 : ");

                if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Glove))
                {
                    if (prevTop == (int)ItemPos.Glove)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(currPlayer.wearingEquip[Item.ItemType.Glove].name);
                }
                else
                    Console.WriteLine(string.Empty);
                ////////////////////////////////////////////////////////////////////////////
                Console.SetCursorPosition(10, (int)ItemPos.Shoes);

                if (prevTop == (int)ItemPos.Shoes)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Shoes))
                        currPosItem = currPlayer.wearingEquip[Item.ItemType.Shoes];
                    else
                        currPosItem = null;
                    currPosType = Item.ItemType.Shoes;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("▶ 신발 : ");

                if (currPlayer.wearingEquip.ContainsKey(Item.ItemType.Shoes))
                {
                    if (prevTop == (int)ItemPos.Shoes)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(currPlayer.wearingEquip[Item.ItemType.Shoes].name);
                }
                else
                    Console.WriteLine(string.Empty);
            }

            // 현재 커서에 위치한 장비에 대한 설명 출력
            ExplanationEquipment();

            ExplanationApplyStatus();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(24, tileEndYSize + 9);

            // 플래그에 따른 커서위치에 출력
            if (!EquipFlag && !UnEquipFlag && !UnEquipSelection)
            {
                if (currPosItem != null)
                {
                    Console.WriteLine("Enter　:　장착해제　　　　방향키 : 이동");
                    Console.SetCursorPosition(24, tileEndYSize + 10);
                    Console.WriteLine("Del　　:  버리기");
                    Console.SetCursorPosition(24, tileEndYSize + 12);
                    Console.WriteLine("BackSpace　:　돌아가기");
                }
                else
                {
                    Console.WriteLine("Enter　:　장착　　　　　　방향키 : 이동");
                    Console.SetCursorPosition(24, tileEndYSize + 11);
                    Console.WriteLine("BackSpace　:　돌아가기");
                }
            }
            // 장착해제에 대한 출력
            else if (UnEquipFlag && !UnEquipSelection)
            {
                Console.SetCursorPosition(28, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                UnEquipSelection = true;

                Console.WriteLine($"{currPosItem.name}를 장착 해제하시겠습니까?");

                if (UnEquipSelection)
                {
                    Selection();
                }
            }
            else if (!UnEquipFlag && UnEquipSelection)
            {
                Console.SetCursorPosition(28, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"{currPosItem.name}를 장착 해제하시겠습니까?");
            }
            else if (UnEquipSelection && UnEquipFlag && CompleteUnEquipSelect > 0)
            {
                Console.SetCursorPosition(17, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                int CursorLeft = 28;
                int CursorTop = 25;
                currPlayer.UnEquip(currPosItem as Equipment, ref CursorLeft, ref CursorTop);

                UnEquipSelection = false;
                UnEquipFlag = false;
                CompleteUnEquipSelect = 0;
                prevTop = (int)ItemPos.Weapon;
                Console.CursorTop = prevTop;
                System.Threading.Thread.Sleep(3000);
                Render();
                return;
            }
            else if (UnEquipSelection && UnEquipFlag && CompleteUnEquipSelect < 0)
            {
                if (currPosItem != null)
                {
                    Console.WriteLine("Enter　:　장착해제　　　　방향키 : 이동");
                    Console.SetCursorPosition(24, tileEndYSize + 10);
                    Console.WriteLine("Del　　:  버리기");
                    Console.SetCursorPosition(24, tileEndYSize + 12);
                    Console.WriteLine("BackSpace　:　돌아가기");
                }
                else
                {
                    Console.WriteLine("Enter　:　장착　　　　　　방향키 : 이동");
                    Console.SetCursorPosition(24, tileEndYSize + 11);
                    Console.WriteLine("BackSpace　:　돌아가기");
                }

                UnEquipSelection = false;
                UnEquipFlag = false;
                CompleteUnEquipSelect = 0;

            }
            else if (UnEquipSelection && UnEquipFlag && CompleteUnEquipSelect == 0)
            {
                Console.SetCursorPosition(28, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"{currPosItem.name}를 장착 해제하시겠습니까?");
            }
            // 장착에 대한 출력
            else if (EquipFlag && !EquipSelection)
            {
                Console.SetCursorPosition(28, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                EquipSelection = true;

                Console.WriteLine($"해당부위에 장착 하시겠습니까?");

                if (EquipSelection)
                {
                    tempState = UiState.GetInstance().prevState;
                    Selection();
                }
            }
            else if (!EquipFlag && EquipSelection)
            {
                Console.SetCursorPosition(28, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"해당부위에 장착 하시겠습니까?");
            }
            else if (EquipSelection && EquipFlag && CompleteEquipSelect > 0)
            {
                Console.SetCursorPosition(17, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                Restart();
                Console.CursorTop = prevTop;
                return;
            }
            else if (EquipSelection && EquipFlag && CompleteEquipSelect < 0)
            {
                if (currPosItem != null)
                {
                    Console.WriteLine("Enter　:　장착해제　　　　방향키 : 이동");
                    Console.SetCursorPosition(24, tileEndYSize + 10);
                    Console.WriteLine("Del　　:  버리기");
                    Console.SetCursorPosition(24, tileEndYSize + 12);
                    Console.WriteLine("BackSpace　:　돌아가기");
                }
                else
                {
                    Console.WriteLine("Enter　:　장착　　　　　　방향키 : 이동");
                    Console.SetCursorPosition(24, tileEndYSize + 11);
                    Console.WriteLine("BackSpace　:　돌아가기");
                }

                EquipSelection = false;
                EquipFlag = false;
                CompleteEquipSelect = 0;
            }
            else if (EquipSelection && EquipFlag && CompleteEquipSelect == 0)
            {
                Console.SetCursorPosition(28, tileEndYSize + 9);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"해당부위에 장착 하시겠습니까?");
            }

            Console.SetCursorPosition(10, prevTop);
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

        public void Restart()
        {
            if(CompleteEquipSelect >= 0)
                prevTop = (int)ItemPos.Weapon;

            keyDown = Key.Default;

            UnEquipFlag = false;
            EquipFlag = false;
            UnEquipSelection = false;
            EquipSelection = false;

            CompleteUnEquipSelect = 0;
            CompleteEquipSelect = 0;

            currPosItem = null;
            currPosType = Item.ItemType.Other;
            UiState.GetInstance().prevState = tempState;

            Render();
            Game.GetInstance().RestartState();
        }

        private void Selection()
        {
            EquipmentSelectState.GetInstance().prevState = this;
            EquipmentSelectState.GetInstance().DefaultRender();
            Game.GetInstance().ChangeState(EquipmentSelectState.GetInstance());
            Game.GetInstance().RestartState();
        }

        private void ExplanationEquipment()
        {
            if (currPlayer.wearingEquip.Count == 0 || currPosItem == null) { return; }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(36, tileStartYSize + 1);
            currPosItem.Explain(36, tileStartYSize + 1);
        }

        private void ExplanationApplyStatus()
        {
            if (currPlayer.wearingEquip.Count == 0 || currPosItem == null) { return; }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(36, tileStartYSize + 10);
            currPosItem.ShowApplyStatus(36, tileStartYSize + 10);
        }
    }
}
