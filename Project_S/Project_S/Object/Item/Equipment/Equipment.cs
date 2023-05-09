using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    [Serializable]
    public abstract class Equipment : Item
    {
        public int requireLevel;
        public string requireJob;

        public abstract void ApplyStatusModifier(Player player);
        public abstract void RemoveStatusModifier(Player player);
        public abstract void ShowApplyStatus(int CursorXPos, int CursorYPos);
    }
}
