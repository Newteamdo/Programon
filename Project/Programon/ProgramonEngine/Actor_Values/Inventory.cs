using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class Inventory
    {
        private Dictionary<Item, int> items;

        public Inventory()
        {
            items = new Dictionary<Item, int>();
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
            }
        }

        /// <summary>
        /// Searches an item with a specified name. Throws an <see cref="KeyNotFoundException"/> when not found.
        /// If the <paramref name="name"/> is more than once in the inventory, it returns the first result.
        /// </summary>
        /// <param name="name">The name of the item to search.</param>
        /// <returns>A KeyValuePair with the Item and the amount.</returns>
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
        /// Searches an item with the specified amount. Throws an <see cref="KeyNotFoundException"/> when not found. 
        /// If the <paramref name="amount"/> is more than once in the inventory, it returns the first result.
        /// </summary>
        /// <param name="amount">The amount of the item to search.</param>
        /// <returns>A KeyValuePair with the Item and the amount.</returns>
        /// <exception cref="KeyNotFoundException">The searched name doesn't exists in the inventory.</exception>
        public KeyValuePair<Item, int> GetItem(int amount)
        {
            KeyValuePair<Item, int> foundItem = new KeyValuePair<Item, int>();
            foreach (KeyValuePair<Item, int> item in items)
            {
                if (item.Value == amount)
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
