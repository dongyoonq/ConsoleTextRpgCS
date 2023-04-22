using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class ExplosiveArrow : Skill
    {
        public int Damage { get; set; }

        public override void Use(Player player, Monster target)
        {
            if (player.level >= RequiredLevel && player.mana >= RequiredMana)
            {
                target.health -= Damage;
                player.mana -= RequiredMana;
            }
        }
    }
}
