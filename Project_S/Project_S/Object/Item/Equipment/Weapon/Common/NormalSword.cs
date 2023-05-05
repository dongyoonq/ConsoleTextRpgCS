using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    [Serializable]
    internal class NormalSword : Weapon
    {
        public NormalSword()
        {
            name = "Sword";
            attackPoint = 40;
            attackSpeed = 1.15;
            requireLevel = 1;
            weaponType = WeaponType.Common;

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
            Console.WriteLine($"{this.name}을 휘두른다.");
        }

        public override void Explain(int CursorXPos, int CursorYPos)
        {
            string[] explain =
            {
                $"[ {name} ]",
                "",
                $"평범한 검이다.",
                $"공격력 : {attackPoint}",
                $"공격속도 : {attackSpeed}",
                $"요구레벨 : {requireLevel}",
                $"착용 가능 직업 : {requireJob = GetJobType()}",
            };

            foreach( string exp in explain )
            {
                Console.WriteLine(exp);
                Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            }
        }
    }
}
