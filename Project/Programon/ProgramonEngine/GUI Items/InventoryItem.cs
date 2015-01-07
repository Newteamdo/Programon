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
        private Item item;
        private SpriteFont textFont;
        private Vector2 itemNameScale;
        private Vector2 itemDescriptionScale;
        private Vector2 itemNamePosition;
        private Vector2 itemDescPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        public InventoryItem()
        {
            this.item = new Item();
            this.itemRectangle = new Rectangle();
            this.imageRectangle = new Rectangle();
            this.textFont = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        /// <param name="ItemRectangle">The item rectangle. (Position and Size)</param>
        /// <param name="item">The item in the inventory.</param>
        /// <param name="font">The font to display text with.</param>
        public InventoryItem(Rectangle ItemRectangle, Item item, SpriteFont font)
        {
            this.item = item;
            this.itemRectangle = ItemRectangle;
            this.imageRectangle = new Rectangle();
            this.textFont = font;
            this.item.Description = StringExtensions.WrapText(item.Description, (itemRectangle.Width - imageRectangle.Width), textFont);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            imageRectangle = new Rectangle(itemRectangle.X, itemRectangle.Y, itemRectangle.Height, itemRectangle.Height);

            Vector2 size = textFont.MeasureString(item.Name);

            float itemWidth = size.X;
            float itemHeight = size.Y;
            float FontScaleItemW = (itemRectangle.Width - imageRectangle.Width) / itemWidth;

            if (FontScaleItemW >= 1f)
            {
                FontScaleItemW = 1f;
            }

            float FontScaleItemH = (itemRectangle.Height / 3) /itemHeight;

            itemWidth = itemWidth * FontScaleItemW;
            itemHeight = itemHeight * FontScaleItemH;

            itemNameScale = new Vector2(FontScaleItemW, FontScaleItemH);

            itemNamePosition = new Vector2(itemRectangle.X + imageRectangle.Width, itemRectangle.Y);
            itemDescPosition = new Vector2(itemRectangle.X + imageRectangle.Width, itemRectangle.Y + itemHeight);
            
            Vector2 descriptionSize = textFont.MeasureString(item.Description);

            float descriptionWidth = descriptionSize.X;
            float descriptionHeight = descriptionSize.Y;
            float FontScaleDescW = (itemRectangle.Width - imageRectangle.Width) / descriptionWidth;
            float FontScaleDescH = (itemRectangle.Height - itemHeight)/ descriptionHeight;

            descriptionWidth = descriptionWidth * FontScaleDescW;
            descriptionHeight = descriptionHeight * FontScaleDescH;

            itemDescriptionScale = new Vector2(FontScaleDescW, FontScaleDescH);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.FromStaticColor(Color.Black, Color.White, spriteBatch.GraphicsDevice).Texture, itemRectangle, Color.White);
            spriteBatch.Draw(item.Sprite.Texture, imageRectangle, Color.White);
            spriteBatch.DrawString(textFont, item.Name, itemNamePosition, Color.White, 0f,new Vector2(0,0), itemNameScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(textFont, item.Description, itemDescPosition, Color.White, 0f,new Vector2(0,0),itemDescriptionScale,SpriteEffects.None, 0);
        }
    }
}
