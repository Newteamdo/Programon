using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class AI
    {
        public Ability UseAbility(Creature playerProgramon, Creature computerProgramon)
        {
            int rnd = new Random().Next(0, computerProgramon.abilities.Count - 1);
            return computerProgramon.abilities[rnd];
        }
    }
}
