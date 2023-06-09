﻿using System;
using System.Collections.Generic;
using System.Text;
using static Project_S.Weapon;

namespace Project_S
{
    // Player 클래스
    [Serializable]
    public class Player
    {
        // 플레이어 상태 클래스
        public class PlayerState
        {
            public string nickName;
            public int level;
            public string jobName;
            public Player.Job job;
            public Player.Pos pos;
            public Status status;
            public Inventory inventory;
            public Dictionary<Item.ItemType, Equipment> wearingEquip;
            public List<Skill> skills;

            public PlayerState(string nickName, int level, Player.Job job, string jobName,  Player.Pos pos, Status status, 
                Inventory inventory, Dictionary<Item.ItemType, Equipment> wearingEquip, List<Skill> skills)
            {
                this.nickName = nickName;
                this.level = level;
                this.job = job;
                this.jobName = jobName;
                this.pos = pos;
                this.status = status;
                this.inventory = inventory;
                this.wearingEquip = wearingEquip;
                this.skills = skills;
            }
        }

        [Serializable]
        public struct Pos
        {
            public int x, y;

            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [Serializable]
        [Flags]
        public enum Job
        {
            None = 0,
            Warrior = 1 << 0,
            Archer = 1 << 1,
            Mage = 1 << 2,
            Thief = 1 << 3,
            All = Warrior | Archer | Mage | Thief
        }

        // 이벤트 정의
        public delegate bool EquipEventHandler(Equipment equipment, ref int cursorLeft, ref int cursorTop);
        public static event EquipEventHandler EquipEvent;
        public static event EquipEventHandler UnEquipEvent;

        // 프로퍼티
        public string nickname;     // 닉네임
        public int level = 1;       // 레벨
        public int Hp, Mp;

        public Job job = Job.None;  // 직업
        public string jobName;

        private Pos _pos;
        public Pos pos { get { return _pos; } set { _pos = value; } }
        public int posX { get { return _pos.x; } set { _pos.x = value; } }
        public int posY { get { return _pos.y; } set { _pos.y = value; } }

        public Status status;
        public Inventory inventory;
        public Dictionary<Item.ItemType, Equipment> wearingEquip;

        public List<Skill> skills;  // 스킬

        public Player(string name)
        {
            nickname = name;
            jobName = GetJobType();
            _pos.x = 0; _pos.y = 0;
            status = new Status();
            status.AttackPoint = 50;
            status.MagicPoint = 30;
            status.MaxHp = 600;
            status.MaxMp = 30;
            Hp = status.MaxHp;
            Mp = status.MaxMp;
            status.AttackSpeed = 1;
            status.Defense = 50;
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

        protected string GetJobType()
        {
            switch (job)
            {
                case Job.None:
                    jobName = "초보자";
                    break;
                case Job.Warrior:
                    jobName = "전사";
                    break;
                case Job.Archer:
                    jobName = "궁수";
                    break;
                case Job.Mage:
                    jobName = "법사";
                    break;
                case Job.Thief:
                    jobName = "도적";
                    break;
            }

            return jobName;
        }

        /// <summary>
        /// 장비 착용 메서드
        /// </summary>
        /// <param name="equipment"></param>
        public bool Equip(Equipment equipment, ref int cursorLeft, ref int cursorTop)
        {
            return EquipEvent.Invoke(equipment, ref cursorLeft, ref cursorTop) ? true : false;
        }

        /// <summary>
        /// 장비 벗는 메서드
        /// </summary>
        /// <param name="equipment"></param>
        public bool UnEquip(Equipment equipment, ref int cursorLeft, ref int cursorTop)
        {
            return UnEquipEvent.Invoke(equipment, ref cursorLeft, ref cursorTop) ? true : false;
        }

        public void useItem(Item item)
        {
            if(inventory.list.Contains(item))
            {
                item.use();
                RemoveItemFromInventory(item);
            }
            else
            {
                Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
                Console.WriteLine("인벤토리에 해당 아이템이 없습니다.");
            }
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

        public static bool CheckAddEquipEvent()
        {
            if (EquipEvent != null && UnEquipEvent != null && EquipEvent.GetInvocationList().Length > 0 && UnEquipEvent.GetInvocationList().Length > 0)
                return true;

            return false;
        }
    }

    // 플레이어 메멘토 클래스
    [Serializable]
    public class PlayerMemento : IMemento
    {
        private readonly string _name;
        private readonly string _jobName;
        private readonly int _level;
        private readonly Player.Job _job;
        private readonly Player.Pos _pos;
        private readonly Status _status;
        private readonly Inventory _inventory;
        private readonly Dictionary<Item.ItemType, Equipment> _wearingEquip;
        private readonly List<Skill> _skills;

        public PlayerMemento(Player player)
        {
            _name = player.nickname;
            _jobName = player.jobName;
            _level = player.level;
            _job = player.job;
            _pos = player.pos;
            _status = player.status;
            _inventory = player.inventory;
            _wearingEquip = player.wearingEquip;
            _skills = player.skills;

        }

        public string GetName()
        {
            return _name;
        }

        public string GetDate()
        {
            return DateTime.Now.ToString();
        }

        public Player.PlayerState GetPlayerState()
        {
            return new Player.PlayerState(_name, _level, _job, _jobName, _pos, _status, _inventory, _wearingEquip, _skills);
        }
    }
}