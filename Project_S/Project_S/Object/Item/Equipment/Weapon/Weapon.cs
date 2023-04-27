using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    abstract class Weapon : Equipment
    {
        protected int attackPoint;
        protected int magicPoint;
        protected double attackSpeed;
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
    }
}
