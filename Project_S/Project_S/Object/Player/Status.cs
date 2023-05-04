using System;
using System.Collections.Generic;
using System.Text;

namespace Project_S
{
    [Serializable]
    public class Status
    {
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public int AttackPoint { get; set; }
        public int MagicPoint { get; set; }
        public double AttackSpeed { get; set; }
        public int Defense { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Luck { get; set; }
        public double Exp { get; set; }
    }
}
