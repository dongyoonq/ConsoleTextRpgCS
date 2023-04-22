using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class InventoryManager
    {
        private Inventory inventory;
        public int Layout;

        private static InventoryManager Inst;
        public static InventoryManager GetInstance()
        {
            return Inst ??= new InventoryManager();
        }

        public bool Init()
        {
            return true;
        }
    }
}
