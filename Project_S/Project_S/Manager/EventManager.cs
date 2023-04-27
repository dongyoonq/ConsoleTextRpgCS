using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // 다른 클래스에서 발생한 이벤트 핸들러를 EventManager에서 구독하는 클래스
    // 또는 다른클래스 이벤트와 다른클래스의 이벤트 핸들러를 연결해주는 역할로
    // 이벤트의 전반적인 부분을 관리하는 역할을한다.
    public class EventManager
    {
        private static EventManager Inst;
        public static EventManager GetInstance()
        {
            return Inst ??= new EventManager();
        }

        public bool Init()
        {
            // 입력 관리자 클래스(InputManager)에서 발생한 KeyPressed 이벤트를
            // Player.OnKeyPressed 함수에 연결(구독)합니다.
            // 이벤트가 발생하면 이를 처리하는 핸들러(Player.OnKeyPressed)를 호출
            // 추후 UI를 조작하고 싶을 때 플레이어를 움직이는 OnKeyPressed를 지우고
            // UI를 조작하는 이벤트 핸들러를 구독시키면된다.
            InputManager.GetInstance().KeyPressed += Player.OnKeyPressed;

            // 장착, 장착해제 핸들러 등록(구독)
            Player.EquipEvent += OnEquip;
            Player.UnEquipEvent += UnEquip;

            return true;
        }

        /// <summary>
        /// 장비 착용 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="equipment"></param>
        public void OnEquip(object sender, Equipment equipment)
        {
            Player player = sender as Player;

            Console.WriteLine($"{equipment.name} 장착 시도");

            // 들어온 아이템이없으면 빠져나온다.
            if (equipment == null)
                return;

            // 들어온 아이템이 인벤토리에 없으면 빠져나온다.
            if (!player.inventory.list.Contains(equipment))
            {
                Console.WriteLine($"{equipment.name}가 인벤토리에 없습니다.");
                return;
            }

            // 플레이어 클래스(직업)별 장비의 다른 처리
            switch(player)
            {
                // 플레이어가 전사일때, 들어온 아이템이 칼, 공용타입이 아니면 착용 불가
                case Warrior:
                    if (equipment.type == Item.ItemType.Weapon)
                    {
                        if (equipment is Weapon)
                        {
                            Weapon weapon = (Weapon)equipment;
                            if (weapon.weaponType != Weapon.WeaponType.Sword)
                            {
                                if (weapon.weaponType == Weapon.WeaponType.Common)
                                    break;
                                Console.WriteLine("검, 공용무기가 아니면 착용 불가능합니다.");
                                return;
                            }
                        }
                    }
                    break;
                // 플레이어가 궁수일때, 들어온 아이템이 활, 공용타입이 아니면 착용 불가
                case Archer:
                    if (equipment.type == Item.ItemType.Weapon)
                    {
                        if (equipment is Weapon)
                        {
                            Weapon weapon = (Weapon)equipment;
                            if (weapon.weaponType != Weapon.WeaponType.Bow)
                            {
                                if (weapon.weaponType == Weapon.WeaponType.Common)
                                    break;
                                Console.WriteLine("활, 공용무기가 아니면 착용 불가능합니다.");
                                return;
                            }
                        }
                    }
                    break;
                // 플레이어가 법사일때, 들어온 아이템이 지팡이, 공용타입이 아니면 착용 불가
                case Mage:
                    if (equipment.type == Item.ItemType.Weapon)
                    {
                        if (equipment is Weapon)
                        {
                            Weapon weapon = (Weapon)equipment;
                            if (weapon.weaponType != Weapon.WeaponType.Staff)
                            {
                                if (weapon.weaponType == Weapon.WeaponType.Common)
                                    break;
                                Console.WriteLine("지팡이, 공용무기가 아니면 착용 불가능합니다.");
                                return;
                            }
                        }
                    }
                    break;
                // 플레이어가 도적일때, 들어온 아이템이 단도, 공용타입이 아니면 착용 불가
                case Thief:
                    if (equipment.type == Item.ItemType.Weapon)
                    {
                        if (equipment is Weapon)
                        {
                            Weapon weapon = (Weapon)equipment;
                            if (weapon.weaponType != Weapon.WeaponType.Dagger)
                            {
                                if (weapon.weaponType == Weapon.WeaponType.Common)
                                    break;
                                Console.WriteLine("단도, 공용무기가 아니면 착용 불가능합니다.");
                                return;
                            }
                        }
                    }
                    break;
                // 플레이어가 플레이어일때, 들어온 아이템이 공용타입이 아니면 착용 불가
                case Player:
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
                    break;
                default:
                    throw new Exception("정의되지 않은 플레이어의 호출");
            }

            // 장비요구 레벨보다 현재 레벨이 작으면 착용 불가
            if (player.level < equipment.requireLevel)
            {
                Console.WriteLine("착용레벨이 낮아 착용 불가능합니다.");
                return;
            }

            // 들어온 아이템이 그 부위에 착용중이면 장비를 벗는다.
            if (player.wearingEquip.ContainsKey(equipment.type))
                player.UnEquip(player.wearingEquip[equipment.type]);

            // 인벤토리에 장비를 지우고
            player.inventory.list.Remove(equipment);
            // 착용한 분위에 이 장비를 착용 시킨다.
            player.wearingEquip.Add(equipment.type, equipment);
            Console.WriteLine($"{equipment.name} 장착");
            // 그 장비에 대한 스텟 적용
            equipment.ApplyStatusModifier(player);
        }

        /// <summary>
        /// 장비 착용해제 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="equipment"></param>
        public void UnEquip(object sender, Equipment equipment)
        {
            Player player = sender as Player;

            Console.WriteLine($"{equipment.name} 벗기 시도");

            // 들어온 아이템이없으면 빠져나온다.
            if (equipment == null)
                return;

            // 착용중인 부위에 아이템이 있으면
            if (player.wearingEquip.ContainsKey(equipment.type))
            {
                Console.WriteLine($"착용중인 {player.wearingEquip[equipment.type].name} 벗음");
                // 인벤토리에 착용중인 장비를 넣어주고
                player.inventory.list.Add(player.wearingEquip[equipment.type]);
                // 착용중인 장비를 지워준다.
                player.wearingEquip.Remove(equipment.type);
                // 스텟 미적용
                equipment.RemoveStatusModifier(player);
            }
            else
                Console.WriteLine($"{equipment.type}를 장착하고 있지 않습니다.");
        }
    }
}