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

        public List<Skill> skills;  // 스킬

        public Player(string name)
        {
            status = new Status();
            status.MaxHp = 600;
            status.MaxMp = 30;
            status.AttackPoint = 50;
            nickname = name;
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

        public void Equip(Equipment equipment)
        {
            equipment.ApplyStatusModifier(this);
        }

        public void UnEquip(Equipment equipment)
        {
            equipment.RemoveStatusModifier(this);
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