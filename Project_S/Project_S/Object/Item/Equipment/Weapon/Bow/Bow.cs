using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    internal class Bow : Weapon
    {
        public Bow()
        {
            name = "Bow";
            attackPoint = 40;
            attackSpeed = 1.2;
            requireLevel = 5;
            weaponType = WeaponType.Bow;

            if (!ItemManager.GetInstance().itemTable.ContainsKey(this.name))
                ItemManager.GetInstance().itemTable.Add(this.name, this);
        }

        public override void ApplyStatusModifier(Player player)
        {
            player.status.AttackPoint += attackPoint;
            player.status.AttackSpeed = attackSpeed;
        }

        public override void Explain(int CursorXPos, int CursorYPos)
        {

        }

        public override void RemoveStatusModifier(Player player)
        {
            player.status.AttackPoint -= attackPoint;
            player.status.AttackSpeed = 1.0;
        }

        public override void ShowApplyPredicateStatus(int CursorXPos, int CursorYPos)
        {
        }

        public override void ShowApplyStatus(int CursorXPos, int CursorYPos)
        {

        }

        public override void use()
        {
            Console.WriteLine($"{this.name}을 쏜다.");
        }
    }
}
