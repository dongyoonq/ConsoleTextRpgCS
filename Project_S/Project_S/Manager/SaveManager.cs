using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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

            Console.SetCursorPosition(27, 3);
            Console.WriteLine("Game saved.");
            Task.Delay(1000);
        }

        // 저장된 게임 상태를 불러옴
        public void Load()
        {
            string path = Directory.GetCurrentDirectory();

            string[] files = Directory.GetFiles(path, "*.dat");

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

            Console.WriteLine("Game loaded.");
        }

        private void LoadPlayer(IMemento memento)
        {
            Player InGamePlayer = new Player("");
            PlayerManager.GetInstance().playerList.Add(InGamePlayer);
            EventManager.GetInstance().OnPlayerInputEventAdd();

            UI.GetInstance().Init();
        }

        private void LoadState(IMemento memento)
        {
            GameModeState.GetInstance().Load(GameModeState.GetInstance(), memento as GameStateMemento);
            Game.GetInstance().ChangeState(GameModeState.GetInstance());
            GameModeState.GetInstance().currScene.Init();
        }
    }
}
