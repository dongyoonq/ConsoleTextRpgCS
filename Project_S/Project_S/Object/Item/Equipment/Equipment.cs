using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public abstract class Equipment : Item
    {
        public int requireLevel;

        public abstract void ApplyStatusModifier(Player player);
        public abstract void RemoveStatusModifier(Player player);
    }
}
