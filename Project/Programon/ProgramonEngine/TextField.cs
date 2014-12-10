using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to display a text field.
    /// </summary>
    public class TextField
    {
        private string text { get; set; }
        private Color textColor { get; set; }
        private Color backgroundColor { get; set; }
        private Color borderColor { get; set; }

        private SpriteFont font { get; set; }
        private Rectangle fontRect { get; set; }
        private Rectangle borderRect { get; set; }

        private float fontScale { get; set; }

        private Rectangle rect;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// With default values.
        /// </summary>
        public TextField()
        {
            text = "Test";
            textColor = Color.Red;
            backgroundColor = Color.Red;
            borderColor = Color.Black;
            rect = new Rectangle(0, 0, 120, 20);
            font = null;
            fontRect = new Rectangle();
            borderRect = new Rectangle(0, 0, 234, 42);
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
            this.text = text;
            this.textColor = textColor;
            this.backgroundColor = backgroundColor;
            this.rect = rectangle;
            this.font = fontType;
            this.fontScale = 0.0f;

            this.borderColor = borderColor;

            CalculatePosition();
        }
        /// <summary>
        /// Draws the current text field to the screen, using the provided SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.FromStaticColor(borderColor, Color.White, spriteBatch.GraphicsDevice).Texture, rect, Color.White);
            spriteBatch.Draw(Sprite.FromStaticColor(backgroundColor, Color.White, spriteBatch.GraphicsDevice).Texture, borderRect, Color.White);

            spriteBatch.DrawString(font, text, new Vector2(fontRect.X, fontRect.Y), textColor, 0, new Vector2(0, 0), this.fontScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Sets the position of the text field.
        /// </summary>
        /// <param name="x">The new X position of the text field.</param>
        /// <param name="y">The new Y position of the text field.</param>
        public void SetPosition(int x, int y)
        {
            this.rect.X = x;
            this.rect.Y = y;
            CalculatePosition();
        }

        /// <summary>
        /// Sets the size of the text field.
        /// </summary>
        /// <param name="width">The new width of the text field.</param>
        /// <param name="height">The new height of the text field.</param>
        public void SetSize(int width, int height)
        {
            this.rect.Width = width;
            this.rect.Height = height;
            CalculatePosition();
        }

        /// <summary>
        /// Sets the text of the text field.
        /// </summary>
        /// <param name="text">The new text for the text field.</param>
        public void SetText(string text)
        {
            this.text = text;
            CalculatePosition();
        }

        /// <summary>
        /// Sets the color of the text for the text field.
        /// </summary>
        /// <param name="color">The new color for the text.</param>
        public void SetTextColor(Color color)
        {
            this.textColor = color;
        }

        /// <summary>
        /// Sets the background color for the text field.
        /// </summary>
        /// <param name="color">The new background color.</param>
        public void SetBackgroundColor(Color color)
        {
            this.backgroundColor = color;
        }

        /// <summary>
        /// Gets the text field rectangle.
        /// </summary>
        /// <returns>The rectangle from the text field.</returns>
        public Rectangle GetRectangle()
        {
            return this.rect;
        }

        /// <summary>
        /// Calculates the position of the border rectangle, font position and the font scale.
        /// </summary>
        private void CalculatePosition()
        {
            this.borderRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            Vector2 size = font.MeasureString(text);

            float width = size.X;
            float height = size.Y;

            if (rect.Height == 0)
            {
                this.fontScale = borderRect.Width / width;
            }
            else
            {
                this.fontScale = borderRect.Height / height;
            }

            width = width * fontScale;
            height = height * fontScale;

            float posX = (borderRect.Width / 2) - (width / 2) + borderRect.X;
            float posY = (borderRect.Height / 2) - (height / 2) + borderRect.Y + 2;

            fontRect = new Rectangle((int)posX, (int)posY, (int)width, (int)height);
        }
    }
}
