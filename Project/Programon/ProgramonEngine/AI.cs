using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class AI
    {
        private enum AbilityTypes { Healing, Defencive, Utility, Offencive, Debuff }

        public Ability UseRndAbility(Creature playerProgramon, Creature computerProgramon)
        {
            int rnd = new Random().Next(0, computerProgramon.abilities.Length - 1);
            return computerProgramon.abilities[rnd];
        }

        private Dictionary<AbilityTypes, Ability[]> SplitAbilities(IEnumerable<Ability> abilities)
        {
            Ability[] healing = abilities.Where(a => a.selfBuff.statChange.health > 0).ToArray();
            Ability[] defencive = abilities.Where(a => a.selfBuff.statChange.sDefence > 0).ToArray();
            Ability[] utility = abilities.Where(a => a.selfBuff.statChange.sAttack > 0 || a.selfBuff.statChange.speed > 0).ToArray();

            Ability[] offencive = abilities.Where(a => a.enemyBuff.statChange.health < 0).ToArray();
            Ability[] debuff = abilities.Where(a => a.enemyBuff.statChange.maxHealth < 0 ||
                            a.enemyBuff.statChange.sAttack < 0 ||
                            a.enemyBuff.statChange.sDefence < 0 ||
                            a.enemyBuff.statChange.speed < 0).ToArray();

            Dictionary<AbilityTypes, Ability[]> value = new Dictionary<AbilityTypes, Ability[]>();

            if (healing.Length > 0) value.Add(AbilityTypes.Healing, healing);
            if (defencive.Length > 0) value.Add(AbilityTypes.Defencive, defencive);
            if (utility.Length > 0) value.Add(AbilityTypes.Utility, utility);
            if (offencive.Length > 0) value.Add(AbilityTypes.Offencive, offencive);
            if (debuff.Length > 0) value.Add(AbilityTypes.Debuff, debuff);

            return value;
        }
    }
}
