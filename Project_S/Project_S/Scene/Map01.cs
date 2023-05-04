using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_S
{
    [Serializable]
    public class Map01 : Scene
    {
        private Player InGamePlayer;

        private enum State
        {

        }

        private static Map01 Inst;
        public new static Map01 GetInstance()
        {
            sceneName = "Map01";
            return Inst ??= new Map01();
        }

        public override bool Init()
        {
            if (PlayerManager.GetInstance().playerList.Count != 0)
            {
                InGamePlayer = PlayerManager.GetInstance().playerList[0];
                InGamePlayer.pos = new Player.Pos(5, 8);
            }

            return SetMap(StringToChar(LoadFileToStringMap())) ? true : false;
        }

        public override void Input()
        {
            InputManager.GetInstance().PlayerInputHandle(InGamePlayer, this);

        }

        public override void Update()
        {
        }

        public override void Render()
        {
            Console.Clear();

            Show(this.InGamePlayer);
        }

        protected override void Show(Player player)
        {
            ShowTileMap();
            DefaultRender();
            UiManager.GetInstance().Show();
        }

        private void DefaultRender()
        {
            Console.SetCursorPosition(InGamePlayer.posX * 2, InGamePlayer.posY);
            Console.WriteLine("★");
        }

        protected override string LoadFileToStringMap()
        {
            StreamReader file = new StreamReader(@"..\..\..\Object\Map\TileMap\Map01.txt");

            string strMap = "";
            strMap = file.ReadToEnd();

            file.Close();

            return strMap;
        }

        protected override void ResetMap()
        {

        }

        private void NextState(int prevTop)
        {

        }

    }
}
