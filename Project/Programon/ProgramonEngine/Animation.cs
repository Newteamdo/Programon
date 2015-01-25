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
          private float Time { get; set; }

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

          public Sprite NextFrame(float deltaTime)
          {
               Time += deltaTime;

               if (Time > .2f)
               {
                    currentFrame++;
                    if (currentFrame == MaxFrames) currentFrame = 0;
                    Time = 0;
               }

               return Sprites[currentFrame];
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
