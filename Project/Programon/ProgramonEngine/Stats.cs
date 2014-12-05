namespace ProgramonEngine
{
    public struct Stats
    {
        public int health;
        public int maxHealth;
        public int attack;
        public int defence;
        public int sAttack;
        public int sDefence;
        public int speed;
        public Stats (int health, int maxHealth, int attack, int defence, int sAttack, int sDefence, int speed)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.attack = attack;
            this.defence = defence;
            this.sAttack = sAttack;
            this.sDefence = sDefence;
            this.speed = speed;
        }
    }
}
