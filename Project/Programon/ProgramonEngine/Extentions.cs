using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
    }
}
