using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ProgramonEngine
{
    public class Creature
    {
        public byte level;
        public int exp;
        public Stats programonStats;
        public Stats statsPerLevel;
        public Sprite programonSprite;

        public Creature(byte level, Stats programonStats, Stats statsPerLevel)
        {
            this.level = level;
            this.programonStats = programonStats;
            this.statsPerLevel = statsPerLevel;

        }
    }
}
