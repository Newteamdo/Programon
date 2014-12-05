using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Actor
    {
        public Transform Transform { get; set; }
        public Sprite Sprite { get; set; }

        public Actor(Vector2 startPos)
        {
            Transform = new Transform(startPos, Vector2.One);
        }

        public virtual void Load(ContentManager content, string textureName)
        {
            Sprite = new Sprite(content.Load<Texture2D>(textureName));
        }
    }
}
