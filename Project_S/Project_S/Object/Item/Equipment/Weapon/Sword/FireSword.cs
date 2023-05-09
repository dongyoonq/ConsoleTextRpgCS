using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    [Serializable]
    internal class FireSword : Weapon
    {
        public FireSword()
        {
            name = "FireSword";
            attackPoint = 50;
            attackSpeed = 1.1;
            requireLevel = 5;
            weaponType = WeaponType.Sword;

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
            string[] explain =
{
                $"[ {name} ]",
                "",
                $"불속성 검이다.",
                $"휘두르면 불을쓰는 이펙트가 발생한다.",
                $"공격력 : {attackPoint}",
                $"공격속도 : {attackSpeed}",
            };

            foreach (string exp in explain)
            {
                Console.WriteLine(exp);
                Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            }

            Console.Write($"요구레벨 : {requireLevel} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"(현재레벨 : {Scene.InGamePlayer.level})");
            Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"착용 가능 직업 : {requireJob = GetJobType()} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"(현재직업 : {Scene.InGamePlayer.jobName})");
        }

        public override void RemoveStatusModifier(Player player)
        {
            player.status.AttackPoint -= attackPoint;
            player.status.AttackSpeed = 1.0;
        }

        public override void ShowApplyPredicateStatus(int CursorXPos, int CursorYPos)
        {
            Weapon equipWeapon;

            if (Scene.InGamePlayer.wearingEquip.ContainsKey(this.type))
            {
                if (Scene.InGamePlayer.wearingEquip[this.type] is Weapon)
                    equipWeapon = Scene.InGamePlayer.wearingEquip[this.type] as Weapon;
                else
                    return;
            }
            else
            {
                equipWeapon = new NormalSword();
                equipWeapon.attackPoint = 0;
                equipWeapon.attackSpeed = 1;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"※ 변경 능력치 ※ ");
            Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            Console.WriteLine();
            Console.SetCursorPosition(CursorXPos, ++CursorYPos);

            // 착용 부위가 있다면
            if (Scene.InGamePlayer.wearingEquip.ContainsKey(this.type))
            {

                Console.Write($"공격력 : {Scene.InGamePlayer.status.AttackPoint} "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("→ ");
                Console.Write($"{Scene.InGamePlayer.status.AttackPoint - equipWeapon.attackPoint + this.attackPoint}"); Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(CursorXPos, ++CursorYPos);
                Console.Write($"공격속도 : {equipWeapon.attackSpeed} "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("→ ");
                Console.Write($"{this.attackSpeed}"); Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.Write($"공격력 : {Scene.InGamePlayer.status.AttackPoint} "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("→ ");
                Console.Write($"{Scene.InGamePlayer.status.AttackPoint + this.attackPoint}"); Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(CursorXPos, ++CursorYPos);
                Console.Write($"공격속도 : {Scene.InGamePlayer.status.AttackSpeed} "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("→ ");
                Console.Write($"{this.attackSpeed}"); Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }

        public override void ShowApplyStatus(int CursorXPos, int CursorYPos)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"※ 적용 능력치※ ");
            Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            Console.WriteLine();
            Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            Console.Write($"공격력 : {Scene.InGamePlayer.status.AttackPoint}({Scene.InGamePlayer.status.AttackPoint - attackPoint} + ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{attackPoint}"); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($")");
            Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            Console.Write($"공격속도 : {Scene.InGamePlayer.status.AttackSpeed}(1.0 + ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Math.Round(attackSpeed - 1, 2)}"); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($")");
        }

        public override void use()
        {
            Console.WriteLine($"{this.name}을 휘두른다.");
        }
    }
}
