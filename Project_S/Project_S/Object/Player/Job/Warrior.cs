using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Warrior : Player
    {
        public Warrior(string name) : base(name)
        {
            job = Job.Warrior;
            status.MaxHp = 1600;
            status.MaxMp = 50;
        }
    }
}
