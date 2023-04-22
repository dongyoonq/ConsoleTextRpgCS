using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class SoundManager
    {
        private static SoundManager Inst;
        public static SoundManager GetInstance()
        {
            return Inst ??= new SoundManager();
        }

        public void Run()
        {

        }
    }
}
