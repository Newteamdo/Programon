using Microsoft.Xna.Framework;
using System;

namespace ProgramonEngine
{
    public class NPC : Actor
    {
        private double NextKeyStamp = 0;
        Vector2 maxUpperLeftPosition;
        Vector2 maxDownerRightPosition;

        public NPC(Vector2 startPos, Vector2 scale, Map currentMap, Vector2 maxUpperLeftPosition, Vector2 maxDownerRightPosition)
            : base(startPos, scale, currentMap)
        {
            this.maxUpperLeftPosition = maxUpperLeftPosition;
            this.maxDownerRightPosition = maxDownerRightPosition;
        }

        public void Update(Map map, GameTime gameTime)
        {
            Vector2 newPosition = Transform.Position;
            if (gameTime.TotalGameTime.TotalSeconds >= NextKeyStamp)
            {
                NextKeyStamp = gameTime.TotalGameTime.TotalSeconds + 1;

                Random rnd = new Random();
                Vector2 direction = new Vector2((float) rnd.Next(-1, 2), (float) rnd.Next(-1, 2));

                //Check if x is between two points
                if (Transform.Position.X + direction.X < maxUpperLeftPosition.X || Transform.Position.X + direction.X > maxDownerRightPosition.X)
                {
                    direction.X *= -1f;
                }

                //Check if y is between two points
                if (Transform.Position.Y + direction.Y < maxUpperLeftPosition.Y || Transform.Position.Y + direction.Y > maxDownerRightPosition.Y)
                {
                    direction.Y *= -1f;
                }

                newPosition = Transform.Position + direction;
            }

            if (map.MapDictionary.ContainsKey(newPosition))
            {
                Move(map.MapDictionary[newPosition]);
            }
        }

        public override void Move(Node newPos)
        {
            base.Move(newPos);
        }
    }
}
