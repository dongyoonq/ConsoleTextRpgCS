using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Thief : Player
    {
        public Thief(string name) : base(name)
        {
            job = Job.Thief;
            status.MaxHp = 1200;
            status.MaxMp = 90;
        }

    }
}
