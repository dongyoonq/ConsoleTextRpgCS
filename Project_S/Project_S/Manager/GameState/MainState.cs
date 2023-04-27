using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class MainState : GameState
    {
        public override void Input()
        {
            // 메인 상태에서의 Input 처리
            InputManager.GetInstance().HandleInput();
        }

        public override void Update()
        {
            // 메인 상태에서의 Update 처리
        }

        public override void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Main State");
            // 메인 상태에서의 Render 처리

        }
    }
}
