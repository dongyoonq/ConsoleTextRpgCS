using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class ItemManager
    {
        public Dictionary<string, Item> itemTable;
        private string ItemPrefab;

        private static ItemManager Inst;
        public static ItemManager GetInstance()
        {
            return Inst ??= new ItemManager();
        }

        public bool Init()
        {
            itemTable = new Dictionary<string, Item>();
            return true;
        }
    }
}
