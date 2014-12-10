using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Programon
{
    class PopUpDialogueScreen : GameScreen
    {
        MenuComponent1 Menucomponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return Menucomponent.SelectedIndex; }
            set { Menucomponent.SelectedIndex = value; }
        }

        public PopUpDialogueScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image ) : base (game, spriteBatch)
        {
            string[] menuItems = { "Yes", "No" };
            Menucomponent = new MenuComponent1(game, spriteBatch, spriteFont, menuItems);
            Components.Add(Menucomponent);
            this.image = image;

            imageRectangle = new Rectangle(
                (Game.Window.ClientBounds.Width - this.image.Width) / 2,
                (Game.Window.ClientBounds.Height - this.image.Height) / 2,
                this.image.Width,
                this.image.Height);

            Menucomponent.Position = new Vector2(
                (imageRectangle.Width - Menucomponent.Width) / 2,
                imageRectangle.Bottom - Menucomponent.Height - 10);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, imageRectangle, Color.White);
            base.Draw(gameTime);
        }
    }
}
