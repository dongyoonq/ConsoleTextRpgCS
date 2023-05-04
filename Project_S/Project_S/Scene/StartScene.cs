using System;
using System.Collections.Generic;
using System.IO;
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

        public override bool Init()
        {
            return SetMap(StringToChar(LoadFileToStringMap())) ? true : false;
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
            ShowTileMap();
            DefaultRender();
        }

        private void DefaultRender()
        {
            Console.SetCursorPosition(17, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" 새로운 플레이어를 생성하세요 !");

            if (prevTop == (int)State.CreatePlayer)
            {
                Console.SetCursorPosition(27, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ▶ 생성한다 ");
            }
            else
            {
                Console.SetCursorPosition(27, (int)State.CreatePlayer);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 생성한다 ");
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

            if (prevTop == (int)State.Exit)
            {
                Console.SetCursorPosition(29, prevTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ▶ 종료");
            }
            else
            {
                Console.SetCursorPosition(29, (int)State.Exit);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" 종료");
            }
        }

        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileMap\StartScene.txt");

            string strMap = "";
            strMap = file.ReadToEnd();

            file.Close();

            return strMap;
        }

        protected override void ResetMap()
        {
            Console.Clear();
            prevTop = 6;
            base.ResetMap();
            Init();
        }

        private void NextState(int prevTop)
        {
            switch (prevTop)
            {
                case (int)State.CreatePlayer:
                    ResetMap();
                    GameModeState.GetInstance().SetScene("PlayerCreateScene");
                    break;
                case (int)State.Back:
                    ResetMap();
                    Game.GetInstance().ChangeState(MainState.GetInstance());
                    break;
                case (int)State.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
