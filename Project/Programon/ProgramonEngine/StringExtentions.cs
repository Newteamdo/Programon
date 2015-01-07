using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitInParts(this string s, int partLength)
        {
            if (s == null) throw new ArgumentNullException("The string can't be null.");
            if (partLength <= 0) throw new ArgumentException("Part length has to be positive.", "partLength");

            for (int i = 0; i < s.Length; i += partLength)
            {
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
            }
        }

        /// <summary>
        /// Wraps the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxLineLength">Maximum length of the line.</param>
        /// <param name="spriteFont">The sprite font.</param>
        /// <returns></returns>
        public static string WrapText(string text, int maxLineLength, SpriteFont spriteFont)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float linewidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);
                if (linewidth + size.X < maxLineLength)
                {
                    sb.Append(word + " ");
                    linewidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    linewidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
    }
}
