using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    // 게임모드에선 장면들에서 Input, Render, Update한다.
    public class UiState : GameState
    {
        private UI crrUi;
        public GameState prevState;

        private static UiState Inst;
        public static UiState GetInstance()
        {
            return Inst ??= new UiState();
        }

        public override void Input()
        {
            crrUi?.Input();
        }

        public override void Update()
        {
            crrUi?.Update();
        }

        public override void Render()
        {
            crrUi?.Render();
        }

        // SceneManager가 가지고있는 장면을 가져와 설정한다.
        public void SetUi(string UiName)
        {
            crrUi = UiManager.GetInstance().GetUi(UiName);
        }
    }
}
