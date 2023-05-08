using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class Mage : Player
    {
        public Mage(string name) : base(name)
        {
            job = Job.Mage;
            jobName = GetJobType();
            status.MaxHp = 950;
            status.MaxMp = 160;
            skills.Add(new FireballSkill());
        }
    }
}
