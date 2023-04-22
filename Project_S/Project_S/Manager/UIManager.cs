using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class UiManager
    {
        private static UiManager Inst;
        public static UiManager GetInstance()
        {
            return Inst ??= new UiManager();
        }

        public bool Init()
        {
            return true;
        }
    }
}
