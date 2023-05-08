using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project_S.Player;

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
                    player.AddItemToInventory(new NormalSword());
                    break;
                case 'f':
                    player.AddItemToInventory(new FireSword());
                    break;
                case 'p':
                    player.AddItemToInventory(new HpPotion());
                    break;
                case '0':
                    WarriorJobChange(player);
                    break;
                case '9':
                    ArcherJobChange(player);
                    break;
                case '-':
                    LevelUp(player);
                    break;
                case '1':
                    InputInventory(gameState, player);
                    break;
                case '2':
                    InputEquipment(gameState, player);
                    break;
                case '3':

                    break;
                case '4':
                case '5':
                    InputSetting(gameState, player);
                    break;
                default:
                    break;
            }
        }

        private void InputInventory(GameState gameState, Player player)
        {
            Console.Clear();
            UI.currPlayer = player;
            UiState.GetInstance().SetUi("Inventory");
            UiState.GetInstance().prevState = gameState;
            Game.GetInstance().ChangeState(UiState.GetInstance());
        }

        private void InputEquipment(GameState gameState, Player player)
        {
            Console.Clear();
            UI.currPlayer = player;
            UiState.GetInstance().SetUi("Equipment");
            UiState.GetInstance().prevState = gameState;
            Game.GetInstance().ChangeState(UiState.GetInstance());
        }

        private void InputSetting(GameState gameState, Player player)
        {
            Console.Clear();
            UI.currPlayer = player;
            UiState.GetInstance().SetUi("Setting");
            UiState.GetInstance().prevState = gameState;
            Game.GetInstance().ChangeState(UiState.GetInstance());
        }

        private void WarriorJobChange(Player player)
        {
            Warrior warrior = new Warrior(player.nickname);
            warrior.level = player.level;
            warrior.pos = player.pos;
            warrior.status = player.status;
            warrior.inventory = player.inventory;
            warrior.wearingEquip = player.wearingEquip;
            warrior.skills = player.skills;
            warrior.status.MaxHp = 1600;
            warrior.status.MaxMp = 50;
            Scene.InGamePlayer = warrior;
        }

        private void ArcherJobChange(Player player)
        {
            Archer archer = new Archer(player.nickname);
            archer.level = player.level;
            archer.pos = player.pos;
            archer.status = player.status;
            archer.inventory = player.inventory;
            archer.wearingEquip = player.wearingEquip;
            archer.skills = player.skills;
            archer.status.MaxHp = 1100;
            archer.status.MaxMp = 100;
            archer.skills.Add(new ExplosiveArrow());
            archer.skills.Add(new TripleShot());
            Scene.InGamePlayer = archer;
        }

        private void LevelUp(Player player)
        {
            player.level++;
        }
    }
}
