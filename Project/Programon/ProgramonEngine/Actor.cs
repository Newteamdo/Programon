using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Actor
    {
        public Vector2 FixedPosition { get { return Transform.Position * global::ProgramonEngine.Sprite.TextureHeight; } private set { } }
        public Transform Transform { get; set; }
        public Sprite Sprite { get; set; }

        public Actor(Vector2 startPos)
        {
            Transform = new Transform(startPos, Vector2.One);
        }
        
        public void Move(Node newPos)
        {
            if (!newPos.Walkable) return;
            Transform = new Transform(newPos.Transform.Position, Transform.Scale, Transform.Direction);
        }

        public virtual void Load(ContentManager content, string textureName)
        {
            Sprite = new Sprite(content.Load<Texture2D>(textureName));
        }
    }
}
