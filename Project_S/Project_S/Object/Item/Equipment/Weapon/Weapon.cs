using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    [Serializable]
    abstract class Weapon : Equipment
    {
        public int attackPoint;
        public int magicPoint;
        public double attackSpeed;
        public WeaponType weaponType;

        protected Weapon()
        {
            type = ItemType.Weapon;
        }

        [Flags]
        public enum WeaponType
        {
            Common = Sword | Bow | Staff | Dagger,
            Sword = 1 << 0,
            Bow = 1 << 1,
            Staff = 1 << 2,
            Dagger = 1 << 3,
        }

        protected string GetJobType()
        {
            switch (weaponType)
            {
                case WeaponType.Common:
                    requireJob = "공용";
                    break;
                case WeaponType.Sword:
                    requireJob = "전사";
                    break;
                case WeaponType.Bow:
                    requireJob = "궁수";
                    break;
                case WeaponType.Dagger:
                    requireJob = "도적";
                    break;
                case WeaponType.Staff:
                    requireJob = "법사";
                    break;
            }

            return requireJob;
        }
    }
}
