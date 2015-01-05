using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProgramonEngine;
using System.Collections.Generic;

namespace Programon
{
    public class BattleScreen : IMenu
    {
        protected MainWindow mainWindow;
        public ProgramonStatusBox[] ProgramonBoxList { get; private set; }
        public Creature Player { get; private set; }
        public Creature Enemy { get; private set; }

        public SelectionBox SelectionMenu;

        public AbilityBox AbilityMenu;

        public BattleState State;

        public BattleScreen(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            ProgramonBoxList = new ProgramonStatusBox[2];

            Stats eStats = new Stats(99, 100, 20, 20, 20, 20, 20);
            Stats pStats = new Stats(200, 300, 30, 30, 30, 30, 30);
            Player = new Creature(new Vector2(), "Hello", 100, pStats, pStats, new List<Ability>());
            Enemy = new Creature(new Vector2(), "World", 100, eStats, pStats, new List<Ability>());

            ProgramonBoxList[0] = new ProgramonStatusBox(new Vector2(0, 0), Player);
            ProgramonBoxList[1] = new ProgramonStatusBox(new Vector2(14, 5), Enemy);

            SelectionMenu = new SelectionBox(new Vector2(14, 8), new Vector2(1, 1));
            AbilityMenu = new AbilityBox(new Vector2(0, 8), new Vector2(0, 1), Player.abilities);
        }

        public virtual void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            SpriteFont font = content.Load<SpriteFont>("DebugFont");
            Texture2D texture = content.Load<Texture2D>("TestGuiTextures/Testbox");
            //Texture2D cornerTexture = content.Load<Texture2D>("TestGuiTextures/TestBattleTextures/BoxCorner");
            //Texture2D sideTexture = content.Load<Texture2D>("TestGuiTextures/TestBattleTextures/BoxMiddle");
            //Texture2D middleTexture = content.Load<Texture2D>("TestGuiTextures/TestBattleTextures/BoxCenter");
            foreach (ProgramonStatusBox battleBox in ProgramonBoxList)
            {
                battleBox.Load(graphicsDevice, font, texture);
            }

            SelectionMenu.Load(font, texture);
            AbilityMenu.Load(font, texture);
        }

        public IGuiItem[] Childs
        {
            get
            {
                IGuiItem[] IGuiItemList = new IGuiItem[4];
                ProgramonBoxList.CopyTo(IGuiItemList, 0);
                IGuiItemList[2] = SelectionMenu;
                IGuiItemList[3] = AbilityMenu;
                return IGuiItemList;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
