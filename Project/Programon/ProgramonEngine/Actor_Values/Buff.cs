using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Buff
    {
        public string name;
        public int duration;
        public Stats statChange;

        public Buff()
        {
            this.name = "";
            this.duration = 0;
            this.statChange = new Stats();
        }

        public Buff(string name, int duration, Stats statChange)
        {
            this.name = name;
            this.duration = duration;
            this.statChange = statChange;
        }

        public static Buff Load(ContentManager content, string name)
        {
            return content.Load<Buff>(name);
        }
    }
}
