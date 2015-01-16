using System;

namespace ProgramonEngine
{
    public static class BattleMechanics
    {
        public static void PlayerAttack(ref Creature playerProgramon, ref Creature computerProgramon, Ability playerAbility)
        {
            computerProgramon.AddBuff(playerAbility.enemyBuff);
            playerProgramon.AddBuff(playerAbility.selfBuff);
            computerProgramon.UpdateStats();
            playerProgramon.UpdateStats();
            computerProgramon.programonBaseStats.health -= Math.Max((playerAbility.damage + playerProgramon.programonTotalStats.attack - computerProgramon.programonTotalStats.defence), 0);
            if (computerProgramon.programonBaseStats.health < 0) computerProgramon.programonBaseStats.health = 0;
        }

        public static void ComputerAttack(ref Creature playerProgramon, ref Creature computerProgramon)
        {
            AI battleAi = new AI();
            Ability computerAbility;
            computerAbility = battleAi.UseRndAbility(playerProgramon, computerProgramon);
            computerProgramon.AddBuff(computerAbility.selfBuff);
            playerProgramon.AddBuff(computerAbility.enemyBuff);
            computerProgramon.UpdateStats();
            playerProgramon.UpdateStats();
            playerProgramon.programonBaseStats.health -= Math.Min((computerAbility.damage + computerProgramon.programonTotalStats.attack - playerProgramon.programonTotalStats.defence), 0);
        }

        public static void UpdateBuffDurations(ref Creature playerProgramon, ref Creature computerProgramon)
        {
            playerProgramon.UpdateBuffDuration();
            computerProgramon.UpdateBuffDuration();
        }

        public static void Recruit(ref Creature computerProgramon, ref Player player)
        {
            player.AddProgramon(computerProgramon);
        }
    }
}
