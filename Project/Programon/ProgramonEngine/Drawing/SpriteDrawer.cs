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

        public GraphicsDeviceManager Graphics { get; set; }
        private IEnumerable<Node> FixedNodes { get; set; }

        public SpriteDrawer(Game GameWindow, Rectangle bounds, bool fullscreen)
        {
            Graphics = new GraphicsDeviceManager(GameWindow);
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.PreferredBackBufferWidth = bounds.Width;
            Graphics.PreferredBackBufferHeight = bounds.Height;
            Graphics.IsFullScreen = fullscreen;
            FixedNodes = new Node[0];
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.SpriteBatch = spriteBatch;
            DebugFont = content.Load<SpriteFont>("DebugFont");
        }

        /// <summary> Update the drawables based on the camera. </summary>
        public void Update(Dictionary<Vector2, Node> background, Camera cam, Actor player)
        {
            FixedNodes = background.Values.Where(n => n.Transform.IsBetweenBounds(cam.CameraWorld));
        }

        public void BeginDraw()
        {
            SpriteBatch.Begin();
        }

        /// <summary> Draw the background, player and special nodes. </summary>
        public void Draw(Camera camera, Actor player, List<Node> addedNodes = null)
        {
            DrawBackground(camera);
            DrawPlayer(player, camera);

            if (addedNodes != null)
                DrawNodes(addedNodes, camera);
        }

        private void DrawBackground(Camera cam)
        {
            foreach (Node cur in FixedNodes)
            {
                SpriteBatch.Draw(cur.Sprite.Texture,
                    cam.GetRelativePosition(cur.FixedPosition),
                    null,
                    cur.Sprite.Tint,
                    0f,
                    Vector2.Zero,
                    cur.Transform.Scale,
                    SpriteEffects.None,
                    0f);
            }
        }

        public void DrawNodes(List<Node> nodes, Camera cam)
        {
            foreach (Node cur in nodes)
            {
                SpriteBatch.Draw(cur.Sprite.Texture,
                    cam.GetRelativePosition(cur.FixedPosition),
                    null,
                    cur.Sprite.Tint,
                    0f,
                    Vector2.Zero,
                    cur.Transform.Scale,
                    SpriteEffects.None,
                    0f);
            }
        }

        private void DrawPlayer(Actor player, Camera cam)
        {
            SpriteBatch.Draw(player.Sprite.Texture,
                cam.GetRelativePosition(player.FixedPosition),
                null,
                player.Sprite.Tint,
                0f,
                Vector2.Zero,
                player.Transform.Scale,
                SpriteEffects.None,
                0f);
        }

        /// <summary> Draw the given menu. </summary>
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

        /// <summary> Set the window sizes. (WARNING: May cause screen tearing) </summary>
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
