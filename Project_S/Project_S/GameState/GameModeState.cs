using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_S
{
    // 게임모드에선 장면들에서 Input, Render, Update한다.
    public class GameModeState : GameState
    {
        // 플레이어 상태 클래스
        public class SaveGameModeState
        {
            public Scene currScene;

            public SaveGameModeState(Scene currScene)
            {
                this.currScene = currScene;
            }
        }

        private Scene _currScene;
        public Scene currScene {  get { return _currScene; } }

        private static GameModeState Inst;
        public static GameModeState GetInstance()
        {
            return Inst ??= new GameModeState();
        }

        public override void Input()
        {
            _currScene?.Input();
        }

        public override void Update()
        {
            _currScene?.Update();
        }

        public override void Render()
        {
            _currScene?.Render();
        }

        // SceneManager가 가지고있는 장면을 가져와 설정한다.
        public void SetScene(string SceneName)
        {
            _currScene = SceneManager.GetInstance().GetScene(SceneName);
        }

        // 세이브 기능
        public void Save()
        {
            SaveManager.GetInstance().saveData.Add(new GameStateMemento());
        }

        // 불러오기 기능
        public void Load(GameModeState state, GameStateMemento memento)
        {
            SaveGameModeState loadState = memento.GetGameModeState();

            state._currScene = loadState.currScene;
        }
    }

    [Serializable]
    public class GameStateMemento : IMemento
    {
        private readonly Scene _currScene;

        public GameStateMemento()
        {
            _currScene = GameModeState.GetInstance().currScene;
        }

        public string GetName()
        {
            return _currScene.ToString();
        }

        public string GetDate()
        {
            return DateTime.Now.ToString();
        }

        public GameModeState.SaveGameModeState GetGameModeState()
        {
            return new GameModeState.SaveGameModeState(_currScene);
        }
    }
}
