using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class InventoryUI
    {
        private static InventoryUI Inst;
        public static InventoryUI GetInstance()
        {
            return Inst ??= new InventoryUI();
        }

        public void show()
        {
            Console.Clear();
        }
    }
}
