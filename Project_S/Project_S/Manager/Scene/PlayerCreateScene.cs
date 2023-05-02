using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_S
{
    public class PlayerCreateScene : Scene
    {
        private int prevTop = 6;
        bool Up = false;
        bool ArrowKeyDown = false;
        bool InputNickname = false;
        bool RangeChangeFlag = false;
        string name;

        private enum State
        {
            InputName = 6,
            Descison = 8,
            Reset = 10,
            Back = 12,
        }

        private static PlayerCreateScene Inst;
        public new static PlayerCreateScene GetInstance()
        {
            sceneName = "PlayerCreateScene";
            return Inst ??= new PlayerCreateScene();
        }

        public bool Init()
        {
            return SetMap(StringToChar(LoadFileToStringMap())) ? true : false;
        }

        public override void Input()
        {
            Console.SetCursorPosition(0, prevTop);

            if (InputNickname)
            { prevTop = 8; return; }

            var key = Console.ReadKey(true);

            switch (key.Key)
            {

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    ArrowKeyDown = true;
                    InputManager.GetInstance().SetCommand(new CursorMoveUpCommand());
                    Up = true;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    ArrowKeyDown = true;
                    InputManager.GetInstance().SetCommand(new CursorMoveDownCommand());
                    Up = false;
                    break;
                case ConsoleKey.Enter:
                    ArrowKeyDown = false;
                    InputManager.GetInstance().SetCommand(null);
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
            if (InputNickname)
                return;

            if (ArrowKeyDown)
            {
                if (Up)
                    prevTop = Console.GetCursorPosition().Top - 1;
                else
                    prevTop = Console.GetCursorPosition().Top + 1;
            }

            if (prevTop > (int)State.Back)
            { prevTop = (int)State.Back; return; }

            if(!RangeChangeFlag)
            {
                if (prevTop < (int)State.InputName)
                { prevTop = (int)State.InputName; return; }
            }
            else
            {
                if (prevTop < (int)State.Descison)
                { prevTop = (int)State.Descison; return; }
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
            Console.SetCursorPosition(17, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" 플레이어 닉네임을 입력하세요");

            if (prevTop == (int)State.Descison)
            {
                Console.SetCursorPosition(27, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ▶ 결정한다 ");
            }
            else
            {
                Console.SetCursorPosition(27, (int)State.Descison);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 결정한다 ");
            }

            if (prevTop == (int)State.Reset)
            {
                Console.SetCursorPosition(28, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ▶ 초기화 ");
            }
            else
            {
                Console.SetCursorPosition(28, (int)State.Reset);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 초기화 ");
            }

            if (prevTop == (int)State.Back)
            {
                Console.SetCursorPosition(27, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ▶ 돌아간다 ");
            }
            else
            {
                Console.SetCursorPosition(27, (int)State.Back);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 돌아간다 ");
            }

            if(!InputNickname)
            {
                Console.SetCursorPosition(23, (int)State.InputName);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" 닉네임 : ");
                if (prevTop == (int)State.InputName)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    name = null;
                    Console.CursorVisible = true;
                    name = GetNickname();
                    Console.CursorVisible = false;
                    InputNickname = true;
                    RangeChangeFlag = true;
                }
                else
                {
                    Console.SetCursorPosition(23, (int)State.InputName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" 닉네임 : ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{name}");
                }
            }
            else
            {
                Console.SetCursorPosition(23, (int)State.InputName);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" 닉네임 : ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{name}");
                InputNickname = false;
            }
        }

        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileMap\PlayerCreateScene.txt");

            string strMap = "";
            strMap = file.ReadToEnd();

            file.Close();

            return strMap;
        }

        protected override void ResetMap()
        {
            name = null;
            RangeChangeFlag = false;
            InputNickname = false;
            prevTop = 6;
            base.ResetMap();
            Init();
        }

        private void NextState(int prevTop)
        {
            switch (prevTop)
            {
                case (int)State.Descison:
                    CreatePlayer();
                    GameModeState.GetInstance().SetScene("Map01");
                    EventManager.GetInstance().OnPlayerMoveEventAdd();
                    Map01.GetInstance().Init();
                    break;
                case (int)State.Reset:
                    name = null;
                    RangeChangeFlag = false;
                    InputNickname = false;
                    this.prevTop = 6;
                    break;
                case (int)State.Back:
                    Console.Clear();
                    ResetMap();
                    GameModeState.GetInstance().SetScene("StartScene");
                    break;
                default:
                    break;
            }
        }

        private void CreatePlayer()
        {
            Player player = new Player(this.name);
            PlayerManager.GetInstance().playerList.Add(player);
        }

        private string GetNickname()
        {
            do
            {
                Console.SetCursorPosition(33, (int)State.InputName);
                name = Console.ReadLine();
                if (IsNotValidNickname())
                {
                    Console.SetCursorPosition(16, 17);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("닉네임은 영문, 숫자로 구성되어야합니다.");
                    Console.SetCursorPosition(0, 0);
                    Show();
                    Console.SetCursorPosition(23, (int)State.InputName);
                }
            } while (IsNotValidNickname());
            return name;
        }

        private bool IsNotValidNickname()
        {
            return
            ContainsKorean(name) || string.IsNullOrEmpty(name)
                || name.Any(c => char.IsWhiteSpace(c)
                || char.GetUnicodeCategory(c) == UnicodeCategory.OtherPunctuation
                || char.GetUnicodeCategory(c) == UnicodeCategory.ConnectorPunctuation
                || char.GetUnicodeCategory(c) == UnicodeCategory.DashPunctuation) ? true : false;
        }

        private bool ContainsKorean(string s)
        {
            foreach (char c in s)
            {
                UnicodeCategory category = char.GetUnicodeCategory(c);
                if (category == UnicodeCategory.OtherLetter || category == UnicodeCategory.LetterNumber)
                {
                    // 유니코드 범주가 OtherLetter나 Letter인 경우 한글일 가능성이 있음
                    if ((c >= 0xAC00 && c <= 0xD7A3) || (c >= 0x3131 && c <= 0x318E) || c == ' ')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
