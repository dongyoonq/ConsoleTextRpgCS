using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class PlayerInputHandler
    {
        private ICommand _command;

        private static PlayerInputHandler Inst;
        public static PlayerInputHandler GetInstance()
        {
            return Inst ??= new PlayerInputHandler();
        }

        // 플레이어 콘솔 입력 이벤트 핸들러
        public void OnKeyPressed(Player player, char key)
        {
            switch (key)
            {
                case 'a':
                    player.posX--;
                    break;
                case 'd':
                    player.posX++;
                    break;
                case 'w':
                    player.posY--;
                    break;
                case 's':
                    player.posY++;
                    break;
                default:
                    break;
            }
        }
    }
}
