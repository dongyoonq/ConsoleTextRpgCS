using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class QuestManager
    {
        private static QuestManager Inst;
        public static QuestManager GetInstance()
        {
            return Inst ??= new QuestManager();
        }

        public void Run()
        {

        }
    }
}
