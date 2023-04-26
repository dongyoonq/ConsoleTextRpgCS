using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Item 클래스
    public abstract class Item
    {
        public enum ItemType
        {
            Weapon,
            Aromor,
            Portion,
            Other
        }

        protected string name;
        abstract public void use();
    }
}
