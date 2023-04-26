using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Item 클래스
    public abstract class Item
    {
        public ItemType type;

        public enum ItemType
        {
            Weapon,
            Aromor,
            Portion,
            Other
        }

        public string name;
        abstract public void use();
    }
}
