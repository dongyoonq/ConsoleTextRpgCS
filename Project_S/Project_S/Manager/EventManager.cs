using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // 다른 클래스에서 발생한 이벤트 핸들러를 EventManager에서 구독하는 클래스
    // 이벤트의 전반적인 부분을 관리하는 역할을한다.
    public class EventManager
    {
        private static EventManager Inst;
        public static EventManager GetInstance()
        {
            return Inst ??= new EventManager();
        }

        public bool Init()
        {

            // 장착, 장착해제 핸들러 등록(구독)
            Player.EquipEvent += PlayerEquipHandler.GetInstance().OnEquip;
            Player.UnEquipEvent += PlayerEquipHandler.GetInstance().UnEquip;

            return true;
        }

        public void OnPlayerMoveEventAdd()
        {
            // 입력 관리자 클래스(InputManager)에서 발생한 KeyPressed 이벤트를
            // Player.OnKeyPressed 함수에 연결(구독)합니다.
            // 이벤트가 발생하면 이를 처리하는 핸들러(Player.OnKeyPressed)를 호출
            // 추후 UI를 조작하고 싶을 때 플레이어를 움직이는 OnKeyPressed를 지우고
            // UI를 조작하는 이벤트 핸들러를 구독시키면된다.
            InputManager.GetInstance().KeyPressed += PlayerInputHandler.GetInstance().OnKeyPressed;
        }
    }
}