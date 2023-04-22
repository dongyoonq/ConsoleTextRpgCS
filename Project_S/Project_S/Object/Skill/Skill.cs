using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Skill 클래스
    public abstract class Skill
    {
        public string Name { get; set; }
        public int RequiredLevel { get; set; }
        public int RequiredMana { get; set; }

        // ... 기타 필요한 속성 및 메서드
        public abstract void Use(Player player, Monster target);
    }

}
