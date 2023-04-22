using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    // Monster 클래스
    public abstract class Monster
    {
        public string name; // 몬스터 이름
        public int health; // 몬스터 체력
        public int damage; // 몬스터 데미지

        // 몬스터 공격 메서드
        public abstract void Attack(Player player);
    }
}
