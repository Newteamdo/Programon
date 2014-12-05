using Microsoft.Xna.Framework;

namespace ProgramonEngine
{
    public class Creature : Actor
    {
        public byte level;
        public int exp;
        public Stats programonStats;
        public Stats statsPerLevel;

        public Creature(Vector2 startPos, byte level, Stats programonStats, Stats statsPerLevel)
            :base(startPos)
        {
            this.level = level;
            this.programonStats = programonStats;
            this.statsPerLevel = statsPerLevel;
        }
    }
}
