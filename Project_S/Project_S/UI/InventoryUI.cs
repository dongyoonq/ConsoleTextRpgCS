using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class InventoryUI : UI
    {
        private static InventoryUI Inst;
        public new static InventoryUI GetInstance()
        {
            uiName = "Inventory";

            return Inst ??= new InventoryUI();
        }

        public override bool Init()
        {
            return SetMap(StringToChar(LoadFileToStringMap())) ? true : false;
        }

        public override void Input()
        {
            var key = Console.ReadKey();
        }

        public override void Update()
        {

        }

        public override void Render()
        {
            Console.Clear();

            Show();
        }

        protected override void Show()
        {
            ShowTileMap();
            DefaultRender();
        }

        private void DefaultRender()
        {
            Console.SetCursorPosition(8, 1);
            Console.WriteLine("인벤토리 창 입니다.");

            int i = 2;
            foreach (var item in currPlayer.inventory.list)
            {
                Console.SetCursorPosition(10, i+=2);
                Console.WriteLine(item.name);
            }
        }

        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileUI\Inventory.txt");

            string strMap = "";
            strMap = file.ReadToEnd();

            file.Close();

            return strMap;
        }

        protected override void ResetMap()
        {

        }
    }
}
