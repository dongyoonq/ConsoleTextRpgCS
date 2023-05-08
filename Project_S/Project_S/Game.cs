using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Game
    {
        private GameState currentState;
        private bool reset = false;

        private static Game Inst;
        public static Game GetInstance()
        {
            return Inst ??= new Game();
        }

        public void Run()
        {
            while (true)
            {
                Input();

                Update();

                Render();
            }
        }

        public bool Init()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            if (!EventManager.GetInstance().Init())
                return false;

            if (!InputManager.GetInstance().Init())
                return false;

            if (!InventoryManager.GetInstance().Init())
                return false;

            if (!ItemManager.GetInstance().Init())
                return false;

            if (!PlayerManager.GetInstance().Init())
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

            currentState ??= new MainState();
            currentState.Render();

            return true;
        }

        public virtual void Input()
        {
            if (reset)
                reset = false;
            // 현재 상태에서 Input처리
            currentState.Input();
        }

        public virtual void Update()
        {
            if (reset)
                return;
            // 현재 상태에서 Update처리
            currentState.Update();
        }

        public virtual void Render()
        {
            if (reset)
                return;
            // 현재 상태에서 Render처리
            currentState.Render();
        }

        public void ChangeState(GameState newState)
        {
            // 상태 변경 메서드
            currentState = newState;
        }

        public void RestartState()
        {
            reset = true;
        }
    }
}
