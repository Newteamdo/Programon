using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class SpriteDrawer
    {

        private Game GameWindow { get; set; }
        public SpriteBatch SpriteBatch { get; private set; }
        private GraphicsDeviceManager Graphics { get; set; }

        public SpriteDrawer(Game game, int width, int height, bool fullscreen)
        {
            this.GameWindow = game;
            Graphics = new GraphicsDeviceManager(GameWindow);
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.IsFullScreen = fullscreen;
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
        }

        public void Draw()
        {
            DrawNodes();
            DrawGUI();
        }

        private void DrawNodes()
        {
            //Node drawing will happen here
        }
        private void DrawGUI()
        {
            //GUI drawing will happen here
        }

    }
}
