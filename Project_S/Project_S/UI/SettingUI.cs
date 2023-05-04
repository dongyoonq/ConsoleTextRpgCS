using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class SettingUI : UI
    {
        private int prevTop = 6;
        bool Up = false;
        bool ArrowKeyDown = false;

        private enum State
        {
            Save = 6,
            Back = 8
        }

        private static SettingUI Inst;
        public new static SettingUI GetInstance()
        {
            uiName = "Setting";
            return Inst ??= new SettingUI();
        }

        public override bool Init()
        {
            return SetMap(StringToChar(LoadFileToStringMap())) ? true : false;
        }

        public override void Input()
        {

            // 메인 상태에서의 Input 처리
            Console.SetCursorPosition(0, prevTop);

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    InputManager.GetInstance().SetCommand(new CursorMoveUpCommand());
                    ArrowKeyDown = true;
                    Up = true;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    InputManager.GetInstance().SetCommand(new CursorMoveDownCommand());
                    ArrowKeyDown = true;
                    Up = false;
                    break;
                case ConsoleKey.Enter:
                    InputManager.GetInstance().SetCommand(null);
                    ArrowKeyDown = false;
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
            // 메인 상태에서의 Update 처리
            if (ArrowKeyDown)
            {
                if (Up)
                    prevTop = Console.GetCursorPosition().Top - 1;
                else
                    prevTop = Console.GetCursorPosition().Top + 1;
            }

            if (Console.CursorTop > (int)State.Back)
            { prevTop = (int)State.Back; return; }

            if (Console.CursorTop < (int)State.Save)
            { prevTop = (int)State.Save; return; }
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
            if (prevTop == (int)State.Save)
            {
                Console.SetCursorPosition(27, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ▶ 게임 세이브");
            }
            else
            {
                Console.SetCursorPosition(27, (int)State.Save);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 게임 세이브");
            }

            if (prevTop == (int)State.Back)
            {
                Console.SetCursorPosition(27, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ▶ 돌아가기");
            }
            else
            {
                Console.SetCursorPosition(27, (int)State.Back);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 돌아가기");
            }
        }

        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileUI\Setting.txt");

            string strMap = "";
            strMap = file.ReadToEnd();

            file.Close();

            return strMap;
        }

        protected override void ResetMap()
        {

        }

        private void NextState(int prevTop)
        {
            switch (prevTop)
            {
                case (int)State.Save:
                    Console.Clear();
                    SaveModeInputHandle();
                    break;
                case (int)State.Back:
                    Game.GetInstance().ChangeState(UiState.GetInstance().prevState);
                    break;
                    /*
                case (int)State.Exit:
                    Environment.Exit(0);
                    break;*/
                default:
                    break;
            }
        }

        private void SaveModeInputHandle()
        {
            PlayerManager.GetInstance().Save();
            GameModeState.GetInstance().Save();

            SaveManager.GetInstance().Save();
        }
    }
}
