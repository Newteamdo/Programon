using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Stats
    {
        public int health;
        public int maxHealth;
        public int attack;
        public int defence;
        public int sAttack;
        public int sDefence;
        public int speed;

        public Stats()
        {
            this.health = 0;
            this.maxHealth = 0;
            this.attack = 0;
            this.defence = 0;
            this.sAttack = 0;
            this.sDefence = 0;
            this.speed = 0;
        }

        public Stats(int health, int maxHealth, int attack, int defence, int sAttack, int sDefence, int speed)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.attack = attack;
            this.defence = defence;
            this.sAttack = sAttack;
            this.sDefence = sDefence;
            this.speed = speed;
        }

        public static Stats Load(ContentManager content, string name)
        {
            return content.Load<Stats>(name);
        }

        public static Stats operator +(Stats sender, Stats adder)
        {
            return new Stats(
                sender.health + adder.health,
                sender.maxHealth + adder.maxHealth,
                sender.attack + adder.attack,
                sender.defence + adder.defence,
                sender.sAttack + adder.sAttack,
                sender.sDefence + adder.sDefence,
                sender.speed + adder.speed
                );
        }

        public static Stats operator -(Stats sender, Stats adder)
        {
            return new Stats(
                sender.health - adder.health,
                sender.maxHealth - adder.maxHealth,
                sender.attack - adder.attack,
                sender.defence - adder.defence,
                sender.sAttack - adder.sAttack,
                sender.sDefence - adder.sDefence,
                sender.speed - adder.speed
                );
        }
    }
}
