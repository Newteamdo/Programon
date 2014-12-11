using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ProgramonEngine
{
    public class SpriteDrawer
    {        
        private SpriteBatch SpriteBatch { get; set; }
        private SpriteFont DebugFont { get; set; }

        private GraphicsDeviceManager Graphics { get; set; }
        private IEnumerable<Node> FixedNodes { get; set; }
        private Vector2 Offset { get; set; }

        public SpriteDrawer(Game GameWindow, int width, int height, bool fullscreen)
        {
            Graphics = new GraphicsDeviceManager(GameWindow);
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.IsFullScreen = fullscreen;
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.SpriteBatch = spriteBatch;
            DebugFont = content.Load<SpriteFont>("DebugFont");
        }

        public void Update(Dictionary<Vector2, Node> background, Rectangle drawPlane)
        {
            FixedNodes = background.Values.Where(n => n.Transform.IsBetweenBounds(drawPlane));
            Offset = new Vector2(drawPlane.X * Sprite.TextureWidth, drawPlane.Y * Sprite.TextureHeight);
        }

        public void BeginDraw()
        {
            SpriteBatch.Begin();
        }

        public void Draw(Rectangle drawPanel, List<Node> addedNodes = null)
        {
            DrawBackground();

            if (addedNodes != null) 
                DrawNodes(addedNodes);
        }
        public void DrawBattleScreen(List<Node> battleGui)
        {
            foreach (Node cur in battleGui)
            {
                SpriteBatch.Draw(cur.Sprite.Texture, cur.FixedPosition, cur.Sprite.Tint);
            }
        }

        private void DrawBackground()
        {
            foreach(Node cur in FixedNodes)
            {
                SpriteBatch.Draw(cur.Sprite.Texture, cur.FixedPosition + Offset, cur.Sprite.Tint);
            }
        }

        private void DrawNodes(List<Node> nodes)
        {
            foreach(Node cur in nodes)
            {
                SpriteBatch.Draw(cur.Sprite.Texture, cur.FixedPosition + Offset, cur.Sprite.Tint);
            }
        }

        public void DrawBattleGUI()
        {
            
        }

        public void EndDraw()
        {
            SpriteBatch.End();
        }
    }
}
