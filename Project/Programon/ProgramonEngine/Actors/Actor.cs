using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Actor
    {
        public Vector2 FixedPosition { get { return new Vector2(Transform.Position.X * Sprite.NodeWidth, Transform.Position.Y * Sprite.NodeWidth); } private set { } }
        public Transform Transform { get; set; }
        public Sprite Sprite { get; set; }

        public Actor(Vector2 startPos, Vector2 scale)
        {
            Transform = new Transform(startPos, scale);
        }

        /// <summary> Move the actor to a new node but only if the node is walkable. </summary>
        public void Move(Node newPos)
        {
            if (!newPos.Walkable) return;
            Transform = new Transform(newPos.Transform.Position, Transform.Scale, Transform.Direction);
        }

        /// <summary> Load the spite of the node. </summary>
        public virtual void Load(ContentManager content, string textureName)
        {
            Sprite = new Sprite(content.Load<Texture2D>(textureName));
        }
    }
}
