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
        private Vector2 CameraOffset { get; set; }
        private Vector2 ScreenCenter { get; set; }

        public GraphicsDeviceManager Graphics { get; set; }
        private IEnumerable<Node> FixedNodes { get; set; }

        public SpriteDrawer(Game GameWindow, Rectangle bounds, bool fullscreen)
        {
            Graphics = new GraphicsDeviceManager(GameWindow);
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.PreferredBackBufferWidth = bounds.Width;
            Graphics.PreferredBackBufferHeight = bounds.Height;
            ScreenCenter = new Vector2(bounds.Width >> 1, bounds.Height >> 1);
            Graphics.IsFullScreen = fullscreen;
            FixedNodes = new Node[0];
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.SpriteBatch = spriteBatch;
            DebugFont = content.Load<SpriteFont>("DebugFont");
        }

        public void Update(Dictionary<Vector2, Node> background, Rectangle drawPlane, Actor player)
        {
            FixedNodes = background.Values.Where(n => n.Transform.IsBetweenBounds(drawPlane));

            CameraOffset = new Vector2(drawPlane.X * Sprite.TextureWidth, drawPlane.Y * Sprite.TextureHeight);
        }

        public void BeginDraw()
        {
            SpriteBatch.Begin();
        }

        public void Draw(Actor player, List<Node> addedNodes = null)
        {
            DrawBackground();
            DrawPlayer(player);

            if (addedNodes != null)
                DrawNodes(addedNodes);
        }

        private void DrawBackground()
        {
            foreach (Node cur in FixedNodes)
            {
                SpriteBatch.Draw(cur.Sprite.Texture, cur.FixedPosition + CameraOffset, cur.Sprite.Tint);
            }
        }

        public void DrawNodes(List<Node> nodes)
        {
            foreach (Node cur in nodes)
            {
                SpriteBatch.Draw(cur.Sprite.Texture, cur.FixedPosition + CameraOffset, cur.Sprite.Tint);
            }
        }

        private void DrawPlayer(Actor player)
        {
            SpriteBatch.Draw(player.Sprite.Texture, player.FixedPosition + CameraOffset, player.Sprite.Tint);

            SpriteBatch.DrawString(DebugFont, player.Transform.Position.ToString(), Vector2.Zero, Color.White);
        }

        public void DrawGUI(IMenu menu)
        {
            foreach (IGuiItem cur in menu.Childs)
            {
                cur.Draw(SpriteBatch);
            }
        }

        public void EndDraw()
        {
            SpriteBatch.End();
        }

        public void SetWindowSize(int width, int height)
        {
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.ApplyChanges();
        }

        public int GetWindowWidth()
        {
            return Graphics.PreferredBackBufferWidth;
        }

        public int GetWindowHeight()
        {
            return Graphics.PreferredBackBufferHeight;
        }
    }
}
