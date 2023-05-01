using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Player 클래스
    public class Player
    {
        public struct Pos
        {
            public int x, y;
        }

        public enum Job
        {
            None,
            Warrior,
            Mage,
            Archer,
            Thief
        }

        // 이벤트 정의
        public delegate void EquipEventHandler(object sender, Equipment equipment);
        public static event EquipEventHandler EquipEvent;
        public static event EquipEventHandler UnEquipEvent;

        // 프로퍼티
        public string nickname;     // 닉네임
        public int level = 1;       // 레벨

        public Job job = Job.None;  // 직업

        private Pos _pos;
        public Pos pos { get { return _pos; } }
        public int posX { get { return _pos.x; } set { _pos.x = value; } }
        public int posY { get { return _pos.y; } set { _pos.y = value; } }

        public Status status;
        public Inventory inventory;
        public Dictionary<Item.ItemType, Equipment> wearingEquip;

        public List<Skill> skills;  // 스킬

        public Player(string name)
        {
            nickname = name;
            _pos.x = 0; _pos.y = 0;
            status = new Status();
            status.MaxHp = 600;
            status.MaxMp = 30;
            status.AttackPoint = 50;
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
        public void Equip(Equipment equipment)
        {
            EquipEvent?.Invoke(this, equipment);
        }

        /// <summary>
        /// 장비 벗는 메서드
        /// </summary>
        /// <param name="equipment"></param>
        public void UnEquip(Equipment equipment)
        {
            UnEquipEvent?.Invoke(this, equipment);
        }


        public virtual void UseWeapon()
        {
            if (wearingEquip.ContainsKey(Item.ItemType.Weapon))
            {
                wearingEquip[Item.ItemType.Weapon].use();
            }
            else
                Console.WriteLine("무기를 착용중이지 않습니다.");
        }
    }
}