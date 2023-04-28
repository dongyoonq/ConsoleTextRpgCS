using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    // 게임모드에선 장면들에서 Input, Render, Update한다.
    public class GameModeState : GameState
    {
        private Scene currScene;

        private static GameModeState Inst;
        public static GameModeState GetInstance()
        {
            return Inst ??= new GameModeState();
        }

        //Player tester = new Player("동윤");

        public override void Input()
        {
            currScene?.Input();
        }

        public override void Update()
        {
            currScene?.Update();
        }

        public override void Render()
        {
            currScene?.Render();
        }

        // SceneManager가 가지고있는 장면을 가져와 설정한다.
        public void SetScene(string SceneName)
        {
            currScene = SceneManager.GetInstance().GetScene(SceneName);
        }
    }
}
