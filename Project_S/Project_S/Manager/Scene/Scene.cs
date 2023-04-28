using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class Scene : GameState
    {
        protected static string sceneName;

        private static Scene Inst;
        protected static Scene GetInstance()
        {
            sceneName = "Default";
            return Inst ??= new Scene();
        }

        public virtual void Show(Player player) { }
        //Console.SetCursorPosition(player.pos.x * 2, player.pos.y);
        //Console.WriteLine("O");

        protected virtual void Show() { }
    }
}
