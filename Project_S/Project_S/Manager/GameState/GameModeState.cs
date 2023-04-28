using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class GameModeState : GameState
    {
        Player tester = new Player("동윤");

        public override void Input()
        {
            InputManager.GetInstance().HandleInput(tester);
        }

        public override void Update()
        {

        }

        public override void Render()
        {
            Console.Clear();
            UiManager.GetInstance().Show();
            SceneManager.GetInstance().ShowScene(tester);
        }
    }
}
