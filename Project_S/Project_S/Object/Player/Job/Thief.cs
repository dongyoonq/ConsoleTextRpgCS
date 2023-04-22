using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Thief : Player
    {
        public Thief()
        {
            job = Job.Thief;
            health = 1200;
            mana = 90;
        }

    }
}
