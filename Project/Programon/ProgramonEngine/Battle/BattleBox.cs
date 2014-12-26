using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ProgramonEngine
{
    public abstract class BattleBox : IGuiItem
    {
        protected Vector2 StartPos { get; set; }
        protected Vector2 Size { get; set; }
        protected SpriteFont Font { get; set; }
        public Node Node { get; set; }
        public List<Node> TextNodeList { get; set; }

        public BattleBox(Vector2 startPos)
        {
            this.StartPos = startPos;
            TextNodeList = new List<Node>();
        }

        //public BattleBox(Vector2 startPos, Vector2 size)
        //{
        //    this.StartPos = startPos;
        //    this.Size = size;

        //}

        public virtual void Load(SpriteFont font, Texture2D fullBox)
        {
            //Still unstable
            //for (float y = 0; y < StartPos.Y + Size.Y; y++)
            //{
            //    for (float x = 0; x < StartPos.X+ Size.X; x++)
            //    {

            //        if ((x == 0 || x == Size.X) && (y == 0 || y == Size.Y))
            //        {
            //            //corner++;
            //            //Node node = new Node(new Vector2(x, y), cornerTexture);
            //            Node node = new Node(new Transform(StartPos + new Vector2(x, y), new Vector2(4), (0 / 180 * MathHelper.Pi)), new Sprite(cornerTexture), false);
            //            nodeList.Add(node);


            //        }
            //        else if (x == 0 || x == Size.X || y == 0 || y == Size.Y)
            //        {
            //            //nodeList.Add(new Node(new Vector2(x, y), sideTexture));
            //            Node node = new Node(new Transform(StartPos + new Vector2(x, y), new Vector2(4), (0 / 180 * MathHelper.Pi)), new Sprite(sideTexture), false);
            //            nodeList.Add(node);
            //        }
            //        else
            //        {
            //            //nodeList.Add(new Node(new Vector2(x, y), middleTexture));
            //            Node node = new Node(new Transform(StartPos + new Vector2(x, y), new Vector2(4), 0), new Sprite(middleTexture), false);
            //            nodeList.Add(node);
            //        }
            //    }
            //}
            Node = new Node(new Transform(StartPos, new Vector2(1), 0), new Sprite(fullBox), false);
            

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //for (int i = 0; i < nodeList.Count; i++)
            //{
                Node cur = Node;
                spriteBatch.Draw(cur.Sprite.Texture,
                cur.FixedPosition + new Vector2(16, 16),
                null,
                cur.Sprite.Tint,
                cur.Transform.Rotation,
                Vector2.Zero,
                cur.Transform.Scale,
                SpriteEffects.None,
                0f);

            //}
        }
    }
}
