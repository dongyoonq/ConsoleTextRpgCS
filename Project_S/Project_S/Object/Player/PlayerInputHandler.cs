using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_S
{
    public class PlayerInputHandler
    {
        private static PlayerInputHandler Inst;
        public static PlayerInputHandler GetInstance()
        {
            return Inst ??= new PlayerInputHandler();
        }

        // 플레이어 콘솔 입력 이벤트 핸들러
        public void OnKeyPressed(Player player, char key, GameState gameState)
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
                case 'k':
                    player.inventory.list.Add(new NormalSword());
                    break;
                case '1':
                    Console.Clear();
                    Game.GetInstance().ChangeState(UiState.GetInstance());
                    UiState.GetInstance().SetUi("Inventory");
                    UiState.GetInstance().prevState = gameState;
                    break;
                case '2':

                    break;
                case '3':

                    break;
                case '4':
                case '5':
                    Console.Clear();
                    Game.GetInstance().ChangeState(UiState.GetInstance());
                    UiState.GetInstance().SetUi("Setting");
                    UiState.GetInstance().prevState = gameState;
                    break;
                default:
                    break;
            }
        }
    }
}
