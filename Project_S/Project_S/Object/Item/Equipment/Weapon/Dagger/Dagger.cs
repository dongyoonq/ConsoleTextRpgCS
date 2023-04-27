using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    internal class Dagger : Weapon
    {
        public Dagger()
        {
            name = "Dagger";
            attackPoint = 35;
            attackSpeed = 1.25;
            requireLevel = 5;
            weaponType = WeaponType.Dagger;

            if (!ItemManager.GetInstance().itemTable.ContainsKey(this.name))
                ItemManager.GetInstance().itemTable.Add(this.name, this);
        }

        public override void ApplyStatusModifier(Player player)
        {
            player.status.AttackPoint += attackPoint;
            player.status.AttackSpeed = attackSpeed;
        }

        public override void RemoveStatusModifier(Player player)
        {
            player.status.AttackPoint -= attackPoint;
            player.status.AttackSpeed = 1.0;
        }

        public override void use()
        {
            Console.WriteLine($"{this.name}을 쏜다.");
        }
    }
}
