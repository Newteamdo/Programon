using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class Ability
    {
        public string name;
        public int EP;
        public int maxEP;
        public Buff selfBuff;
        public Buff enemyBuff;
        public int damage;
        public Ability(string name, int EP, int damage)
        {
            this.name = name;
            this.EP = EP;
            this.maxEP = EP;
            this.selfBuff = new Buff();
            this.enemyBuff = new Buff();
            this.damage = damage;
        }
        public Ability(string name, int EP, Buff selfBuff, Buff enemyBuff)
        {
            this.name = name;
            this.EP = EP;
            this.maxEP = EP;
            this.selfBuff = selfBuff;
            this.enemyBuff = enemyBuff;
            this.damage = 0;
        }
        public Ability(string name, int EP, Buff selfBuff, Buff enemyBuff, int damage)
        {
            this.name = name;
            this.EP = EP;
            this.maxEP = EP;
            this.selfBuff = selfBuff;
            this.enemyBuff = enemyBuff;
            this.damage = damage;
        }
    }
}
