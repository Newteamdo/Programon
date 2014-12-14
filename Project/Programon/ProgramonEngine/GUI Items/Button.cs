using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProgramonEngine
{

    /// <summary>
    /// A class to display a button.
    /// </summary>
    public class Button : IGuiItem
    {
        public delegate void OnMouseClickEventHandler(Button btn);
        public event OnMouseClickEventHandler OnMouseClick;

        public delegate void OnMouseHoverEventHandler(Button btn);
        public event OnMouseHoverEventHandler OnMouseHover;

        public delegate void OnMouseEnterEventHandler(Button btn);
        public event OnMouseEnterEventHandler OnMouseEnter;

        public delegate void OnMouseLeaveEventHandler(Button btn);
        public event OnMouseLeaveEventHandler OnMouseLeave;

        private Rectangle btnRect;
        private Texture2D texture { get; set; }
        private Color Tint { get; set; }
        private Game Game { get; set; }
        public bool isEnabled { get; set; }

        private MouseState oldMouseState { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// With default values;
        /// </summary>
        public Button()
        {
            btnRect = new Rectangle(0, 0, 100, 100);
            Game = null;
            isEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="rect">The rectangle of the button.</param>
        /// <param name="texture">The texture of the button.</param>
        /// <param name="game">The game.</param>
        public Button(Rectangle rect, Texture2D texture, Game game)
        {
            isEnabled = true;
            btnRect = rect;
            this.texture = texture;
            Tint = Color.White;
            this.Game = game;
            oldMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Sets the position of the button.
        /// </summary>
        /// <param name="x">The new X position.</param>
        /// <param name="y">The new Y position.</param>
        public void SetPosition(int x, int y)
        {
            this.btnRect.X = x;
            this.btnRect.Y = y;
        }

        /// <summary>
        /// Sets the size of the button.
        /// </summary>
        /// <param name="width">The new width.</param>
        /// <param name="height">The new height.</param>
        public void SetSize(int width, int height)
        {
            this.btnRect.Width = width;
            this.btnRect.Height = height;
        }

        /// <summary>
        /// Gets the rectangle of the button.
        /// </summary>
        /// <returns>The rectangle of the button.</returns>
        public Rectangle GetRectangle()
        {
            return this.btnRect;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            Point oldMousePoint = new Point(oldMouseState.X, oldMouseState.Y);
            MouseState mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;
            Point mousePoint = new Point(mouseX, mouseY);

            if (isEnabled)
            {
                Tint = Color.White;
                Rectangle mouseRect = new Rectangle(mouseX, mouseY, 1, 1);
                if (mouse.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    if (mouseRect.Intersects(btnRect))
                    {
                        if (OnMouseClick != null)
                        {
                            OnMouseClick(this);
                        }
                    }
                }

                if (btnRect.Contains(mousePoint))
                {
                    if (OnMouseHover != null)
                    {
                        OnMouseHover(this);
                    }

                    if (!btnRect.Contains(oldMousePoint))
                    {
                        if (OnMouseEnter != null)
                            OnMouseEnter(this);
                    }
                }
                else if (btnRect.Contains(oldMousePoint))
                {
                    if (OnMouseLeave != null)
                        OnMouseLeave(this);
                }

                oldMouseState = mouse;
                oldMousePoint = new Point(oldMouseState.X, oldMouseState.Y);
            }
            else
            {
                Tint = Color.SlateGray;
            }

        }

        /// <summary>
        /// Draws the button with the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, btnRect, Tint);
        }
    }
}