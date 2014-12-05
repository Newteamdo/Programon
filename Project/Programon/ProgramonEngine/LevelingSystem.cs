using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public static class LevelingSystem
    {
        private const int startingExp = 100;
        public static void CheckForLevelup(Creature programon)
        {
            bool canLevel = true;
            while (canLevel)
            {
                if (programon.exp >= Math.Pow(programon.level, 1.337) * startingExp)
                {
                    Levelup(programon);
                }
                else
                {
                    canLevel = false;
                }
            }
        }
        public static void Levelup(Creature programon)
        {
            programon.level++;
            programon.programonStats = programon.programonStats + programon.statsPerLevel;
        }
    }
}
