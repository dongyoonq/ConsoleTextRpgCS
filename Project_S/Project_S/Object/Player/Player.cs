using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public enum Job
    {
        None,
        Warrior,
        Mage,
        Archer,
        Thief
    }

    // Player 클래스
    public class Player
    {
        public string nickname;     // 닉네임
        public int level = 1;       // 레벨

        public Job job = Job.None;  // 직업

        public Status status;
        public Inventory inventory;
        public Dictionary<Item.ItemType, Equipment> wearingEquip;

        public List<Skill> skills;  // 스킬

        public Player(string name)
        {
            status = new Status();
            status.MaxHp = 600;
            status.MaxMp = 30;
            status.AttackPoint = 50;
            nickname = name;
            skills = new List<Skill>();
            inventory = new Inventory();
            wearingEquip = new Dictionary<Item.ItemType, Equipment>();
        }

        // 인벤토리 추가 메서드
        public void AddItemToInventory(Item item)
        {
            this.inventory.list.Add(item);
        }

        // 인벤토리 아이템 제거 메서드
        public void RemoveItemFromInventory(Item item)
        {
            this.inventory.list.Remove(item);
        }

        public virtual void UseSkill()
        {

        }

        public virtual void Attack(Monster monster)
        {

        }

        // 레벨 업 메서드
        public void LevelUp()
        {

        }

        /// <summary>
        /// 장비 착용 메서드
        /// </summary>
        /// <param name="equipment"></param>
        public virtual void Equip(Equipment equipment)
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

            // 들어온 아이템이 공용타입이 아니면 착용 불가
            if (equipment.type == Item.ItemType.Weapon)
            {
                if (equipment is Weapon)
                {
                    Weapon weapon = (Weapon)equipment;
                    if (weapon.weaponType != Weapon.WeaponType.Common)
                    {
                        Console.WriteLine("공용무기가 아니면 착용 불가능합니다.");
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

        /// <summary>
        /// 장비 벗는 메서드
        /// </summary>
        /// <param name="equipment"></param>
        public virtual void UnEquip(Equipment equipment)
        {
            // 들어온 아이템이없으면 빠져나온다.
            if (equipment == null)
                return;

            // 착용중인 부위에 아이템이 있으면
            if (wearingEquip.ContainsKey(equipment.type))
            {
                Console.WriteLine($"착용중인 {wearingEquip[equipment.type].name} 벗음");
                // 인벤토리에 착용중인 장비를 넣어주고
                inventory.list.Add(wearingEquip[equipment.type]);
                // 착용중인 장비를 지워준다.
                wearingEquip.Remove(equipment.type);
                // 스텟 미적용
                equipment.RemoveStatusModifier(this);
            }
            else
                Console.WriteLine($"{equipment.type}를 장착하고 있지 않습니다.");
        }

        public virtual void UseWeapon()
        {
            if(wearingEquip.ContainsKey(Item.ItemType.Weapon))
            {
                wearingEquip[Item.ItemType.Weapon].use();
            }
            else
                Console.WriteLine("무기를 착용중이지 않습니다.");
        }

        // 플레이어 콘솔 입력 이벤트 핸들러
        public static void OnKeyPressed(char key)
        {
            switch (key)
            {
                case 'a':
                    Console.WriteLine($"Player moved left");
                    //player.MoveLeft();
                    break;
                case 'd':
                    Console.WriteLine($"Player moved Right");
                    //player.MoveRight();
                    break;
                case 'w':
                    Console.WriteLine($"Player moved Up");
                    //player.MoveUp();
                    break;
                case 's':
                    Console.WriteLine($"Player moved Down");
                    //player.MoveDown();
                    break;
                default:
                    break;
            }
        }
    }
}