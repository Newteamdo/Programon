using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ProgramonEngine
{
    public class SpriteDrawer
    {
        public Rectangle BufferSize { get { return Graphics.GraphicsDevice.Viewport.Bounds; } private set { } }
        public bool Debug { get; set; }

        public SpriteBatch SpriteBatch { get; set; }
        private SpriteFont DebugFont { get; set; }

        private GraphicsDeviceManager Graphics { get; set; }
        private Node[] FixedNodes { get; set; }

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
            DebugFont = content.Load<SpriteFont>("Fonts/GuiFont_Small");
        }

        /// <summary> Update the drawables based on the camera. </summary>
        public void Update(Dictionary<Vector2, Node> background, Camera cam)
        {
            FixedNodes = background.Values.Where(n => n.Transform.IsBetweenBounds(cam.CameraWorld)).ToArray();
        }

        public void BeginDraw()
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
        }

        /// <summary> Draw the background, player and special nodes. </summary>
        public void Draw(Camera camera, Player player, IEnumerable<Node> addedNodes = null, IEnumerable<Actor> actors = null)
        {
            DrawBackground(camera);

            if (actors != null)
                DrawActors(actors, camera);

            DrawPlayer(player, camera);

            if (addedNodes != null)
                DrawNodes(addedNodes, camera);
        }

        private void DrawBackground(Camera cam)
        {
            for (int i = 0; i < FixedNodes.Count(); i++)
            {
                Node cur = FixedNodes.ElementAt(i);

                SpriteBatch.Draw(cur.Sprite.Texture,
                    cam.GetRelativePosition(cur.FixedPosition),
                    null,
                    cur.Sprite.Tint,
                    cur.Transform.Rotation,
                    Vector2.Zero,
                    cur.Transform.Scale,
                    SpriteEffects.None,
                    0f);

                if (Debug)
                {
                    SpriteBatch.DrawString(DebugFont, cur.Transform.Position.ToString(), cam.GetRelativePosition(cur.FixedPosition), Color.Black);
                    SpriteBatch.DrawString(DebugFont, "W: " + cur.Walkable, cam.GetRelativePosition(cur.FixedPosition + new Vector2(0, 12)), cur.Walkable ? Color.Black : Color.Red);
                    SpriteBatch.DrawString(DebugFont, "T: " + cur.IsTallGrass, cam.GetRelativePosition(cur.FixedPosition + new Vector2(0, 24)), cur.IsTallGrass ? Color.Red : Color.Black);
                }
            }
        }

        public void DrawNodes(IEnumerable<Node> nodes, Camera cam)
        {
            for (int i = 0; i < nodes.Count(); i++)
            {
                Node cur = nodes.ElementAt(i);

                SpriteBatch.Draw(cur.Sprite.Texture,
                    cam.GetRelativePosition(cur.FixedPosition),
                    null,
                    cur.Sprite.Tint,
                    cur.Transform.Rotation,
                    Vector2.Zero,
                    cur.Transform.Scale,
                    SpriteEffects.None,
                    0f);
            }
        }

        private void DrawPlayer(Player player, Camera cam)
        {
            if (player.IsWalking)
            {
                Sprite frame;
                switch (player.Direction)
                {
                    case Direction.LEFT:
                    case Direction.RIGHT:
                        frame = player.Animations[AnimationTypes.WalkingLeft].NextFrame();
                        break;

                    case Direction.UP:
                        frame = player.Animations[AnimationTypes.WalkingUp].NextFrame();
                        break;

                    case Direction.DOWN:
                        frame = player.Animations[AnimationTypes.WalkingDown].NextFrame();
                        break;

                    default:
                        frame = player.Animations[AnimationTypes.WalkingLeft].NextFrame();
                        break;
                }

                switch (player.Direction)
                {
                    case Direction.RIGHT:
                        SpriteBatch.Draw(frame.Texture,
                        cam.GetRelativePosition(player.FixedPosition),
                        null,
                        frame.Tint,
                        0f,
                        Vector2.Zero,
                        player.Transform.Scale,
                        SpriteEffects.FlipHorizontally,
                        0f);
                        break;
                    case Direction.UP:
                    case Direction.DOWN:
                    case Direction.LEFT:
                    default:
                        SpriteBatch.Draw(frame.Texture,
                        cam.GetRelativePosition(player.FixedPosition),
                        null,
                        frame.Tint,
                        0f,
                        Vector2.Zero,
                        player.Transform.Scale,
                        SpriteEffects.None,
                        0f);
                        break;
                }
            }
            else
            {
                switch (player.Direction)
                {
                    case Direction.RIGHT:
                        SpriteBatch.Draw(player.Sprite.Texture,
                        cam.GetRelativePosition(player.FixedPosition),
                        null,
                        player.Sprite.Tint,
                        0f,
                        Vector2.Zero,
                        player.Transform.Scale,
                        SpriteEffects.FlipHorizontally,
                        0f);
                        break;
                    case Direction.DOWN:
                    case Direction.UP:
                    case Direction.LEFT:
                    default:
                        SpriteBatch.Draw(player.Sprite.Texture,
                        cam.GetRelativePosition(player.FixedPosition),
                        null,
                        player.Sprite.Tint,
                        0f,
                        Vector2.Zero,
                        player.Transform.Scale,
                        SpriteEffects.None,
                        0f);
                        break;
                }
            }
        }

        private void DrawActors(IEnumerable<Actor> actors, Camera cam)
        {
            for (int i = 0; i < actors.Count(); i++)
            {
                Actor cur = actors.ElementAt(i);
                if (cur is NPC)
                {
                    NPC current = cur as NPC;
                    if (current.DrawDialog)
                    {
                        DrawGUIItem(current.Dialogue);
                    }
                }

                if (cur.IsWalking)
                {
                    Sprite frame;
                    switch (cur.Direction)
                    {
                        case Direction.LEFT:
                        case Direction.RIGHT:
                            if (cur.Animations.ContainsKey(AnimationTypes.WalkingLeft))
                            {
                                frame = cur.Animations[AnimationTypes.WalkingLeft].NextFrame();
                            }
                            else
                            {
                                frame = cur.Sprite;
                            }
                            break;

                        case Direction.UP:
                            if (cur.Animations.ContainsKey(AnimationTypes.WalkingUp))
                            {
                                frame = cur.Animations[AnimationTypes.WalkingUp].NextFrame();
                            }
                            else
                            {
                                frame = cur.Sprite;
                            }
                            break;

                        case Direction.DOWN:
                            if (cur.Animations.ContainsKey(AnimationTypes.WalkingDown))
                            {
                                frame = cur.Animations[AnimationTypes.WalkingDown].NextFrame();
                            }
                            else
                            {
                                frame = cur.Sprite;
                            }
                            break;

                        default:
                            if (cur.Animations.ContainsKey(AnimationTypes.WalkingLeft))
                            {
                                frame = cur.Animations[AnimationTypes.WalkingLeft].NextFrame();
                            }
                            else
                            {
                                frame = cur.Sprite;
                            }
                            break;
                    }

                    switch (cur.Direction)
                    {
                        case Direction.RIGHT:
                            SpriteBatch.Draw(frame.Texture,
                            cam.GetRelativePosition(cur.FixedPosition),
                            null,
                            frame.Tint,
                            0f,
                            Vector2.Zero,
                            cur.Transform.Scale,
                            SpriteEffects.FlipHorizontally,
                            0f);
                            break;
                        case Direction.UP:
                        case Direction.DOWN:
                        case Direction.LEFT:
                        default:
                            SpriteBatch.Draw(frame.Texture,
                            cam.GetRelativePosition(cur.FixedPosition),
                            null,
                            frame.Tint,
                            0f,
                            Vector2.Zero,
                            cur.Transform.Scale,
                            SpriteEffects.None,
                            0f);
                            break;
                    }
                }
                else
                {
                    switch (cur.Direction)
                    {
                        case Direction.RIGHT:
                            SpriteBatch.Draw(cur.Sprite.Texture,
                            cam.GetRelativePosition(cur.FixedPosition),
                            null,
                            cur.Sprite.Tint,
                            0f,
                            Vector2.Zero,
                            cur.Transform.Scale,
                            SpriteEffects.FlipHorizontally,
                            0f);
                            break;
                        case Direction.DOWN:
                        case Direction.UP:
                        case Direction.LEFT:
                        default:
                            SpriteBatch.Draw(cur.Sprite.Texture,
                            cam.GetRelativePosition(cur.FixedPosition),
                            null,
                            cur.Sprite.Tint,
                            0f,
                            Vector2.Zero,
                            cur.Transform.Scale,
                            SpriteEffects.None,
                            0f);
                            break;
                    }
                }
            }
        }

        /// <summary> Draw the given GUI item. </summary>
        public void DrawGUIItem(IGuiItem item)
        {
            item.Draw(SpriteBatch);
        }

        /// <summary> Draw the given menu. </summary>
        public void DrawGUI(IMenu menu)
        {
            for (int i = 0; i < menu.Childs.Length; i++)
            {
                menu.Childs[i].Draw(SpriteBatch);
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
    }
}
