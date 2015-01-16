using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramonEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Programon
{
    /// <summary>
    /// A class to create an inventory.
    /// </summary>
    public class InventoryMenu : IMenu
    {
        private List<InventoryItem> InventoryItems;
        private MainWindow mainWindow;
        private const int ITEMWIDTH = 500;
        private const int ITEMHEIGHT = 100;
        private SpriteFont font;
        private Player player;

        public IGuiItem[] Childs
        {
            get
            {
                return InventoryItems.ToArray();
            }
            set { }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryMenu"/> class.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        public InventoryMenu(Player player, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.player = player;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            font = mainWindow.Content.Load<SpriteFont>("Fonts/DebugFont");
            InventoryItems = new List<InventoryItem>();
            if (mainWindow.State == GameState.INVENTORY)
            {
                for (int i = 0; i < player.Inventory.GetItems().Count; i++)
                {
                    InventoryItems.Add(new InventoryItem(new Rectangle(0, 0, ITEMWIDTH, ITEMHEIGHT), player.Inventory.GetItems().Keys.ElementAt(i), player.Inventory.GetItems().Values.ElementAt(i), font));
                }
            }
            else if (mainWindow.State == GameState.PORTABLECOMTAKDEVICE)
            {
                for (int i = 0; i < player.Inventory.GetItems().Count; i++)
                {
                    InventoryItems.Add(new InventoryItem(new Rectangle(0, 0, ITEMWIDTH, ITEMHEIGHT), player.PortableComtakDevie.GetItems().Keys.ElementAt(i), player.PortableComtakDevie.GetItems().Values.ElementAt(i), font));
                }
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            InventoryItems.Clear();

            if (mainWindow.State == GameState.INVENTORY)
            {
                for (int i = 0; i < player.Inventory.GetItems().Count; i++)
                {
                    InventoryItems.Add(new InventoryItem(new Rectangle(0, 0, ITEMWIDTH, ITEMHEIGHT), player.Inventory.GetItems().Keys.ElementAt(i), player.Inventory.GetItems().Values.ElementAt(i), font));
                }
            }
            else if(mainWindow.State == GameState.PORTABLECOMTAKDEVICE)
            {
                for (int i = 0; i < player.PortableComtakDevie.GetItems().Count; i++)
                {
                    InventoryItems.Add(new InventoryItem(new Rectangle(0, 0, ITEMWIDTH, ITEMHEIGHT), player.PortableComtakDevie.GetItems().Keys.ElementAt(i), player.PortableComtakDevie.GetItems().Values.ElementAt(i), font));
                }
            }
            CalculatePositions();
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                InventoryItems[i].Update();
            }
        }

        /// <summary>
        /// Calculates the positions.
        /// </summary>
        private void CalculatePositions()
        {
            int row = 0;
            int column = 0;
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if ((row * ITEMHEIGHT) + ITEMHEIGHT > mainWindow.Window.ClientBounds.Height)
                {
                    row = 0;
                    column++;
                }
                int xPos = column * ITEMWIDTH;
                int yPos = row * ITEMHEIGHT;
                InventoryItems[i].SetPosition(new Vector2(xPos, yPos));
                InventoryItems[i].SetSize(new Vector2(ITEMWIDTH, ITEMHEIGHT));
                row++;
            }
        }
    }
}
