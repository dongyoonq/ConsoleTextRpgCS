using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class UiManager
    {
        Dictionary<string, UI> UiTable;

        private static UiManager Inst;
        public static UiManager GetInstance()
        {
            return Inst ??= new UiManager();
        }

        public bool Init()
        {
            UiTable = new Dictionary<string, UI>();

            if (!InventoryUI.GetInstance().Init())
                return false;
            AddUi("Inventory", InventoryUI.GetInstance());

            if (!EquipmentUI.GetInstance().Init())
                return false;
            AddUi("Equipment", EquipmentUI.GetInstance());

            if (!StatusUI.GetInstance().Init())
                return false;
            AddUi("Status", StatusUI.GetInstance());

            if (!SettingUI.GetInstance().Init())
                return false;
            AddUi("Setting", SettingUI.GetInstance());

            return true;
        }

        // Base UI Show
        public void Show()
        {

            string[] Ui = { "  _   _ ___ ",
                             " | | | |_ _|",
                             " | | | || | ",
                             " | |_| || | ",
                             "  \\___/|___|" };
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("=========================================================================");
            foreach(string key in Ui)
                Console.WriteLine(key);
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("　[ 1. 인벤토리 ]　[ 2. 장비 ]　[ 3. 스텟 ]　[ 4. 퀘스트 ]　[ 5. 설정 ]\n");
            Console.WriteLine("=========================================================================");
        }

        // 가지고 있는 장면들을 다른 클래스에 주는 메서드
        public UI GetUi(string UiName)
        {
            if (UiTable.ContainsKey(UiName))
                return UiTable[UiName];

            return null;
        }

        // 장면들을 등록하는 메서드
        private void AddUi(string UiName, UI ui)
        {
            UiTable.Add(UiName, ui);
        }
    }
}
