using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to display a slider.
    /// </summary>
    public class Slider
    {
        const int DISTANCESLIDERTEXTY = 10;
        public delegate void OnMouseHoldEventHandler(Game game, Slider slider, MouseState mouseState);
        public event OnMouseHoldEventHandler OnMouseHold;

        private int minValue { get; set; }
        private int maxValue { get; set; }
        private double currentValue { get; set; }

        private Rectangle rectangle;
        private Rectangle sliderRectangle;
        private Rectangle fontRectangle { get; set; }

        private double perPixel { get; set; }
        private float fontScale { get; set; }

        private Color textColor { get; set; }
        private SpriteFont font { get; set; }

        private Game gameWindow { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class.
        /// With default values.
        /// </summary>
        public Slider()
        {
            this.gameWindow = null;
            this.minValue = 0;
            this.maxValue = 0;
            this.currentValue = 0;

            this.rectangle = new Rectangle(0, 10, 100, 20);
            this.sliderRectangle = new Rectangle(40, 5, 20, 30);
            this.fontRectangle = new Rectangle(40, 50, 20, 30);
            this.font = null;
            this.fontScale = 0.0f;
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
        /// <param name="game">The game.</param>
        public Slider(Rectangle rectangle, int sliderWidth, int minValue, int maxValue, int startValue, Color textColor, SpriteFont fontType, Game game)
        {
            this.gameWindow = game;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = startValue;
            this.textColor = textColor;
            this.font = fontType;

            this.rectangle = rectangle;
            this.sliderRectangle = new Rectangle(40, 5, sliderWidth, 300);
        }

        /// <summary>
        /// Draws the slider with the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw to.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.FromStaticColor(Color.White, Color.White, spriteBatch.GraphicsDevice).Texture, rectangle, Color.White);
            spriteBatch.Draw(Sprite.FromStaticColor(Color.Red, Color.White, spriteBatch.GraphicsDevice).Texture, sliderRectangle, Color.White);

            spriteBatch.DrawString(font, Math.Round(currentValue).ToString(), new Vector2(fontRectangle.X, fontRectangle.Y), textColor, 0.0f, new Vector2(0, 0), fontScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Calculates the positions of all rectangles.
        /// </summary>
        public void CalculatePositions()
        {
            Vector2 size = font.MeasureString(Math.Round(currentValue).ToString());

            float width = size.X;
            float height = size.Y;

            if (rectangle.Height == 0)
            {
                this.fontScale = rectangle.Width / width;
            }
            else
            {
                this.fontScale = rectangle.Height / height;
            }

            width = width * fontScale;
            height = height * fontScale;

            float posX = (sliderRectangle.Width / 2) - (width / 2) + sliderRectangle.X;
            float posY = sliderRectangle.Y + sliderRectangle.Height + DISTANCESLIDERTEXTY;

            fontRectangle = new Rectangle((int)posX, (int)posY, (int)width, (int)height);

            perPixel = (double)rectangle.Width / (maxValue - minValue);
            sliderRectangle.Height = this.rectangle.Height * 2;

            SetSliderPosition((this.rectangle.X + (int)(currentValue * perPixel)), rectangle.Y - rectangle.Height / 2);
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
                if (rectangle.Contains(mousePoint))
                {
                    if (OnMouseHold != null)
                    {
                        OnMouseHold(gameWindow, this, mouse);
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
            this.rectangle.X = x;
            this.rectangle.Y = y;
            CalculatePositions();
        }

        /// <summary>
        /// Sets the size of the background rectangle.
        /// </summary>
        /// <param name="width">The new width.</param>
        /// <param name="heigth">The new heigth.</param>
        public void SetSize(int width, int heigth)
        {
            this.rectangle.Width = width;
            this.rectangle.Height = heigth;
            CalculatePositions();
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <returns>The background rectangle of the slider.</returns>
        public Rectangle GetRectangle()
        {
            return this.rectangle;
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
        /// <param name="xMousePos">The x mouse position.</param>
        public void SetSliderValue(int xMousePos)
        {
            int current = xMousePos;
            double sliderPos = current - rectangle.X;
            double currentPos = (sliderPos / rectangle.Width) * 100;

            currentValue = currentPos;
        }
    }
}
