using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Item 클래스
    [Serializable]
    public abstract class Item
    {
        public ItemType type;

        public enum ItemType
        {
            Weapon,
            Armor,
            Bottom,
            Shoes,
            Glove,
            Potion,
            Other
        }

        public string name;

        protected Item()
        {
        }

        abstract public void use();
        abstract public void Explain(int CursorXPos, int CursorYPos);
    }
}
