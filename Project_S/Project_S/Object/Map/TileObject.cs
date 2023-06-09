﻿using System;

namespace Project_D
{
    /// <summary>
    /// 이 클래스는 게임 맵 상의 오브젝트를 나타내기 위한 Object 클래스입니다.
    /// 각 Object는 위치(Pos)와 타일(Tile) 정보를 가지며, 위치와 타일 정보를 설정하고 가져오는 메소드를 제공합니다.
    /// 타일 정보에 따라 콘솔에 출력될 문자를 GetTileToChar() 메소드에서 설정하며, 각 타일 정보에 맞는 색상도 설정합니다.
    /// 즉, 이 클래스는 게임에서 오브젝트의 위치와 타일 정보를 관리하고, 해당 오브젝트가 콘솔에 어떻게 출력될지 결정하는 역할을 합니다.
    /// </summary>
    [Serializable]
    public class TileObject
    {
        public enum TILE
        {
            NONE,
            WALL,
            PLAYER,
            UIWall,
            UIWall_2,
            StatusWall,
            DEFAULT
        }

        [Serializable]
        public struct Pos
        {
            public int x;
            public int y;

            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private Pos _pos;
        private TILE _tile;

        public Pos pos { get { return _pos; } set { _pos = value; } }
        public TILE tile { get { return _tile; } set { _tile = value; } }
        public int posX { get { return _pos.x; } set { _pos.x = value; } }
        public int posY { get { return _pos.y; } set { _pos.y = value; } }

        public TileObject(Pos pos, TILE tile)
        {
            this.pos = pos;
            this.tile = tile;
        }

        /// <summary>
        /// 이 함수는 Object의 타일 종류(TILE)를 받아와 해당하는 콘솔 문자(char)를 반환하는 역할을 합니다. 
        /// switch문을 통해 각각의 TILE에 맞게 Console.ForegroundColor을 설정하고, 
        /// 그에 맞는 콘솔 문자를 변수 cTile에 할당한 후 반환합니다.
        /// </summary>
        /// <returns></returns>
        public char GetTileToChar()
        {
            char cTile = ' ';
            switch (tile)
            {
                case TILE.NONE:
                    cTile = '　';
                    break;
                case TILE.WALL:
                    Console.ForegroundColor = ConsoleColor.White;
                    cTile = '■';
                    break;
                case TILE.PLAYER:
                    Console.ForegroundColor = ConsoleColor.Red;
                    cTile = '★';
                    break;
                case TILE.UIWall:
                    cTile = '―';
                    break;
                case TILE.UIWall_2:
                    cTile = '▥';
                    break;
                case TILE.StatusWall:
                    cTile = '□';
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Black;
                    cTile = '■';
                    break;
            }

            return cTile;
        }
    }
}
