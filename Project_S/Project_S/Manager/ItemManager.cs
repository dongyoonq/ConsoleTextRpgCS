using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class ItemManager
    {
        private List<Item> items;
        private string ItemPrefab;

        private static ItemManager Inst;
        public static ItemManager GetInstance()
        {
            return Inst ??= new ItemManager();
        }

        public bool Init()
        {
            return true;
        }
    }
}
