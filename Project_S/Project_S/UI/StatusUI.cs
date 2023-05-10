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
    public class StatusUI : UI
    {
        public int prevTop = (int)StatusPos.Strength;

        public const int tileStartXSize = 16;
        public const int tileStartYSize = 4;
        public const int tileEndYSize = 21;
        Key keyDown;

        public enum StatusPos
        {
            Strength = 18,
            Dexterity = 19,
            Intelligence = 20,
            Luck = 21,
        }

        public enum Key
        {
            Up, Down,
            Enter, Default
        }

        private static StatusUI Inst;
        public new static StatusUI GetInstance()
        {
            uiName = "Status";

            return Inst ??= new StatusUI();
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
                    ResetMap();
                    Game.GetInstance().ChangeState(UiState.GetInstance().prevState);
                    return;
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
            // 키에 따른 업데이트 처리
            switch (keyDown)
            {
                case Key.Up:
                    prevTop = Console.GetCursorPosition().Top;
                    break;
                case Key.Down:
                    prevTop = Console.GetCursorPosition().Top;
                    break;
                case Key.Enter:
                    break;
                default:
                    return;
            }

            if (prevTop <= (int)StatusPos.Strength)
                prevTop = (int)StatusPos.Strength;
            else if (prevTop >= (int)StatusPos.Luck)
                prevTop = (int)StatusPos.Luck;
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
            Console.SetCursorPosition(22, 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ 스텟 정보 창 ]");

            ShowPlayerStatusInformation();

            // 플래그 상태정보에 따라 커서 위치에 다른 출력을 해준다.
            RenderFromFlagState();

            Console.SetCursorPosition(0, prevTop);
        }

        /// <summary>
        /// Inventory UI 에 따른 맵 불러오기 오버라이딩
        /// </summary>
        /// <returns></returns>
        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileUI\Status.txt");

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

        }

        private void ShowPlayerStatusInformation()
        {
            int startPos = tileStartYSize + 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(10, startPos++);
            Console.WriteLine($" [ 이름 : {currPlayer.nickname} ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10, ++startPos);
            Console.WriteLine($" 레벨 : {currPlayer.level}");
            Console.SetCursorPosition(10, ++startPos);
            Console.WriteLine($" 공격력 : {currPlayer.status.AttackPoint}");
            Console.SetCursorPosition(10, ++startPos);
            Console.WriteLine($" 주문력 : {currPlayer.status.MagicPoint}");
            Console.SetCursorPosition(10, ++startPos);
            Console.WriteLine($" 공격속도 : {currPlayer.status.AttackSpeed}");
            Console.SetCursorPosition(10, ++startPos);
            Console.WriteLine($" 체력 : {currPlayer.Hp}/{currPlayer.status.MaxHp}");
            Console.SetCursorPosition(10, ++startPos);
            Console.WriteLine($" 마나 : {currPlayer.Mp}/{currPlayer.status.MaxMp}");
            Console.SetCursorPosition(10, ++startPos);
            Console.WriteLine($" 방어력 : {currPlayer.status.Defense}");
            startPos += 3;
            Console.SetCursorPosition(10, ++startPos);
            Console.Write($" 힘　 : {currPlayer.status.Strength}");
            if (prevTop == (int)StatusPos.Strength)
            { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"  ▲"); Console.ForegroundColor = ConsoleColor.White; }
            else
                Console.WriteLine($"  ▲");
            Console.SetCursorPosition(10, ++startPos);
            Console.Write($" 민첩 : {currPlayer.status.Dexterity}");
            if (prevTop == (int)StatusPos.Dexterity)
            { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"  ▲"); Console.ForegroundColor = ConsoleColor.White; }
            else
                Console.WriteLine($"  ▲");
            Console.SetCursorPosition(10, ++startPos);
            Console.Write($" 지능 : {currPlayer.status.Intelligence}");
            if (prevTop == (int)StatusPos.Intelligence)
            { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"  ▲"); Console.ForegroundColor = ConsoleColor.White; }
            else
                Console.WriteLine($"  ▲");
            Console.SetCursorPosition(10, ++startPos);
            Console.Write($" 행운 : {currPlayer.status.Luck}");
            if (prevTop == (int)StatusPos.Luck)
            { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine($"  ▲"); Console.ForegroundColor = ConsoleColor.White; }
            else
                Console.WriteLine($"  ▲");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(tileStartXSize - 6, tileEndYSize + 3);
            Console.WriteLine($"※ 스텟 포인트 : {currPlayer.status.StatusPoint}");
        }

        /// <summary>
        /// 인벤토리창 시작시 커서의 시작위치를 정해준다.
        /// </summary>
        private void SetStartCursorPosition()
        {

        }

        /// <summary>
        /// 플래그 상태정보에 따라 커서 위치에 다른 출력을 해주는 메서드
        /// </summary>
        private void RenderFromFlagState()
        {

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
    }
}
