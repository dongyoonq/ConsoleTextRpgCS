using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    // 게임의 상태를 나타내는 인터페이스
    public abstract class GameState
    {
        public abstract void Input();
        public abstract void Update();
        public abstract void Render();
    }
}
