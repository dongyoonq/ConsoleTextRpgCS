using Project_D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project_D.TileObject;

namespace Project_S
{
    public class Scene : GameState
    {
        protected TileObject[,] tileMap;
        protected static string sceneName;

        private static Scene Inst;
        protected static Scene GetInstance()
        {
            sceneName = "Default";
            return Inst ??= new Scene();
        }

        public virtual void Show(Player player) { }
        //Console.SetCursorPosition(player._pos.x * 2, player._pos.y);
        //Console.WriteLine("O");

        protected virtual void Show() { }
        protected virtual void ShowTileMap() 
        {
            for (int y = 0; y < tileMap.GetLength(0); y++)
            {
                for (int x = 0; x < tileMap.GetLength(1); x++)
                {
                    Console.Write(tileMap[y, x].GetTileToChar());
                }
                Console.WriteLine();
            }
        }

        protected bool SetMap(char[,] map)
        {
            tileMap = new TileObject[map.GetLength(0), map.GetLength(1)];
            // 맵에 빈공간이나, 정해놓은 문자 이외 다른 문자가 들어갔을 경우 false를 리턴
            foreach (char c in map)
                if (string.IsNullOrEmpty(c.ToString()) && c != '■' && c != '　' && c != '▩' && c != '□' && c != '▣')
                    return false;

            for (int y = 0; y < tileMap.GetLength(0); y++)
            {
                for (int x = 0; x < tileMap.GetLength(1); x++)
                {
                    tileMap[y, x] = new TileObject(new Pos(0, 0), TILE.DEFAULT);
                }
            }

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    tileMap[y, x].pos = new Pos(y, x);
                    switch (map[y, x])
                    {
                        case '　':
                            tileMap[y, x].tile = TILE.NONE;
                            break;
                        case '■':
                            tileMap[y, x].tile = TILE.WALL;
                            break;
                        case '★':
                            tileMap[y, x].tile = TILE.PLAYER;
                            break;
                        default:
                            break;
                    }
                }
            }

            return true;
        }
        protected virtual void ResetMap()
        {
            
            tileMap = new TileObject[tileMap.GetLength(0), tileMap.GetLength(1)];
            for (int y = 0; y < tileMap.GetLength(0); y++)
            {
                for (int x = 0; x < tileMap.GetLength(1); x++)
                {
                    tileMap[y, x] = new TileObject(new Pos(0, 0), TILE.DEFAULT);
                }
            }
        }

        protected virtual string LoadFileToStringMap() { return null; }
        protected virtual char[,] StringToChar(string str)
        {
            string[] readLineStr = str.Split("\r\n");
            char[,] newMap = new char[readLineStr.Length, readLineStr[0].Length];
            for (int y = 0; y < readLineStr.Length; y++)
            {
                for (int x = 0; x < readLineStr[y].Length; x++)
                {
                    newMap[y, x] = readLineStr[y][x];
                }
            }

            return newMap;
        }
    }
}
