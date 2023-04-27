using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class TripleShot : Skill
    {
        public TripleShot()
        {
        }

        public int Damage { get; set; }

        public override void Use(Player player, Monster target)
        {
            if (player.level >= RequiredLevel && player.status.MaxMp >= RequiredMana)
            {
                target.health -= Damage;
                player.status.MaxMp -= RequiredLevel;
            }
        }
    }
}
