using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    class BossMonster : Monster
    {
        public List<Skill> Skills { get; set; }

        public override void Attack(Player player)
        {
            player.health -= damage;
        }

        public void UseRandomSkill(Player player)
        {
            Skill skill = Skills[new Random().Next(0, Skills.Count)];
            skill.Use(player, this);
        }
    }
}
