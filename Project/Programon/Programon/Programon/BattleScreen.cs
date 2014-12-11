using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProgramonEngine;
using System.Collections.Generic;

namespace Programon
{
    class BattleScreen
    {
        protected MainWindow mainWindow;
        Creature Enemy;

        public List<Node> GuiList { get; private set; }

        Texture2D texture;

        public BattleScreen(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            GuiList = new List<Node>();


        }

        public virtual void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("TestGuiTextures/Testbox");
            GuiList.Add(new Node(new Vector2(1, 1), texture));
        }
    }
}
