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
    public class Inventory : IMenu
    {
        private List<InventoryItem> invItem;
        private Dictionary<Item, int> items;
        private MainWindow mainWindow;
        private const int ITEMWIDTH = 500;
        private const int ITEMHEIGHT = 100;
        private SpriteFont font;

        public IGuiItem[] Childs
        {
            get
            {
                return invItem.ToArray();
            }
            set { }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inventory"/> class.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        public Inventory(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            font = mainWindow.Content.Load<SpriteFont>("DebugFont");
            items = new Dictionary<Item, int>();
            invItem = new List<InventoryItem>();

            for (int i = 0; i < 10; i++)
            {
                AddItem(new Item("Item " + i + ".", "This is a test description of Item " + i + ". Inventory description test. This is supposed to be working.", new Sprite(mainWindow.Content.Load<Texture2D>("No"))), i * 10);
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            CalculatePositions();
            for (int i = 0; i < invItem.Count; i++)
            {
                invItem[i].Update();
            }
        }

        /// <summary>
        /// Calculates the positions.
        /// </summary>
        private void CalculatePositions()
        {
            int row = 0;
            int column = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if ((row * ITEMHEIGHT) + ITEMHEIGHT > mainWindow.Window.ClientBounds.Height)
                {
                    row = 0;
                    column++;
                }
                int xPos = column * ITEMWIDTH;
                int yPos = row * ITEMHEIGHT;
                invItem[i].SetPosition(new Vector2(xPos, yPos));
                invItem[i].SetSize(new Vector2(ITEMWIDTH, ITEMHEIGHT));
                row++;
            }
        }

        /// <summary>
        /// Adds an item.
        /// If the amount is equal or lower than 0 the item won't be added.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="amount">The amount.</param>
        public void AddItem(Item item, int amount)
        {
            if (amount > 0)
            {
                if (items.ContainsKey(item))
                {
                    items[item] = amount;

                }
                else
                {
                    items.Add(item, amount);
                    invItem.Add(new InventoryItem(new Rectangle(0, 0, ITEMWIDTH, ITEMHEIGHT), item, amount, font));
                }
            }
        }

        /// <summary>
        /// Sets the amount of an item.
        /// If the amount is lower or equal than 0 the item gets removed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="amount">The amount.</param>
        public void SetAmount(Item item, int amount)
        {
            if (items.ContainsKey(item))
            {
                if (amount > 0)
                {
                    items[item] = amount;
                    foreach (InventoryItem inv in invItem)
                    {
                        if (inv.Item == item)
                        {
                            inv.SetAmount(amount);
                            break;
                        }
                    }
                }
                else
                {
                    RemoveItem(item);
                }
            }
        }

        /// <summary>
        /// Removes the item. If the inventory contains it.
        /// </summary>
        /// <param name="item">The item.</param>
        public void RemoveItem(Item item)
        {
            if (items.ContainsKey(item))
            {
                items.Remove(item);
                foreach (InventoryItem inv in invItem)
                {
                    if (inv.Item == item)
                    {
                        invItem.Remove(inv);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Searches the item with a specified name. Throws an <see cref="KeyNotFoundException"/> when not found.
        /// </summary>
        /// <param name="name">The name of the item to search.</param>
        /// <returns>A KeyValuePair with the Item and the amount./returns>
        /// <exception cref="KeyNotFoundException">The searched name doesn't exists in the inventory.</exception>
        public KeyValuePair<Item, int> GetItem(string name)
        {
            KeyValuePair<Item, int> foundItem = new KeyValuePair<Item, int>();
            foreach (KeyValuePair<Item, int> item in items)
            {
                if (item.Key.Name == name)
                {
                    foundItem = item;
                    break;
                }
            }

            if (foundItem.Equals(default(KeyValuePair<Item, int>)))
            {
                throw new KeyNotFoundException("The searched name doesn't exists in the inventory.");
            }

            return foundItem;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns>All the items in the inventory.</returns>
        public Dictionary<Item, int> GetItems()
        {
            return this.items;
        }

        /// <summary>
        /// Gets the amount of the item in the current inventory. Returns 0 if the item doesn't exists.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public int GetItemAmount(Item item)
        {
            if (items.ContainsKey(item))
            {
                return items[item];
            }

            return 0;
        }
    }
}
