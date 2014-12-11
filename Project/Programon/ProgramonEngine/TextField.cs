using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to display a text field.
    /// </summary>
    public class TextField
    {
        private string Text { get; set; }
        private Color TextColor { get; set; }
        private Color BackgroundColor { get; set; }
        private Color BorderColor { get; set; }

        private SpriteFont Font { get; set; }
        private Rectangle FontRect { get; set; }
        private Rectangle BorderRect { get; set; }

        private Rectangle backgroundRect;

        private float FontScale { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// With default values.
        /// </summary>
        public TextField()
        {
            Text = "Test";
            TextColor = Color.Red;
            BackgroundColor = Color.Red;
            BorderColor = Color.Black;
            backgroundRect = new Rectangle(0, 0, 120, 20);
            Font = null;
            FontRect = new Rectangle();
            BorderRect = new Rectangle(0, 0, 234, 42);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        /// <param name="text">The text in the text field.</param>
        /// <param name="textColor">The text color for the text field.</param>
        /// <param name="backgroundColor">The background color of the text field.</param>
        /// <param name="borderColor">The border color of the text field.</param>
        /// <param name="rectangle">The rectangle (Position and Size) of the text field.</param>
        /// <param name="fontType">The font to use for the text in the text field.</param>
        public TextField(string text, Color textColor, Color backgroundColor, Color borderColor, Rectangle rectangle, SpriteFont fontType)
        {
            this.Text = text;
            this.TextColor = textColor;
            this.BackgroundColor = backgroundColor;
            this.backgroundRect = rectangle;
            this.Font = fontType;
            this.FontScale = 0.0f;

            this.BorderColor = borderColor;

            CalculatePosition();
        }
        /// <summary>
        /// Draws the current text field to the screen, using the provided SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.FromStaticColor(BorderColor, Color.White, spriteBatch.GraphicsDevice).Texture, backgroundRect, Color.White);
            spriteBatch.Draw(Sprite.FromStaticColor(BackgroundColor, Color.White, spriteBatch.GraphicsDevice).Texture, BorderRect, Color.White);

            spriteBatch.DrawString(Font, Text, new Vector2(FontRect.X, FontRect.Y), TextColor, 0, new Vector2(0, 0), this.FontScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Sets the position of the text field.
        /// </summary>
        /// <param name="x">The new X position of the text field.</param>
        /// <param name="y">The new Y position of the text field.</param>
        public void SetPosition(int x, int y)
        {
            this.backgroundRect.X = x;
            this.backgroundRect.Y = y;
            CalculatePosition();
        }

        /// <summary>
        /// Sets the size of the text field.
        /// </summary>
        /// <param name="width">The new width of the text field.</param>
        /// <param name="height">The new height of the text field.</param>
        public void SetSize(int width, int height)
        {
            this.backgroundRect.Width = width;
            this.backgroundRect.Height = height;
            CalculatePosition();
        }

        /// <summary>
        /// Sets the text of the text field.
        /// </summary>
        /// <param name="text">The new text for the text field.</param>
        public void SetText(string text)
        {
            this.Text = text;
            CalculatePosition();
        }

        /// <summary>
        /// Sets the color of the text for the text field.
        /// </summary>
        /// <param name="color">The new color for the text.</param>
        public void SetTextColor(Color color)
        {
            this.TextColor = color;
        }

        /// <summary>
        /// Sets the background color for the text field.
        /// </summary>
        /// <param name="color">The new background color.</param>
        public void SetBackgroundColor(Color color)
        {
            this.BackgroundColor = color;
        }

        /// <summary>
        /// Gets the text field rectangle.
        /// </summary>
        /// <returns>The rectangle from the text field.</returns>
        public Rectangle GetRectangle()
        {
            return this.backgroundRect;
        }

        /// <summary>
        /// Calculates the position of the border rectangle, font position and the font scale.
        /// </summary>
        private void CalculatePosition()
        {
            this.BorderRect = new Rectangle(backgroundRect.X + 1, backgroundRect.Y + 1, backgroundRect.Width - 2, backgroundRect.Height - 2);

            Vector2 size = Font.MeasureString(Text);

            float width = size.X;
            float height = size.Y;

            if (backgroundRect.Height == 0)
            {
                this.FontScale = BorderRect.Width / width;
            }
            else
            {
                this.FontScale = BorderRect.Height / height;
            }

            width = width * FontScale;
            height = height * FontScale;

            float posX = (BorderRect.Width / 2) - (width / 2) + BorderRect.X;
            float posY = (BorderRect.Height / 2) - (height / 2) + BorderRect.Y + 2;

            FontRect = new Rectangle((int)posX, (int)posY, (int)width, (int)height);
        }
    }
}
