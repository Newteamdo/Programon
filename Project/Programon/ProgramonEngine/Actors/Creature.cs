using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProgramonEngine
{
    [Serializable]
    public class Creature : Actor
    {
        public byte level;
        public int exp;
        public string name;
        public Stats programonBaseStats;
        public Stats programonTotalStats;
        [System.Xml.Serialization.XmlIgnore]
        public List<Buff> buffList = new List<Buff>();
        public Stats statsPerLevel;
        public Ability[] abilities;
        public string description;

        protected internal Creature()
        {
            this.level = 0x00;
            this.exp = 0;
            this.name = "";
            this.programonBaseStats = new Stats();
            this.programonTotalStats = new Stats();
            this.statsPerLevel = new Stats();
        }

        public Creature(Vector2 startPos, string name, byte level, Stats programonStats, Stats statsPerLevel, Ability[] abilities, Map currentMap, string description)
            : base(startPos, Vector2.One, currentMap)
        {
            this.name = name;
            this.level = level;
            this.programonBaseStats = programonStats;
            this.statsPerLevel = statsPerLevel;
            this.abilities = abilities;
            this.description = description;
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
            for (int i = 0; i < buffList.Count; i++)
            {
                Buff buff = buffList[i];

                if (buff.duration > 0)
                {
                    buff.duration--;
                }
                else
                {
                    buffList.RemoveAt(i);
                }
            }
        }

        public static void GetBattleXp(Creature enemy, Creature friendly)
        {
            friendly.exp = enemy.level * 2;
        }           
    }
}
