using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Memento Pattern
    public class SaveManager
    {
        private static SaveManager Inst;
        public static SaveManager GetInstance()
        {
            return Inst ??= new SaveManager();
        }

        public void Run()
        {

        }
    }
}
