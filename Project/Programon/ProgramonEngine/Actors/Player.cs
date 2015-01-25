using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Player : Actor
    {
        public List<Creature> programons = new List<Creature>();
        public Inventory Inventory { get; set; }
        public Inventory PortableComtakDevie { get; set; } // I'm using marco's class to display all programons.

        public Player(Vector2 startPos, Vector2 scale, Map currentMap)
            : base(startPos, scale, currentMap)
        {
            Inventory = new Inventory();
            PortableComtakDevie = new Inventory();
            CanEncounter = true;
        }

        public void AddProgramon(Creature programon)
        {
            programons.Add(programon);
        }

        public void RemoveProgramon(Creature programon)
        {
            programons.Remove(programon);
        }
    }
}
