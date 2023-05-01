using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class PlayerManager
    {
        public List<Player> playerList;

        private static PlayerManager Inst;
        public static PlayerManager GetInstance()
        {
            return Inst ??= new PlayerManager();
        }

        public bool Init()
        {
            playerList = new List<Player> ();
            return true;
        }
    }
}
