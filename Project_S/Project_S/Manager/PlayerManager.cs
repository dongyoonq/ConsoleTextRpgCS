using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class PlayerManager
    {
        public Dictionary<string, Player> playerTable;

        private static PlayerManager Inst;
        public static PlayerManager GetInstance()
        {
            return Inst ??= new PlayerManager();
        }

        public bool Init()
        {
            playerTable = new Dictionary<string, Player>();
            return true;
        }
    }
}
