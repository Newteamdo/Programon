using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProgramonEngine;

namespace Programon
{
    class MainWindow : Game
    {
        private SpriteDrawer spriteDrawer { get; set; }

        public MainWindow()
        {
            spriteDrawer = new SpriteDrawer(this, 1024, 720, false);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteDrawer.LoadContent(new SpriteBatch(GraphicsDevice));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            spriteDrawer.SpriteBatch.Begin();

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteDrawer.Draw();

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            spriteDrawer.SpriteBatch.End();

            base.EndDraw();
        }
    }
}
