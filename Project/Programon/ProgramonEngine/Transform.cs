using System;
using Microsoft.Xna.Framework;

namespace ProgramonEngine
{
    [Serializable]
    public struct Transform
    {
        public Vector2 Position { get { return Pposition; } set { Pposition = value; } }
        public Vector2 Scale { get { return Pscale; } set { Pscale = value; } }
        public float Rotation { get { return Pdirection; } set { Pdirection = value; } }

        private Vector2 Pposition;
        private Vector2 Pscale;
        private float Pdirection;

        public Transform(Vector2 position, Vector2 scale)
        {
            Pposition = position;
            Pscale = scale;
            Pdirection = 0f;
        }

        public Transform(Vector2 position, Vector2 scale, float rotation)
        {
            Pposition = position;
            Pscale = scale;
            Pdirection = rotation;
        }

        /// <summary> Check if the transform is located in the rectangle. </summary>
        public bool IsBetweenBounds(Rectangle bounds)
        {
            if (Pposition.X >= bounds.X && Pposition.X <= bounds.Width && Pposition.Y >= bounds.Y && Pposition.Y <= bounds.Height)
                return true;

            return false;
        }
    }
}
