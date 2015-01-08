using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to create an item.
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int healthToRestore;
        public float EPToRestore;
        public Sprite Sprite { get; set; }
        public Stats StatChange { get; set; }
        public Buff BuffOnUse { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            this.Name = "";
            this.Description = "";
            healthToRestore = 0;
            EPToRestore = 0;
            this.Sprite = new Sprite();
            this.StatChange = new Stats();
            this.BuffOnUse = new Buff();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="description">The description of the item.</param>
        /// <param name="sprite">The sprite of the item.</param>
        public Item(string name,string description, Sprite sprite)
        {
            this.Name = name;
            this.Description = description;
            healthToRestore = 0;
            EPToRestore = 0;
            this.Sprite = sprite;
            this.StatChange = new Stats();
            this.BuffOnUse = new Buff();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="description">The description of the item.</param>
        /// <param name="sprite">The sprite of the item.</param>
        /// <param name="statChange">The stats change.</param>
        /// <param name="buffOnUse">The buff on use.</param>
        public Item(string name, string description, int health, float EP, Sprite sprite, Stats statChange, Buff buffOnUse)
        {
            this.Name = name;
            this.Description = description;
            this.healthToRestore = health;
            this.EPToRestore = EP;
            this.Sprite = sprite;
            this.StatChange = statChange;
            this.BuffOnUse = buffOnUse;
        }


        public void RestoreEP(Ability ability, Item item)
        {
            ability.EP = Math.Min(ability.EP+(int)(ability.maxEP * item.EPToRestore / 100),ability.maxEP);
        }

        public void UseItem(Creature programon, Item item)
        {
            programon.programonBaseStats.health = Math.Min(programon.programonBaseStats.health + item.healthToRestore, programon.programonBaseStats.maxHealth);
            programon.programonBaseStats += item.StatChange;
            programon.buffList.Add(item.BuffOnUse);
        }
    }
}
