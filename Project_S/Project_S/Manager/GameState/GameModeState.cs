using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class GameModeState : GameState
    {
        public override void Input()
        {

        }

        public override void Update()
        {

        }

        public override void Render()
        {
            SceneManager.GetInstance().ShowScene();
            UiManager.GetInstance().Show();
        }
    }
}
