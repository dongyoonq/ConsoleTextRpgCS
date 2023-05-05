using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    [Serializable]
    abstract class Weapon : Equipment
    {
        protected int attackPoint;
        protected int magicPoint;
        protected double attackSpeed;
        protected string requireJob;
        public WeaponType weaponType;

        protected Weapon()
        {
            type = ItemType.Weapon;
        }

        public enum WeaponType
        {
            Common,
            Sword,
            Staff,
            Bow,
            Dagger
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
