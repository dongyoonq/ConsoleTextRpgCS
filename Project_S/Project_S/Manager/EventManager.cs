using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // 다른 클래스에서 발생한 이벤트를 EventManager에서 구독하는 클래스
    public class EventManager
    {
        private string eventName;
        object? eventData;

        private static EventManager Inst;
        public static EventManager GetInstance()
        {
            return Inst ??= new EventManager();
        }

        public bool Init()
        {
            // 입력 관리자 클래스(InputManager)에서 발생한 KeyPressed 이벤트를
            // Player.OnKeyPressed 함수에 연결(구독)합니다.
            // 이벤트가 발생하면 이를 처리하는 핸들러(Player.OnKeyPressed)를 호출
            InputManager.GetInstance().KeyPressed += Player.OnKeyPressed;
            return true;
        }
    }
}