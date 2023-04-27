using System;

namespace Project_S
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("newPlayer");
            if (Core.GetInstance().Init())
                Core.GetInstance().Run();

            //Core.GetInstance().Init();

            //Player player = new Player("newPlayer");
            Player archer = new Archer("보우맨");
            Equipment sword = new NormalSword();
            Equipment firesword = new FireSword();
            player.AddItemToInventory(sword);
            player.AddItemToInventory(firesword);

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(sword);
            player.UseWeapon();

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(firesword);
            player.UseWeapon();

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.UnEquip(sword);
            player.UseWeapon();

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(sword);
            player.UseWeapon();

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            player.Equip(firesword);
            player.UseWeapon();

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();

            Console.WriteLine($"{sword.name} 버리기");
            player.RemoveItemFromInventory(sword);
            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();

            player.Equip(sword);
            player.UseWeapon();

            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();
            Equipment bow = new Bow();
            player.Equip(bow);

            player.AddItemToInventory(bow);
            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();

            player.Equip(bow);
            Console.Write("[ 현재 인벤토리 ] ");
            foreach (var item in player.inventory.list)
                Console.Write($"{item.name}, ");
            Console.WriteLine();
            Console.Write("[ 현재 장착중인 아이템 ] ");
            foreach (var item in player.wearingEquip)
                Console.Write($"{item.Value.name}, ");
            Console.WriteLine();

            archer.AddItemToInventory(sword);
            archer.AddItemToInventory(firesword);
            archer.AddItemToInventory(bow);

            archer.Equip(sword);
            archer.Equip(firesword);
            archer.Equip(bow);
        }
    }
}
