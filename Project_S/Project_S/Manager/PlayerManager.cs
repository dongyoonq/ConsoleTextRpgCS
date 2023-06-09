﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace Project_S
{
    public class PlayerManager
    {
        public List<Player> playerList;

        private static PlayerManager Inst;
        public static PlayerManager GetInstance()
        {
            return Inst ??= new PlayerManager();
        }

        public bool Init()
        {
            playerList = new List<Player> ();
            return true;
        }

        // 세이브 기능
        public void Save()
        {
            playerList[0] = Scene.InGamePlayer;
            SaveManager.GetInstance().saveData.Add(new PlayerMemento(Scene.InGamePlayer));
        }

        // 불러오기 기능
        public void Load(Player player, PlayerMemento memento)
        {
            Player.PlayerState state = memento.GetPlayerState();

            player.nickname = state.nickName;
            player.level = state.level;
            player.job = state.job;
            player.jobName = state.jobName;
            player.pos = state.pos;
            player.status = state.status;
            player.inventory = state.inventory;
            player.wearingEquip = state.wearingEquip;
            player.skills = state.skills;

        }
    }
}
