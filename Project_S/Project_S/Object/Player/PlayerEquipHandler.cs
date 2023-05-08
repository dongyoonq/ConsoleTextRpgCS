using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class PlayerEquipHandler
    {
        private Queue<string> result;

        private static PlayerEquipHandler Inst;
        public static PlayerEquipHandler GetInstance()
        {
            return Inst ??= new PlayerEquipHandler();
        }

        private PlayerEquipHandler() { }

        /// <summary>
        /// 장비 착용 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="equipment"></param>
        public bool OnEquip(Equipment equipment, ref int cursorLeft, ref int cursorTop)
        {
            result = new Queue<string>();

            Player player = Scene.InGamePlayer;

            result.Enqueue($"{equipment.name}를 장착한다.");

            // 들어온 아이템이없으면 빠져나온다.
            if (equipment == null)
                return false;

            // 들어온 아이템이 인벤토리에 없으면 빠져나온다.
            if (!player.inventory.list.Contains(equipment))
            {
                result.Enqueue($"{equipment.name}가 인벤토리에 없습니다.");
                while(result.Count  > 0)
                {
                    Console.SetCursorPosition(cursorLeft ,cursorTop++);
                    Console.WriteLine(result.Dequeue());
                }
                return false;
            }

            // 플레이어 클래스(직업)별 장비의 다른 처리
            switch (player.jobName)
            {
                case "전사":
                    if (equipment.requireJob != "전사" && equipment.requireJob != "공용")
                    {
                        result.Enqueue($"플레이어의 직업은 : \"{player.jobName}\"인데");
                        result.Enqueue($"해당 장비의 착용가능 직업이 : \"{equipment.requireJob}\"이므로");
                        result.Enqueue("해당 장비는 착용 불가능합니다.");
                        while (result.Count > 0)
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop++);
                            Console.WriteLine(result.Dequeue());
                        }
                        return false;
                    }
                    break;
                case "궁수":
                    if (equipment.requireJob != "궁수" && equipment.requireJob != "공용")
                    {
                        result.Enqueue($"플레이어의 직업은 : \"{player.jobName}\"인데");
                        result.Enqueue($"해당 장비의 착용가능 직업이 : \"{equipment.requireJob}\"이므로");
                        result.Enqueue("해당 장비는 착용 불가능합니다.");
                        while (result.Count > 0)
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop++);
                            Console.WriteLine(result.Dequeue());
                        }
                        return false;
                    }
                    break;
                case "법사":
                    if (equipment.requireJob != "법사" && equipment.requireJob != "공용")
                    {
                        result.Enqueue($"플레이어의 직업은 : \"{player.jobName}\"인데");
                        result.Enqueue($"해당 장비의 착용가능 직업이 : \"{equipment.requireJob}\"이므로");
                        result.Enqueue("해당 장비는 착용 불가능합니다.");
                        while (result.Count > 0)
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop++);
                            Console.WriteLine(result.Dequeue());
                        }
                        return false;
                    }
                    break;
                case "도적":
                    if (equipment.requireJob != "도적" && equipment.requireJob != "공용")
                    {
                        result.Enqueue($"플레이어의 직업은 : \"{player.jobName}\"인데");
                        result.Enqueue($"해당 장비의 착용가능 직업이 : \"{equipment.requireJob}\"이므로");
                        result.Enqueue("해당 장비는 착용 불가능합니다.");
                        while (result.Count > 0)
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop++);
                            Console.WriteLine(result.Dequeue());
                        }
                        return false;
                    }
                    break;
                // 플레이어가 플레이어일때, 들어온 아이템이 공용타입이 아니면 착용 불가
                case "초보자":
                    if (equipment.requireJob != "공용")
                    {
                        result.Enqueue($"플레이어의 직업은 : \"{player.jobName}\"인데");
                        result.Enqueue($"해당 장비의 착용가능 직업이 : \"{equipment.requireJob}\"이므로");
                        result.Enqueue("해당 장비는 착용 불가능합니다.");
                        while (result.Count > 0)
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop++);
                            Console.WriteLine(result.Dequeue());
                        }
                        return false;
                    }
                    break;
                default:
                    throw new Exception("정의되지 않은 플레이어의 호출");
            }

            // 장비요구 레벨보다 현재 레벨이 작으면 착용 불가
            if (player.level < equipment.requireLevel)
            {
                result.Enqueue("착용레벨이 낮아 착용 불가능합니다.");
                while (result.Count > 0)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop++);
                    Console.WriteLine(result.Dequeue());
                }
                return false; 
            }

            // 들어온 아이템이 그 부위에 착용중이면 장비를 벗는다.
            if (player.wearingEquip.ContainsKey(equipment.type))
            {
                while (result.Count > 0)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop++);
                    Console.WriteLine(result.Dequeue());
                }
                player.UnEquip(player.wearingEquip[equipment.type], ref cursorLeft, ref cursorTop);
            }

            // 인벤토리에 장비를 지우고
            player.inventory.list.Remove(equipment);
            // 착용한 분위에 이 장비를 착용 시킨다.
            player.wearingEquip.Add(equipment.type, equipment);
            result.Enqueue($"{equipment.name}를 장착 했습니다.");
            // 그 장비에 대한 스텟 적용
            equipment.ApplyStatusModifier(player);

            while (result.Count > 0)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop++);
                Console.WriteLine(result.Dequeue());
            }

            return true;
        }

        /// <summary>
        /// 장비 착용해제 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="equipment"></param>
        public bool UnEquip(Equipment equipment, ref int cursorLeft, ref int cursorTop)
        {
            result = new Queue<string>();

            Player player = Scene.InGamePlayer;

            // 들어온 아이템이없으면 빠져나온다.
            if (equipment == null)
                return false;

            // 착용중인 부위에 아이템이 있으면
            if (player.wearingEquip.ContainsKey(equipment.type))
            {
                result.Enqueue($"현재 착용중인 {player.wearingEquip[equipment.type].name} 벗는다.");
                result.Enqueue($"착용중인 {player.wearingEquip[equipment.type].name} 벗음");
                // 인벤토리에 착용중인 장비를 넣어주고
                player.inventory.list.Add(player.wearingEquip[equipment.type]);
                // 착용중인 장비를 지워준다.
                player.wearingEquip.Remove(equipment.type);
                // 스텟 미적용
                equipment.RemoveStatusModifier(player);
                while (result.Count > 0)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop++);
                    Console.WriteLine(result.Dequeue());
                }
                return true;
            }
            else
            {
                result.Enqueue($"{equipment.type}를 장착하고 있지 않습니다.");
                while (result.Count > 0)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop++);
                    Console.WriteLine(result.Dequeue());
                }
                return false;
            }
        }
    }
}
