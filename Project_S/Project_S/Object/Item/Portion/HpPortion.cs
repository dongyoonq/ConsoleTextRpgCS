using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    [Serializable]
    internal class HpPortion : Portion
    {
        public HpPortion()
        {
            name = "HpPortion";
            value = 20;

            if (!ItemManager.GetInstance().itemTable.ContainsKey(this.name))
                ItemManager.GetInstance().itemTable.Add(this.name, this);
        }

        public override void use()
        {
            int StartPosX = Console.GetCursorPosition().Left;
            int StartPosY = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(StartPosX, StartPosY);
            Console.WriteLine($"{this.name}을 사용합니다.");
            Console.SetCursorPosition(StartPosX, StartPosY + 1);
            Console.WriteLine($"{this.value}만큼 HP가 회복되었습니다.");
        }

        public override void Explain(int CursorXPos, int CursorYPos)
        {
            string[] explain =
            {
                $"[ {name} ]",
                "",
                $"HP를 회복시켜주는 포션이다.",
                $"수치 : {value}",
            };

            foreach (string exp in explain)
            {
                Console.WriteLine(exp);
                Console.SetCursorPosition(CursorXPos, ++CursorYPos);
            }
        }
    }
}
