using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class BattleMechanics
    {
        public void PlayerAttack(Creature playerProgramon, Creature computerProgramon, Ability playerAbility)
        { 
            computerProgramon.AddBuff(playerAbility.enemyBuff);
            playerProgramon.AddBuff(playerAbility.selfBuff);
            computerProgramon.UpdateStats();
            playerProgramon.UpdateStats();
            computerProgramon.programonBaseStats.health-=Math.Min((playerAbility.damage+playerProgramon.programonTotalStats.attack-computerProgramon.programonTotalStats.defence),0);
        }

        public void ComputerAttack(Creature playerProgramon, Creature computerProgramon)
        {
            AI battleAi = new AI();
            Ability computerAbility;
            computerAbility = battleAi.UseAbility(playerProgramon, computerProgramon);
            computerProgramon.AddBuff(computerAbility.selfBuff);
            playerProgramon.AddBuff(computerAbility.enemyBuff);
            computerProgramon.UpdateStats();
            playerProgramon.UpdateStats();
            playerProgramon.programonBaseStats.health -= Math.Min((computerAbility.damage + computerProgramon.programonTotalStats.attack - playerProgramon.programonTotalStats.defence), 0);
            UpdateBuffDurations(playerProgramon,computerProgramon);
        }

        private void UpdateBuffDurations(Creature playerProgramon, Creature computerProgramon)
        {
            playerProgramon.UpdateBuffDuration();
            computerProgramon.UpdateBuffDuration();
        }
    }
}
