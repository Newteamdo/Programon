using Microsoft.Xna.Framework;
using System;

namespace ProgramonEngine
{
    public class NPC : Actor
    {
        private double NextKeyStamp = 0;
        public NPC(Vector2 startPos, Vector2 scale, Map currentMap)
            : base(startPos, scale, currentMap)
        {

        }

        public void Update(Map map, GameTime gameTime)
        {
            Vector2 leftUpperMax = new Vector2(8, 3);
            Vector2 rightDownMax = new Vector2(12, 7);
            Vector2 newPosition = Transform.Position;
            if (gameTime.TotalGameTime.TotalSeconds >= NextKeyStamp)
            {
                NextKeyStamp = gameTime.TotalGameTime.TotalSeconds + 1;

                Random rnd = new Random();
                Vector2 direction = new Vector2((float) rnd.Next(-1, 2), (float) rnd.Next(-1, 2));

                //Check if x is between two points
                if (Transform.Position.X + direction.X < leftUpperMax.X || Transform.Position.X + direction.X > rightDownMax.X)
                {
                    direction.X *= -1f;
                }

                //Check if y is between two points
                if (Transform.Position.Y + direction.Y < leftUpperMax.Y || Transform.Position.Y + direction.Y > rightDownMax.Y)
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
