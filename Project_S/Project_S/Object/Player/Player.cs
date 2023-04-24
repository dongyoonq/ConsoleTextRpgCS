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
        public string nickname; // 닉네임
        public int level = 0; // 레벨
        public int health = 600; // 체력
        public int mana = 30; // 마나

        public Status status;
        public Job job = Job.None; // 직업
        public List<Skill> skills; // 스킬
        public Inventory inventory;

        public Player()
        {
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