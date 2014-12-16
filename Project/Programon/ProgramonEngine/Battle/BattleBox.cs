using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ProgramonEngine
{
    public abstract class BattleBox
    {
        protected Vector2 startPos { get; set;}

        public Node node;

        public BattleBox()
        {

        }

        public void SetTexture(Texture2D texture)
        {
            node = new Node(startPos, texture);
        }
    }
}
