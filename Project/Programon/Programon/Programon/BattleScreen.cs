using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProgramonEngine;
using System.Collections.Generic;

namespace Programon
{
    public class BattleScreen
    {
        protected MainWindow mainWindow;
        public ProgramonStatusBox[] ProgramonBoxList { get; private set; }
        public Creature[] ProgramonList { get; private set; }

        public SelectionBox SelectionMenu;

        public AbilityBox AbilityMenu;

        Texture2D texture;

        public BattleState State;

        public BattleScreen(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            ProgramonBoxList = new ProgramonStatusBox[2];
            ProgramonList = new Creature[2];

            Stats eStats = new Stats(99, 100, 20, 20, 20, 20, 20);
            Stats pStats = new Stats(200, 300, 30, 30, 30, 30, 30);
            ProgramonList[0] = new Creature(new Vector2(), 100, eStats, eStats, new List<Ability>());
            ProgramonList[1] = new Creature(new Vector2(), 100, pStats, pStats, new List<Ability>());

            ProgramonBoxList[0] = new ProgramonStatusBox(new Vector2(1, 1));
            ProgramonBoxList[1] = new ProgramonStatusBox(new Vector2(43, 20));

            ProgramonList[1].abilities.Add( new Ability("TestAttack1", 20, 200));
            ProgramonList[1].abilities.Add(new Ability("TestAttack2", 20, 200));
            ProgramonList[1].abilities.Add(new Ability("TestAttack3", 20, 200));
            ProgramonList[1].abilities.Add(new Ability("TestAttack4", 20, 200));
            SelectionMenu = new SelectionBox(new Vector2(43, 34));
            AbilityMenu = new AbilityBox(new Vector2(1,34), ProgramonList[1].abilities);
        }

        public virtual void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("TestGuiTextures/Testbox");
            foreach (ProgramonStatusBox battleBox in ProgramonBoxList)
            {
                battleBox.SetTexture(texture);
            }
            SelectionMenu.SetTexture(texture);
            AbilityMenu.SetTexture(texture);
        }
    }
}
