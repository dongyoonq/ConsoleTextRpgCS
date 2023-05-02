using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Scene 클래스
    public class SceneManager
    {
        Dictionary<string, Scene> sceneMap;

        // 생성자
        private static SceneManager Inst;
        public static SceneManager GetInstance()
        {
            return Inst ??= new SceneManager();
        }

        // 장면 실행 메서드
        public bool Init()
        {
            sceneMap = new Dictionary<string, Scene>();

            if (!StartScene.GetInstance().Init())
                return false;
            AddScene("StartScene", StartScene.GetInstance());

            if (!PlayerCreateScene.GetInstance().Init())
                return false;
            AddScene("PlayerCreateScene", PlayerCreateScene.GetInstance());

            if (!Map01.GetInstance().Init())
                return false;
            AddScene("Map01", Map01.GetInstance());

            return true;
        }

        // 가지고 있는 장면들을 다른 클래스에 주는 메서드
        public Scene GetScene(string SceneName)
        {
            if(sceneMap.ContainsKey(SceneName))
                return sceneMap[SceneName];

            return null;
        }

        // 장면들을 등록하는 메서드
        private void AddScene(string SceneName, Scene scene)
        {
            sceneMap.Add(SceneName, scene);
        }
    }
}
