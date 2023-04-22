using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Core
    {
        private static Core Inst;
        public static Core GetInstance()
        {
            return Inst ??= new Core();

        }

        public bool Init()
        {
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
            InputManager.GetInstance().HandleInput();
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }
    }
}
