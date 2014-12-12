using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public class Node
    {
        public Vector2 FixedPosition { get { return Transform.Position * global::ProgramonEngine.Sprite.TextureHeight; } private set { } }
        public Transform Transform { get; private set; }
        public Sprite Sprite { get; private set; }
        public bool Walkable { get; private set; }

        public Node(Vector2 position, Texture2D texture)
        {
            Transform = new Transform(position, new Vector2(Sprite.TextureWidth / texture.Width, Sprite.TextureHeight / texture.Height));
            Sprite = new Sprite(texture, Color.White);
            Walkable = true;
        }

        public Node(Transform transform, Sprite sprite, bool walkable)
        {
            Transform = transform;
            Sprite = sprite;
            Walkable = walkable;
        }
    }
}
