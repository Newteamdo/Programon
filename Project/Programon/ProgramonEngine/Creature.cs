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
        public Stats programonBaseStats;
        public Stats programonTotalStats;
        public List<Buff> buffList= new List<Buff>();
        public Stats statsPerLevel;
        public List<Ability> abilities = new List<Ability>();

        public Creature(Vector2 startPos, byte level, Stats programonStats, Stats statsPerLevel,List<Ability> abilities)
            :base(startPos)
        {
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
                if (buff.duration>0)
                {
                    buff.duration--;
                    buffSum += buff.statChange; 
                }
                else
                {
                    buffList.Remove(buff);     
                }
            }
            programonTotalStats = buffSum + programonBaseStats;
        }
    }
}
