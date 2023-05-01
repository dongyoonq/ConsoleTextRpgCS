using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class MonsterManager
    {
        public Dictionary<string, Monster> monsterTable;
        private int spawnInfo;

        private static MonsterManager Inst;
        public static MonsterManager GetInstance()
        {
            return Inst ??= new MonsterManager();
        }

        public bool Init()
        {
            return true;
        }
    }
}
