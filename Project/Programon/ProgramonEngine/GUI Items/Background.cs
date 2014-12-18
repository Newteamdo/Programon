using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class Background:IGuiItem
    {
        public Rectangle Rectangle { get; set; }
        private Game Game { get; set; }
        private Texture2D Texture { get; set; }

        public Background()
        {
            Rectangle = new Rectangle(0, 0, 100, 100);
            Game = null;
            Texture = null;
        }

        public Background(Texture2D texture, int width, int height)
        {
            this.Rectangle = new Rectangle(0,0, width,height);
            this.Texture = texture;
        }

        public Background(Texture2D texture, Rectangle rect)
        {
            this.Rectangle = rect;
            this.Texture = texture;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Rectangle,Color.White);
        }
    }
}
