using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    class NormalMonster : Monster
    {
        public override void Attack(Player player)
        {
            player.status.MaxHp -= damage;
        }
    }
}
