using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to display an item from the inventory.
    /// </summary>
    public class InventoryItem : IGuiItem
    {
        private Rectangle imageRectangle;
        private Rectangle itemRectangle;
        public Item Item { get; private set; }
        private int amount;
        private SpriteFont textFont;
        private Vector2 itemNamePosition;
        private Vector2 itemNameScale;
        private Vector2 itemDescPosition;
        private Vector2 itemDescScale;
        private Vector2 itemAmountPosition;
        private Vector2 itemAmountScale;
        private string itemDescriptionText;
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        public InventoryItem()
        {
            this.Item = new Item();
            this.itemNamePosition = new Vector2();
            this.itemNameScale = new Vector2();
            this.itemDescPosition = new Vector2();
            this.itemDescScale = new Vector2();
            this.itemAmountPosition = new Vector2();
            this.itemAmountScale = new Vector2();
            this.amount = 0;
            this.itemRectangle = new Rectangle();
            this.imageRectangle = new Rectangle();
            this.textFont = null;
            this.itemDescriptionText = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        /// <param name="ItemRectangle">The item rectangle. (Position and Size)</param>
        /// <param name="item">The item in the inventory.</param>
        /// <param name="font">The font to display text with.</param>
        public InventoryItem(Rectangle ItemRectangle, Item item, int amount, SpriteFont font)
        {
            this.Item = item;
            this.amount = amount;
            this.itemRectangle = ItemRectangle;
            this.imageRectangle = new Rectangle();
            this.textFont = font;
            this.itemDescriptionText = StringExtensions.WrapText(item.Description, (itemRectangle.Width - imageRectangle.Width), textFont);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            imageRectangle = new Rectangle(itemRectangle.X, itemRectangle.Y, itemRectangle.Height, itemRectangle.Height);

            Vector2 size = textFont.MeasureString(Item.Name);

            float itemWidth = size.X;
            float itemHeight = size.Y;
            float FontScaleItemW = (itemRectangle.Width - imageRectangle.Width) / itemWidth;

            if (FontScaleItemW >= 1f)
            {
                FontScaleItemW = 1f;
            }

            float FontScaleItemH = (itemRectangle.Height / 3) / itemHeight;

            itemWidth = itemWidth * FontScaleItemW;
            itemHeight = itemHeight * FontScaleItemH;

            itemNameScale = new Vector2(FontScaleItemW, FontScaleItemH);

            itemNamePosition = new Vector2(itemRectangle.X + imageRectangle.Width, itemRectangle.Y);
            itemDescPosition = new Vector2(itemRectangle.X + imageRectangle.Width, itemRectangle.Y + itemHeight);

            Vector2 descriptionSize = textFont.MeasureString(itemDescriptionText);

            float descriptionWidth = descriptionSize.X;
            float descriptionHeight = descriptionSize.Y;
            float FontScaleDescW = (itemRectangle.Width - imageRectangle.Width) / descriptionWidth;
            float FontScaleDescH = (itemRectangle.Height - itemHeight) / descriptionHeight;

            descriptionWidth = descriptionWidth * FontScaleDescW;
            descriptionHeight = descriptionHeight * FontScaleDescH;

            itemDescScale = new Vector2(FontScaleDescW, FontScaleDescH);

            Vector2 amountSize = textFont.MeasureString("x" + amount);
            float amountWidth = amountSize.X;
            float amountHeight = amountSize.Y;

            float FontScaleAmountW = (imageRectangle.Width / 2) / amountWidth;
            float FontScaleAmountH = (imageRectangle.Height / 6) / amountHeight;

            amountWidth = amountWidth * FontScaleAmountW;
            amountHeight = amountHeight * FontScaleAmountH;

            itemAmountScale = new Vector2(FontScaleAmountW, FontScaleAmountH);
            itemAmountPosition = new Vector2((imageRectangle.X + imageRectangle.Height) - amountWidth, (imageRectangle.Y + imageRectangle.Height) - amountHeight);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.FromStaticColor(Color.Black, Color.White, spriteBatch.GraphicsDevice).Texture, itemRectangle, Color.White);
            spriteBatch.Draw(Sprite.FromStaticColor(Color.Yellow, Color.White, spriteBatch.GraphicsDevice).Texture, imageRectangle, Color.White);
            spriteBatch.DrawString(textFont, "x" + amount, itemAmountPosition, Color.Red, 0f, new Vector2(0, 0), itemAmountScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(textFont, Item.Name, itemNamePosition, Color.White, 0f, new Vector2(0, 0), itemNameScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(textFont, itemDescriptionText, itemDescPosition, Color.White, 0f, new Vector2(0, 0), itemDescScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Sets the amount of the item.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void SetAmount(int amount)
        {
            this.amount = amount;
        }

        /// <summary>
        /// Sets the position.
        /// </summary>
        /// <param name="position">The new position.</param>
        public void SetPosition(Vector2 position)
        {
            this.itemRectangle.X = (int) position.X;
            this.itemRectangle.Y = (int) position.Y;
        }

        /// <summary>
        /// Sets the size.
        /// </summary>
        /// <param name="size">The new size.</param>
        public void SetSize(Vector2 size)
        {
            this.itemRectangle.Width = (int) size.X;
            this.itemRectangle.Height = (int) size.Y;
        }
    }
}
