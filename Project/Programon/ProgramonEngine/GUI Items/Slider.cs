using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to display a slider.
    /// </summary>
    public class Slider : IGuiItem
    {
        const int DISTANCESLIDERTEXTY = 10;

        public delegate void OnMouseHoldEventHandler(Slider slider, MouseState mouseState);
        public event OnMouseHoldEventHandler OnMouseHold;

        private int MinValue { get; set; }
        private int MaxValue { get; set; }
        private double CurrentValue { get; set; }

        private Rectangle backgroundRectangle;
        private Rectangle sliderRectangle;
        private Rectangle FontRectangle { get; set; }

        private double ValuePerPixel { get; set; }
        private float FontScale { get; set; }

        private Color TextColor { get; set; }
        private SpriteFont TextFont { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class.
        /// With default values.
        /// </summary>
        public Slider()
        {
            this.MinValue = 0;
            this.MaxValue = 0;
            this.CurrentValue = 0;

            this.backgroundRectangle = new Rectangle(0, 10, 100, 20);
            this.sliderRectangle = new Rectangle(40, 5, 20, 30);
            this.FontRectangle = new Rectangle(40, 50, 20, 30);
            this.TextFont = null;
            this.FontScale = 0.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="sliderWidth">Width of the slider.</param>
        /// <param name="minValue">The minimum value of the slider.</param>
        /// <param name="maxValue">The maximum value of the slider.</param>
        /// <param name="startValue">The start value of the slider.</param>
        /// <param name="textColor">Color of the slider text.</param>
        /// <param name="fontType">The font.</param>
        public Slider(Rectangle rectangle, int sliderWidth, int minValue, int maxValue, double startValue, Color textColor, SpriteFont fontType)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.CurrentValue = startValue;
            this.TextColor = textColor;
            this.TextFont = fontType;

            this.backgroundRectangle = rectangle;
            this.sliderRectangle = new Rectangle(40, 5, sliderWidth, 300);
        }

        /// <summary>
        /// Draws the slider with the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw to.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.FromStaticColor(Color.White, Color.White, spriteBatch.GraphicsDevice).Texture, backgroundRectangle, Color.White);
            spriteBatch.Draw(Sprite.FromStaticColor(Color.Red, Color.White, spriteBatch.GraphicsDevice).Texture, sliderRectangle, Color.White);

            spriteBatch.DrawString(TextFont, Math.Round(CurrentValue).ToString(), new Vector2(FontRectangle.X, FontRectangle.Y), TextColor, 0.0f, new Vector2(0, 0), FontScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Calculates the positions of all rectangles.
        /// </summary>
        public void CalculatePositions()
        {
            Vector2 size = TextFont.MeasureString(Math.Round(CurrentValue).ToString());

            float width = size.X;
            float height = size.Y;

            if (backgroundRectangle.Height == 0)
            {
                this.FontScale = backgroundRectangle.Width / width;
            }
            else
            {
                this.FontScale = backgroundRectangle.Height / height;
            }

            width = width * FontScale;
            height = height * FontScale;

            float posX = (sliderRectangle.Width / 2) - (width / 2) + sliderRectangle.X;
            float posY = sliderRectangle.Y + sliderRectangle.Height + DISTANCESLIDERTEXTY;

            FontRectangle = new Rectangle((int)posX, (int)posY, (int)width, (int)height);

            ValuePerPixel = (double)backgroundRectangle.Width / (MaxValue - MinValue);
            sliderRectangle.Height = this.backgroundRectangle.Height * 2;

            SetSliderPosition((this.backgroundRectangle.X + (int)(CurrentValue * ValuePerPixel)), backgroundRectangle.Y - backgroundRectangle.Height / 2);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            CalculatePositions();
            MouseState mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;
            Point mousePoint = new Point(mouseX, mouseY);

            Rectangle mouseRect = new Rectangle(mouseX, mouseY, 1, 1);
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (backgroundRectangle.Contains(mousePoint))
                {
                    if (OnMouseHold != null)
                    {
                        OnMouseHold(this, mouse);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the slider position.
        /// </summary>
        /// <param name="x">The new x position.</param>
        /// <param name="y">The new y position.</param>
        public void SetSliderPosition(int x, int y)
        {
            this.sliderRectangle.X = x - sliderRectangle.Width / 2;

            this.sliderRectangle.Y = y;
        }

        /// <summary>
        /// Sets the position of the background rectangle.
        /// </summary>
        /// <param name="x">The new x position.</param>
        /// <param name="y">The new y position.</param>
        public void SetPosition(int x, int y)
        {
            this.backgroundRectangle.X = x;
            this.backgroundRectangle.Y = y;
            CalculatePositions();
        }

        /// <summary>
        /// Sets the size of the background rectangle.
        /// </summary>
        /// <param name="width">The new width.</param>
        /// <param name="heigth">The new heigth.</param>
        public void SetSize(int width, int heigth)
        {
            this.backgroundRectangle.Width = width;
            this.backgroundRectangle.Height = heigth;
            CalculatePositions();
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <returns>The background rectangle of the slider.</returns>
        public Rectangle GetRectangle()
        {
            return this.backgroundRectangle;
        }

        /// <summary>
        /// Gets the slider position.
        /// </summary>
        /// <returns>A Vector2 with the slider position.</returns>
        public Vector2 GetSliderPosition()
        {
            return new Vector2(this.sliderRectangle.X, this.sliderRectangle.Y);
        }

        /// <summary>
        /// Sets the slider value.
        /// </summary>
        /// <param name="value">The value of the slider.</param>
        public void SetSliderValue(double value)
        {
            this.CurrentValue = value;
        }

        public double GetSliderValue()
        {
            return this.CurrentValue;
        }

        /// <summary>
        /// Sets the slider value.
        /// </summary>
        /// <param name="xMousePos">The x mouse position.</param>
        public void SetSliderValue(int xMousePos)
        {
            int current = xMousePos;
            double sliderPos = current - backgroundRectangle.X;
            double currentPos = (sliderPos / backgroundRectangle.Width) * 100;

            CurrentValue = currentPos;
        }
    }
}
