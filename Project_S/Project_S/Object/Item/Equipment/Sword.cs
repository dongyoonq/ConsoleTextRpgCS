using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    internal class Sword : Weapon
    {

        public Sword()
        {
            name = "Sword";
            type = Type.Weapon;
            attackPoint = 30;

            if(!ItemManager.GetInstance().itemTable.ContainsKey(this.name))
                ItemManager.GetInstance().itemTable.Add(this.name, this);
        }

        public override void ApplyStatusModifier(Player player)
        {
            player.status.AttackPoint += attackPoint;
        }

        public override void RemoveStatusModifier(Player player)
        {
            player.status.AttackPoint -= attackPoint;
        }

        public override void use()
        {
            Console.WriteLine($"{this.name}을 휘두른다.");
        }
    }
}
