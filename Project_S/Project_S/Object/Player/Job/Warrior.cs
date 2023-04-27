using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Warrior : Player
    {
        public Warrior(string name) : base(name)
        {
            job = Job.Warrior;
            status.MaxHp = 1600;
            status.MaxMp = 50;
        }


        /// <summary>
        /// 장비 착용 메서드
        /// </summary>
        /// <param name="equipment"></param>
        public override void Equip(Equipment equipment)
        {
            Console.WriteLine($"{equipment.name} 장착시도");

            // 들어온 아이템이없으면 빠져나온다.
            if (equipment == null)
                return;

            // 들어온 아이템이 인벤토리에 없으면 빠져나온다.
            if (!inventory.list.Contains(equipment))
            {
                Console.WriteLine($"{equipment.name}가 인벤토리에 없습니다.");
                return;
            }

            // 들어온 아이템이 검타입 아니면 착용 불가
            if (equipment.type == Item.ItemType.Weapon)
            {
                if (equipment is Weapon)
                {
                    Weapon weapon = (Weapon)equipment;
                    if (weapon.weaponType != Weapon.WeaponType.Bow)
                    {
                        Console.WriteLine("검이 아니면 착용 불가능합니다.");
                        return;
                    }
                }
            }

            // 장비요구 레벨보다 현재 레벨이 작으면 착용 불가
            if (this.level < equipment.requireLevel)
            {
                Console.WriteLine("착용레벨이 낮아 착용 불가능합니다.");
                return;
            }

            // 들어온 아이템이 그 부위에 착용중이면 장비를 벗는다.
            if (wearingEquip.ContainsKey(equipment.type))
                UnEquip(wearingEquip[equipment.type]);

            // 인벤토리에 장비를 지우고
            inventory.list.Remove(equipment);
            // 착용한 분위에 이 장비를 착용 시킨다.
            wearingEquip.Add(equipment.type, equipment);
            Console.WriteLine($"{equipment.name} 장착");
            // 그 장비에 대한 스텟 적용
            equipment.ApplyStatusModifier(this);
        }
    }
}
