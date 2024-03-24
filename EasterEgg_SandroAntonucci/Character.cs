using System;

namespace EasterEgg
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HealthPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Character() { } 

        public Character(string name, int level, int healthPoints, int attack, int defense)
        {
            Name = name;
            Level = level;
            HealthPoints = healthPoints;
            Attack = attack;
            Defense = defense;
        }

    }
}

