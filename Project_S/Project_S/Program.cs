using System;

namespace Project_S
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            if(Core.GetInstance().Init())
                Core.GetInstance().Run();*/

            Core.GetInstance().Init();

            Player player = new Player("newPlayer");
            Equipment sword = new Sword();
            Equipment firesword = new FireSword();
            player.inventory.list.Add(sword);
            player.inventory.list.Add(firesword);

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(sword);

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(firesword);

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.UnEquip(sword);

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(sword);

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(firesword);

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();

            Console.WriteLine($"{sword.name} 버리기");
            player.inventory.list.Remove(sword);
            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();

            player.Equip(sword);
        }
    }
}
