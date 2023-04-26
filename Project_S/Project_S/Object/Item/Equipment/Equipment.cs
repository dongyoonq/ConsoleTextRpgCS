using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public abstract class Equipment : Item
    {
        protected Type type;

        public enum Type
        {
            Weapon,
            Armor,
            Accessory,
            // ...
        }

        public abstract void ApplyStatusModifier(Player player);
        public abstract void RemoveStatusModifier(Player player);
    }
}
