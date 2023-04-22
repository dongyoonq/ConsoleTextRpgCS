using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class MonsterManager
    {
        private List<Monster> monsters;
        private int spawnInfo;

        private static MonsterManager Inst;
        public static MonsterManager GetInstance()
        {
            return Inst ??= new MonsterManager();
        }

        public void Run()
        {

        }
    }
}
