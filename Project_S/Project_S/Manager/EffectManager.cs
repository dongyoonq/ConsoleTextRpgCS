using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class EffectManager
    {
        private static EffectManager Inst;
        public static EffectManager GetInstance()
        {
            return Inst ??= new EffectManager();
        }

        public bool Init()
        {
            return true;
        }
    }
}
