using System;

namespace Project_S
{
    // 게임의 상태를 나타내는 인터페이스
    [Serializable]
    public abstract class GameState
    {
        public abstract void Input();
        public abstract void Update();
        public abstract void Render();
    }
}
