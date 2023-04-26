using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Archer : Player
    {
        public Archer(string name) : base(name)
        {
            job = Job.Archer;
            status.MaxHp = 1100;
            status.MaxMp = 100;
            skills.Add(new ExplosiveArrow());
            skills.Add(new TripleShot());
        }

    }
}
