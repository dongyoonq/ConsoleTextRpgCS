using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    public class SkillManager
    {
        private static SkillManager Inst;
        public static SkillManager GetInstance()
        {
            return Inst ??= new SkillManager();
        }

        public bool Init()
        {
            return true;
        }
    }
}
