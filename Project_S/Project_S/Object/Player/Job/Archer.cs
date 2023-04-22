using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Archer : Player
    {
        public Archer()
        {
            job = Job.Archer;
            health = 1100;
            mana = 100;
            skills.Add(new ExplosiveArrow());
            skills.Add(new TripleShot());
        }

    }
}
