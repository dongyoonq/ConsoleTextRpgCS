using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class InventoryUI : UI
    {
        public int prevTop;
        public int prevLeft;

        public const int tileStartXSize = 16;
        public const int tileStartYSize = 4;
        public const int tileEndYSize = 16;
        Key keyDown;

        bool startflag = false;
        public bool itemUseFlag = false;
        public bool selection = false;
        public int CompleteSelect = 0;

        private Item currPosItem;

        public enum Key
        {
            Up, Down, Left, Right,
            Enter, Back, Delete, Default
        }

        private static InventoryUI Inst;
        public new static InventoryUI GetInstance()
        {
            uiName = "Inventory";

            return Inst ??= new InventoryUI();
        }

        /// <summary>
        /// InventoryUI 상태에서의 Init 처리, 맵 상태 즉 타일정보를 불러온다.
        /// </summary>
        /// <returns></returns>
        public override bool Init()
        {
            return SetMap(StringToChar(LoadFileToStringMap())) ? true : false;
        }

        /// <summary>
        /// InventoryUI 상태에서의 Input 처리
        /// </summary>
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
                case ConsoleKey.Backspace:
                    // 뒤로가기 버튼을 눌렀을경우
                    // 장비창에서 호출한 Inventory면 EquipmentUICallCancelEquip 함수 호출
                    // 아니면 현재상태를 초기화하고 이전 맵으로 돌아간다.
                    if (EquipmentUI.GetInstance().EquipFlag)
                        EquipmentUICallCancelEquip();
                    else
                    {
                        ResetMap();
                        Game.GetInstance().ChangeState(UiState.GetInstance().prevState);
                    }
                    return;
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

        /// <summary>
        /// InventroyUI 상태에서의 Update 처리
        /// </summary>
        public override void Update()
        {
            // 시작 커서위치를 정해준다. 인벤토리 내용이 없으면 0,0
            // 있으면 맵의 시작 포인트로 이동한다 (16, 4)
            SetStartCursorPosition();

            // 현재 인벤토리 리스트X, Y를 구하여 커서가 움직일수 있는
            // 최대 X, Y 위치를 를 구해준다.
            int MaxLeft, MaxTop;
            UpdateCursorMaximum(out MaxLeft, out MaxTop);

            // 키에 따른 업데이트 처리
            switch (keyDown)
            {
                case Key.Up:
                    prevTop = Console.GetCursorPosition().Top - 1;
                    break;
                case Key.Down:
                    prevTop = Console.GetCursorPosition().Top + 1;
                    break;
                case Key.Right:
                    prevLeft = Console.GetCursorPosition().Left + 11;
                    break;
                case Key.Left:
                    prevLeft = Console.GetCursorPosition().Left - 11;
                    break;
                case Key.Enter:
                    itemUseFlag = true;
                    break;
                case Key.Delete:
                    // 해당위치의 아이템을 삭제하고
                    // 마지막 아이템 위치 였다면 커서를 시작위치로 돌린다.
                    DeleteItemFromInventory(MaxLeft, MaxTop);
                    return;
                default:
                    return;
            }

            // 커서가 움직일수 있는 최대 X, Y 위치에 도착해있으면
            // 못움직이게 그 이상으로 못움직이게 설정한다.
            MaxCursorPosition(MaxLeft, MaxTop);
        }

        /// <summary>
        /// InventoryUI 상태에서의 Render 처리
        /// </summary>
        public override void Render()
        {
            Console.Clear();

            Show();
        }

        /// <summary>
        /// 화면에 맵과, 출력될 문자를 그려준다.
        /// </summary>
        protected override void Show()
        {
            ShowTileMap();
            DefaultRender();
        }

        /// <summary>
        /// 화면에 출력될 문자를 그리는 메서드
        /// </summary>
        private void DefaultRender()
        {
            Console.SetCursorPosition(24, 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ 인벤토리 창 ]");

            // 현재 인벤토리의 아이템 리스트 정보를 불러온다.
            GetItemListFromInventory();

            // 현재 커서에 위치한 아이템에 대한 설명을 출력해준다.
            ExplanationItem();

            ExplanationApplyStatus();

            // 플래그 상태정보에 따라 커서 위치에 다른 출력을 해준다.
            RenderFromFlagState();

            Console.SetCursorPosition(prevLeft, prevTop);
        }

        /// <summary>
        /// Inventory UI 에 따른 맵 불러오기 오버라이딩
        /// </summary>
        /// <returns></returns>
        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileUI\Inventory.txt");

            string strMap = "";
            strMap = file.ReadToEnd();

            file.Close();

            return strMap;
        }

        /// <summary>
        /// 현재 맵(UI) 상태정보를 초기화 해주는 메서드
        /// </summary>
        protected override void ResetMap()
        {
            Console.Clear();

            prevLeft = tileStartXSize; prevTop = tileStartYSize;

            keyDown = Key.Default;

            startflag = false;
            itemUseFlag = false;
            selection = false;
            CompleteSelect = 0;

            currPosItem = null;
        }

        /// <summary>
        /// 인벤토리창 시작시 커서의 시작위치를 정해준다.
        /// </summary>
        private void SetStartCursorPosition()
        {
            if (!startflag)
            {
                if (currPlayer.inventory.list.Count == 0)
                { prevTop = 0; prevLeft = 0; }
                else
                { prevTop = tileStartYSize; prevLeft = tileStartXSize; }

                startflag = true;
            }
        }

        /// <summary>
        /// 커서가 움직일수 있는 최대 X, Y 위치에 도착해있으면
        /// 못움직이게 그 이상으로 못움직이게 설정하는 메서드
        /// </summary>
        /// <param name="maxLeft"></param>
        /// <param name="maxTop"></param>
        private void MaxCursorPosition(int maxLeft, int maxTop)
        {
            if (prevTop <= tileStartYSize)
                prevTop = tileStartYSize;
            else if (prevTop >= maxTop)
                prevTop = maxTop;
            else if (prevLeft <= tileStartXSize)
                prevLeft = tileStartXSize;
            else if (prevLeft >= maxLeft)
                prevLeft = maxLeft;

            if (prevTop >= maxTop && prevLeft <= tileStartXSize)
            {
                int a = Console.GetCursorPosition().Left;
                int b = Console.GetCursorPosition().Top;
                prevTop = maxTop;
                prevLeft = tileStartXSize;
            }
            else if (prevTop <= tileStartYSize && prevLeft <= tileStartXSize)
            {
                int a = Console.GetCursorPosition().Left;
                int b = Console.GetCursorPosition().Top;
                prevTop = tileStartYSize;
                prevLeft = tileStartXSize;
            }
            else if (prevTop >= maxTop && prevLeft >= maxLeft)
            {
                int a = Console.GetCursorPosition().Left;
                int b = Console.GetCursorPosition().Top;
                prevTop = maxTop;
                prevLeft = maxLeft;
            }
            else if (prevTop <= maxTop && prevLeft >= maxLeft)
            {
                int a = Console.GetCursorPosition().Left;
                int b = Console.GetCursorPosition().Top;
                prevLeft = maxLeft;
            }
        }

        /// <summary>
        ///  해당위치의 아이템을 삭제하고
        /// 마지막 아이템 위치 였다면 커서를 시작위치로 돌리는 메서드
        /// </summary>
        /// <param name="maxLeft"></param>
        /// <param name="maxTop"></param>
        private void DeleteItemFromInventory(int maxLeft, int maxTop)
        {
            currPlayer.RemoveItemFromInventory(currPosItem);
            if (prevTop >= maxTop && prevLeft >= maxLeft)
            {
                prevTop = tileStartYSize;
                prevLeft = tileStartXSize;
            }
        }

        /// <summary>
        /// 인벤토리 개수를 가져와 가로 개수, 세로 개수를 구하고 ( 인벤토리 개수가 9개라면 가로 개수는 1, 세로 개수는 2 )
        /// 구한 가로, 세로 개수를 이용하여 현재 인벤토리에서 커서가 움직일수 있는 최대 X, Y 위치를 를 구해주는 메서드
        /// </summary>
        /// <param name="maxLeft"></param>
        /// <param name="maxTop"></param>
        private void UpdateCursorMaximum(out int maxLeft, out int maxTop)
        {
            maxLeft = 0; maxTop = 0;

            int listCount = currPlayer.inventory.list.Count;

            int listXSize = (listCount < 7) ? 0 : listCount / 7;
            int listYSize = listCount % 7;

            if (prevTop < tileStartYSize + (listYSize * 2))
                maxLeft = tileStartXSize + (listXSize * 12);
            else if (prevTop >= tileStartYSize + (listYSize * 2))
                maxLeft = tileStartXSize + ((listXSize - 1) * 12);

            if (prevLeft < tileStartXSize + (listXSize * 12))
                maxTop = tileEndYSize;
            else if (prevLeft >= tileStartXSize + (listXSize * 12))
                maxTop = tileStartYSize + ((listYSize - 1) * 2);
        }

        /// <summary>
        /// 현재 인벤토리의 아이템 리스트 정보를 불러온다.
        /// </summary>
        private void GetItemListFromInventory()
        {
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
        }

        /// <summary>
        /// 현재 커서에 위치한 아이템에 대한 설명을 출력해준다.
        /// </summary>
        private void ExplanationItem()
        {
            if(currPlayer.inventory.list.Count == 0 || currPosItem == null) { return; }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(60, tileStartYSize);
            currPosItem.Explain(60, tileStartYSize);
        }

        private void ExplanationApplyStatus()
        {
            if (currPlayer.inventory.list.Count == 0 || currPosItem == null || currPosItem is not Equipment) { return; }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(60, tileStartYSize + 10);
            Equipment equipment;
            equipment = currPosItem as Equipment;
            equipment.ShowApplyPredicateStatus(60, tileStartYSize + 10);
        }

        /// <summary>
        /// 플래그 상태정보에 따라 커서 위치에 다른 출력을 해주는 메서드
        /// </summary>
        private void RenderFromFlagState()
        {

            Console.SetCursorPosition(22, tileEndYSize + 4);
            Console.ForegroundColor = ConsoleColor.White;

            // 인벤토리에 아무것도 없을경우
            if (currPlayer.inventory.list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("가방이 비었습니다.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(21, tileEndYSize + 6);
                Console.WriteLine("BackSpace : 돌아가기");
                return;
            }

            Console.SetCursorPosition(12, tileEndYSize + 4);
            Console.ForegroundColor = ConsoleColor.White;

            // 어떠한 플래그도 켜지지 않는 경우
            if (!itemUseFlag && !selection)
            {
                Console.WriteLine("Enter　:　사용하기　　　　방향키 : 이동");
                Console.SetCursorPosition(12, tileEndYSize + 5);
                Console.WriteLine("Del　　:  버리기");
                Console.SetCursorPosition(12, tileEndYSize + 7);
                Console.WriteLine("BackSpace　:　돌아가기");

                Console.SetCursorPosition(prevLeft, prevTop);
            }
            // itemUseFlag가 켜진경우 즉, Enter키가 막 눌렸을때 상황을 출력하고 선택플래그를 킨다.
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
            // itemUseFlag, selection이 켜졌을때 CompleteSelect가 0보다 큰 경우, 즉 SelectState에서
            // Yes 버튼(키)이 눌린 경우에 1이 되므로 아이템을 사용한 상태에 대한 출력을 해준다.
            // Select가 장비UI에서 들어오지 않는경우, 즉 Inventory 현재 상태에서 아이템을 사용했을때는 UseItem 메서드를
            // Select가 장비UI에서 들어왔을때, 즉 EquimentUI에서 Inventory 상태로 이동했을때 아이템을 사용했을때는 EquipmentUICallEquip 메서드를 호출한다.
            else if (selection && itemUseFlag && CompleteSelect > 0)
            {
                Console.SetCursorPosition(17, tileEndYSize + 4);
                Console.ForegroundColor = ConsoleColor.White;

                if (!EquipmentUI.GetInstance().EquipFlag)
                    UseItem();
                else
                    EquipmentUICallEquip();

                return;
            }
            // itemUseFlag, selection이 켜졌을때 CompleteSelect가 0보다 작은 경우, 즉 SelectState에서
            // No 버튼(키)이 눌린 경우에 -1이 되므로 아이템을 사용하지 않았을때에 대한 출력을 해준다.
            else if (selection && itemUseFlag && CompleteSelect < 0)
            {
                Console.WriteLine("Enter　:　사용하기　　　　방향키 : 이동");
                Console.SetCursorPosition(12, tileEndYSize + 5);
                Console.WriteLine("Del　　:  버리기");
                Console.SetCursorPosition(12, tileEndYSize + 7);
                Console.WriteLine("BackSpace　:　돌아가기");

                selection = false;
                itemUseFlag = false;
                CompleteSelect = 0;

            }
            // itemUseFlag가 켜지고 selection이 켜진경우 즉, Enter키가 눌린 상태에 대한 출력을 해준다.
            else if (selection && itemUseFlag && CompleteSelect == 0)
            {
                Console.SetCursorPosition(17, tileEndYSize + 4);
                Console.ForegroundColor = ConsoleColor.White;

                if (currPosItem is Equipment)
                    Console.WriteLine($"{currPosItem.name}를 장착 하시겠습니까?");
                else
                    Console.WriteLine($"{currPosItem.name}를 사용 하시겠습니까?");
            }
        }

        /// <summary>
        /// select 플래그가 켜져있을때 호출하는 메서드이다.
        /// SelectState의 전 상태 정보를 자신으로 저장하고
        /// SelectState를 그려주고, 상태를 UiState에서 SelectState로 변경한다.
        /// </summary>
        private void Selection()
        {
            InventorySelectState.GetInstance().prevState = this;
            InventorySelectState.GetInstance().DefaultRender();
            Game.GetInstance().ChangeState(InventorySelectState.GetInstance());
        }

        /// <summary>
        /// EquipmnetUI에서 Inventory가 호출되었을때 아무것도 선택안하고
        /// 뒤로갈때 호출하는 메서드이다. 현재 상태를 초기화하고, EquipmentUI 상태로 돌아가고
        /// EquipmentUI에서 Restart 메서드를 호출한다.
        /// </summary>
        private void EquipmentUICallCancelEquip()
        {
            ResetMap();
            Game.GetInstance().ChangeState(EquipmentUI.GetInstance());
            EquipmentUI.GetInstance().Restart();
        }

        /// <summary>
        /// 현재상태에서 아이템 사용 메서드, 사용 시 아이템이 장비일 경우 플레이어의 Equip 메서드 호출,
        /// 장비가 아닐경우엔 플레이어의 useItem 메서드 호출
        /// </summary>
        private void UseItem()
        {
            // 커서에 위치한 아이템이 장비라면
            if (currPosItem is Equipment)
            {
                int CursorLeft = 11;
                int CursorTop = tileEndYSize + 3;
                currPlayer.Equip(currPosItem as Equipment, ref CursorLeft, ref CursorTop);
            }
            // 아니라면
            else
            {
                Console.WriteLine($"{currPosItem.name}를 사용 했습니다.");
                Console.SetCursorPosition(17, tileEndYSize + 5);
                currPlayer.useItem(currPosItem);
            }

            System.Threading.Thread.Sleep(3000);

            // 후처리 : 모든 플래그를 꺼주고, 다시 출력시켜준다.
            selection = false;
            itemUseFlag = false;
            CompleteSelect = 0;
            prevLeft = tileStartXSize; prevTop = tileStartYSize;
            Render();
        }

        /// <summary>
        /// EquipmnetUI에서 현재상태(InventroyUI)가 호출되었을때 장비 장착 메서드
        /// EquipmnetUI에서 호출했을때 해당 위치의 부위에 맞는 아이템을 장착 시켰을경우
        /// 장착이 가능한 상태라면(플레이어 Equip메서드 true) 해당부위에 아이템을 장착시키고,
        /// 아닐경우 다른 출력을 해준다.
        /// </summary>
        private void EquipmentUICallEquip()
        {
            // 커서에 위치한 아이템이 장비창에서 들어온 타입과 같은 타입이라면
            if (currPosItem.type == EquipmentUI.GetInstance().currPosType)
            {
                int CursorLeft = 11;
                int CursorTop = tileEndYSize + 3;
                // 장착가능한 상태일때
                if(currPlayer.Equip(currPosItem as Equipment, ref CursorLeft, ref CursorTop))
                {
                    EquipmentUI.GetInstance().CompleteEquipSelect = 1;
                    System.Threading.Thread.Sleep(3000);
                    ResetMap();
                    Game.GetInstance().ChangeState(EquipmentUI.GetInstance());
                    EquipmentUI.GetInstance().Restart();
                    return;
                }
                // 아닐때
                else
                {
                    System.Threading.Thread.Sleep(3000);
                    ResetMap();
                    Render();
                    Game.GetInstance().RestartState();
                }
            }
            // 커서에 위치한 아이템이 장비창에서 들어온 타입과 다른 타입이라면
            else
            {
                Console.SetCursorPosition(11, tileEndYSize + 4);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{currPosItem.name}은 {EquipmentUI.GetInstance().currPosType}부위에 장착할 수 없습니다.");
                System.Threading.Thread.Sleep(3000);
                ResetMap();
                Render();
                Game.GetInstance().RestartState();
                return;
            }
        }
    }
}
