using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Mage : Player
    {
        public Mage()
        {
            job = Job.Mage;
            health = 950;
            mana = 160;
            skills.Add(new FireballSkill());
        }

    }
}
