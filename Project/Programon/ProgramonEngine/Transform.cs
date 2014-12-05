using Microsoft.Xna.Framework;

namespace ProgramonEngine
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public struct Transform
    {
        public Vector2 Position { get { return Pposition; } set { Pposition = value; } }
        public Vector2 Scale { get { return Pscale; } set { Pscale = value; } }
        public Direction Direction { get { return Pdirection; } set { Pdirection = value; } }

        private Vector2 Pposition;
        private Vector2 Pscale;
        private Direction Pdirection;

        public Transform(Vector2 position, Vector2 scale)
        {
            Pposition = position;
            Pscale = scale;
            Pdirection = global::ProgramonEngine.Direction.Up;
        }

        public Transform(Vector2 position, Vector2 scale, Direction direction)
        {
            Pposition = position;
            Pscale = scale;
            Pdirection = direction;
        }

        public bool IsBetweenBounds(Rectangle bounds)
        {
            if (Pposition.X >= bounds.X && Pposition.X <= bounds.Width && Pposition.Y >= bounds.Y && Pposition.Y <= bounds.Height)
                return true;

            return false;
        }
    }
}
