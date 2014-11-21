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
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Direction Direction { get; set; }

        public Transform(Vector2 position, Vector2 scale)
        {
            Position = position;
            Scale = scale;
            Direction = global::ProgramonEngine.Direction.Up;
        }

        public Transform(Vector2 position, Vector2 scale, Direction direction)
        {
            Position = position;
            Scale = scale;
            Direction = direction;
        }
    }
}
