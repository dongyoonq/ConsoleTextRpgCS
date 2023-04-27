using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Core
    {
        private GameState currentState;
        private static Core Inst;
        public static Core GetInstance()
        {
            return Inst ??= new Core();
        }

        public bool Init()
        {
            Console.CursorVisible = false;

            if (!EventManager.GetInstance().Init())
                return false;

            if (!InputManager.GetInstance().Init())
                return false;

            if (!InventoryManager.GetInstance().Init())
                return false;

            if (!ItemManager.GetInstance().Init())
                return false;

            if (!MonsterManager.GetInstance().Init())
                return false;

            if (!QuestManager.GetInstance().Init())
                return false;

            if (!SaveManager.GetInstance().Init())
                return false;

            if (!SceneManager.GetInstance().Init())
                return false;

            if (!SkillManager.GetInstance().Init())
                return false;

            if (!UiManager.GetInstance().Init())
                return false;

            if (!EffectManager.GetInstance().Init())
                return false;

            currentState = new MainState();

            return true;
        }

        public void Run()
        {
            while(true)
            {
                Input();

                Update();

                Render();
            }
        }

        public virtual void Input()
        {
            // 현재 상태에서 Input처리
            currentState.Input();
        }

        public virtual void Update()
        {
            // 현재 상태에서 Update처리
            currentState.Update();
        }

        public virtual void Render()
        {
            // 현재 상태에서 Render처리
            currentState.Render();
        }

        public void ChangeState(GameState newState)
        {
            // 상태 변경 메서드
            currentState = newState;
        }
    }
}
