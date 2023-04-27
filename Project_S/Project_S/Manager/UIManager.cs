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

        public void Show()
        {
            string[] Ui = { "  _   _ ___ ",
                             " | | | |_ _|",
                             " | | | || | ",
                             " | |_| || | ",
                             "  \\___/|___|" };
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("=========================================================================");
            foreach(string key in Ui)
                Console.WriteLine(key);
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("=========================================================================");
        }
    }
}
