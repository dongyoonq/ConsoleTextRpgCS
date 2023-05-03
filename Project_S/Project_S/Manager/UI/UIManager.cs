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
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("　[ 1. 인벤토리 ]　[ 2. 장비 ]　[ 3. 스텟 ]　[ 4. 퀘스트 ]　[ 5. 설정 ]\n");
            Console.WriteLine("=========================================================================");
        }
    }
}
