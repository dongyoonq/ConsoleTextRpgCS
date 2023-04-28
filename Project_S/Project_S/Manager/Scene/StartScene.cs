using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class StartScene : Scene
    {
        private static StartScene Inst;
        public new static StartScene GetInstance()
        {
            sceneName = "StartScene";
            return Inst ??= new StartScene();
        }

        public override void Input()
        {
            Console.ReadKey(true);
            //InputManager.GetInstance().HandleInput(tester);
        }

        public override void Update()
        {
        }

        public override void Render()
        {
            Console.Clear();
            Show();
        }

        protected override void Show()
        {
            //UiManager.GetInstance().Show();
            Console.SetCursorPosition(20, 1);
            Console.WriteLine("StartScene 입니다.");
        }
    }
}
