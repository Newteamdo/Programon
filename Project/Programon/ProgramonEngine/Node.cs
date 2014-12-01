using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public class Node
    {
        public Vector2 FixedPosition { get { return Transform.Position * 16; } private set { } }
        public Transform Transform { get; private set; }
        public Sprite Sprite { get; private set; }

        public Node(Vector2 position, Texture2D texture)
        {
            Transform = new Transform(position, Vector2.One);
            Sprite = new Sprite(texture, Color.White);
        }

        public Node(Transform transform, Sprite sprite)
        {
            Transform = transform;
            Sprite = sprite;
        }
    }
}
