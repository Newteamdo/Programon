using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProgramonEngine
{
    public class Creature : Actor
    {
        public byte level;
        public int exp;
        public string name;
        public Stats programonBaseStats;
        public Stats programonTotalStats;
        public List<Buff> buffList = new List<Buff>();
        public Stats statsPerLevel;
        public List<Ability> abilities = new List<Ability>();

        public Creature(Vector2 startPos,string name, byte level, Stats programonStats, Stats statsPerLevel, List<Ability> abilities, Map currentMap)
            : base(startPos, Vector2.One, currentMap)
        {
            this.name = name;
            this.level = level;
            this.programonBaseStats = programonStats;
            this.statsPerLevel = statsPerLevel;
            this.abilities = abilities;
        }

        public void AddBuff(Buff buff)
        {
            buffList.Add(buff);
        }

        public void UpdateStats()
        {
            Stats buffSum = new Stats();
            foreach (Buff buff in buffList)
            {
                buffSum += buff.statChange;
            }
            programonTotalStats = buffSum + programonBaseStats;
        }

        public void UpdateBuffDuration()
        {
            foreach (Buff buff in buffList)
            {
                if (buff.duration > 0)
                {
                    buff.duration--;
                }
                else
                {
                    buffList.Remove(buff);
                }
            }
        }
    }
}
