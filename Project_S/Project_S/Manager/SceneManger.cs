using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Scene 클래스
    public class SceneManager
    {
        // 생성자
        private static SceneManager Inst;
        public static SceneManager GetInstance()
        {
            return Inst ??= new SceneManager();
        }

        // 장면 실행 메서드
        public void Run()
        {
            // 장면에 필요한 기능을 구현
        }
    }
}
