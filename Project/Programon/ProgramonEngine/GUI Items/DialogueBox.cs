using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProgramonEngine
{
    public class DialogueBox : IGuiItem
    {
        private const int TextSpeed = 1;
        private const float BoxSpeed = 10;
        private const Keys SkipKey = Keys.Enter;

        private Texture2D BoxTexture { get; set; }
        private SpriteFont Font { get; set; }

        private Vector2 TextPosition { get; set; }
        private Vector2 BoxFinalPosition { get; set; }
        private Vector2 BoxPosition;
        private bool BoxUp { get; set; }

        private string Text { get; set; }
        private int TextIndex { get; set; }
        private bool Skipable { get; set; }
        public DialogueBox(string text, SpriteFont font, bool skipable, Rectangle bufferSize, Texture2D boxTexture)
        {
            Text = text;
            Skipable = skipable;
            BoxTexture = boxTexture;
            Font = font;

            BoxFinalPosition = new Vector2(bufferSize.X, bufferSize.Height - boxTexture.Height);
            BoxPosition = new Vector2(BoxFinalPosition.X, bufferSize.Height);
            TextPosition = new Vector2(BoxFinalPosition.X + 10, BoxFinalPosition.Y);
        }

        private string[] GetDrawableText()
        {
            string rawValue = Text.Substring(0, TextIndex);
            int stringLength = (int) Font.MeasureString(rawValue).X;

            int lines = BoxTexture.Width > stringLength ? 1 : (int) Math.Ceiling(stringLength / (float) BoxTexture.Width);
            int lineLength = rawValue.Length / lines;
            string[] values = new string[lines];

            for (int i = 0; i < lines; i++)
            {
                values[i] = rawValue.Substring(i * lineLength, lineLength);
            }

            return values;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (BoxPosition != BoxFinalPosition)
            {
                if (BoxSpeed < Vector2.Distance(BoxPosition, BoxFinalPosition))
                    BoxPosition.Y -= BoxSpeed;
                else
                    BoxPosition = BoxFinalPosition;

                spriteBatch.Draw(BoxTexture, BoxPosition, Color.White);
            }
            else
            {
                spriteBatch.Draw(BoxTexture, BoxPosition, Color.White);

                string[] text = GetDrawableText();
                for (int i = 0; i < text.Length; i++)
                {
                    float fontHeight = Font.MeasureString(text[i]).Y;
                    spriteBatch.DrawString(Font, text[i], new Vector2(TextPosition.X, TextPosition.Y + i * fontHeight), Color.Black);
                }

                if (Keyboard.GetState().IsKeyDown(SkipKey))
                    TextIndex = Text.Length;
                else if (TextIndex != Text.Length)
                    TextIndex += TextSpeed;
            }
        }
    }
}
