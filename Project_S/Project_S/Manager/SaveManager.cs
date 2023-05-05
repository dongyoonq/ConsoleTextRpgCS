using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Project_S
{
    // Memento Pattern
    public class SaveManager
    {
        public List<IMemento> saveData;

        private static SaveManager Inst;
        public static SaveManager GetInstance()
        {
            return Inst ??= new SaveManager();
        }

        public bool Init()
        {
            saveData = new List<IMemento>();
            return true;
        }

        public void Save()
        {
            for(int i = 0; i < saveData.Count; i++)
            {
                using (FileStream fs = new FileStream($"{saveData[i].ToString()}.dat", FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, saveData[i]);
                }
            }

            for(int i = 0; i < 6; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(31, 3);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Save Loading");

                for (int j = 0; j < i % 3 + 1; j++)
                {
                    Console.Write(".");
                }

                System.Threading.Thread.Sleep(1000);

            }

            Console.Clear();
            Console.SetCursorPosition(29, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Game Save Completed !");
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(2000);
        }

        // 저장된 게임 상태를 불러옴
        public void Load()
        {
            string path = Directory.GetCurrentDirectory();

            string[] files = Directory.GetFiles(path, "*.dat");

            Game.GetInstance().Init();

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        IMemento memento = (IMemento)bf.Deserialize(fs);

                        switch(memento)
                        {
                            case PlayerMemento:
                                LoadPlayer(memento); break;
                            case GameStateMemento:
                                LoadState(memento); break;
                            default:break;


                        }

                    }
                }
                else
                {
                    Console.WriteLine($"save file : {file}.dat not found.");
                }
            }


            for (int i = 0; i < 6; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(31, 3);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("SaveFile Loading");

                for (int j = 0; j < i % 3 + 1; j++)
                {
                    Console.Write(".");
                }

                System.Threading.Thread.Sleep(1000);
            }

            Console.Clear();
            Console.SetCursorPosition(29, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Game Load Completed !");
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(2000);
        }

        private void LoadPlayer(IMemento memento)
        {
            Player InGamePlayer = new Player("");
            PlayerManager.GetInstance().Load(InGamePlayer, memento as PlayerMemento);
            PlayerManager.GetInstance().playerList.Add(InGamePlayer);
            EventManager.GetInstance().OnPlayerInputEventAdd();
            EventManager.GetInstance().OnPlayerEquipEventAdd();
        }

        private void LoadState(IMemento memento)
        {
            GameModeState.GetInstance().Load(GameModeState.GetInstance(), memento as GameStateMemento);
            Game.GetInstance().ChangeState(GameModeState.GetInstance());
            GameModeState.GetInstance().currScene.Init();
        }
    }
}
