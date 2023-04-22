using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Warrior : Player
    {
        public Warrior()
        {
            job = Job.Warrior;
            health = 1600;
            mana = 60;
        }

    }
}
