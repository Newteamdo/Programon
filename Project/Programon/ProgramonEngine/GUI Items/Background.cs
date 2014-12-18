using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to display a background.
    /// </summary>
    public class Background:IGuiItem
    {
        public Rectangle Rectangle { get; set; }
        private Game Game { get; set; }
        private Texture2D Texture { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Background"/> class.
        /// </summary>
        public Background()
        {
            Rectangle = new Rectangle(0, 0, 100, 100);
            Game = null;
            Texture = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Background"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Background(Texture2D texture, int width, int height)
        {
            this.Rectangle = new Rectangle(0,0, width,height);
            this.Texture = texture;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Background"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="rect">The rectangle.</param>
        public Background(Texture2D texture, Rectangle rect)
        {
            this.Rectangle = rect;
            this.Texture = texture;
        }

        /// <summary>
        /// Sets the width.
        /// </summary>
        /// <param name="width">The width.</param>
        public void SetWidth(int width)
        {
            this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, width, this.Rectangle.Height);
        }

        /// <summary>
        /// Sets the height.
        /// </summary>
        /// <param name="height">The height.</param>
        public void SetHeight(int height)
        {
            this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, this.Rectangle.Width, height);
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Rectangle,Color.White);
        }
    }
}
