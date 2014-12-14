using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public class Node
    {
        public Vector2 FixedPosition { get { return new Vector2(Transform.Position.X * Sprite.NodeWidth, Transform.Position.Y * Sprite.NodeWidth); } private set { } }
        public Transform Transform { get; private set; }
        public Sprite Sprite { get; private set; }
        public bool Walkable { get; private set; }

        public Node(Vector2 position, Texture2D texture)
        {
            Transform = new Transform(position, new Vector2(Sprite.NodeWidth / texture.Width, Sprite.NodeHeight / texture.Height));
            Sprite = new Sprite(texture, Color.White);
            Walkable = true;
        }

        public Node(Vector2 position, Texture2D texture, bool walkable)
        {
            Transform = new Transform(position, new Vector2(Sprite.NodeWidth / texture.Width, Sprite.NodeHeight / texture.Height));
            Sprite = new Sprite(texture, Color.White);
            Walkable = walkable;
        }

        public Node(Transform transform, Sprite sprite, bool walkable)
        {
            Transform = transform;
            Sprite = sprite;
            Walkable = walkable;
        }
    }
}
