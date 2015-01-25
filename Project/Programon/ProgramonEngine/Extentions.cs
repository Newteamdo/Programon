using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
     public static class Extentions
     {
          public static void ForEach<T>(this IList<T> value, Func<T, T> func)
          {
               if (value.GetType() == typeof(T[]))
               {
                    for (int i = 0; i < (value as T[]).Length; i++)
                    {
                         value[i] = func(value[i]);
                    }
               }
               else
               {
                    for (int i = 0; i < value.Count; i++)
                    {
                         value[i] = func(value[i]);
                    }
               }
          }

          public static void ForEach<T>(this IList<T> value, Action<T> action)
          {
               if (value.GetType() == typeof(T[]))
               {
                    for (int i = 0; i < (value as T[]).Length; i++)
                    {
                         action(value[i]);
                    }
               }
               else
               {
                    for (int i = 0; i < value.Count; i++)
                    {
                         action(value[i]);
                    }
               }
          }

          public static int X(this Vector2 vect2)
          {
               return (int)vect2.X;
          }

          public static int Y(this Vector2 vect2)
          {
               return (int)vect2.Y;
          }

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
          public static string WrapText(this string text, int maxLineLength, SpriteFont spriteFont)
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
