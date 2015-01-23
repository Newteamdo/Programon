using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public enum AnimationTypes
    {
        WalkingUp,
        WalkingLeft,
        WalkingDown
    }

    public class Animation
    {
        public string Name { get; private set; }
        public int currentFrame { get; private set; }

        private Sprite[] Sprites { get; set; }
        private int MaxFrames { get; set; }

        public Animation(string name)
        {
            Name = name;
        }

        public void Load(ContentManager content, params string[] attributeNames)
        {
            MaxFrames = attributeNames.Length;
            Sprites = new Sprite[MaxFrames];

            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i] = new Sprite(content.Load<Texture2D>(attributeNames[i]));
            }
        }

        public Sprite NextFrame()
        {
            currentFrame++;
            if (currentFrame > MaxFrames)
                currentFrame = 1;

            return Sprites[currentFrame - 1];
        }

        public Sprite this[int index]
        {
            get
            {
                return Sprites[index];
            }
        }
    }
}
