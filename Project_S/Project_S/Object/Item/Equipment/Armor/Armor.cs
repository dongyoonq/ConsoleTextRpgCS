using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    abstract class Armor : Equipment
    {
        protected Armor()
        {
            type = ItemType.Armor;
        }
    }
}
