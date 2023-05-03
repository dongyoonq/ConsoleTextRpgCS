using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class UiState : GameState
    {
        private static UiState Inst;
        public static UiState GetInstance()
        {
            return Inst ??= new UiState();
        }

        public override void Input()
        {

        }

        public override void Update()
        {
        }

        public override void Render()
        {
        }
    }
}