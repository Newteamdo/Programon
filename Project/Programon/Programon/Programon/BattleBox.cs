using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProgramonEngine;
using System.Collections.Generic;

namespace Programon
{
    class BattleBox
    {
        Vector2 startPos;

        Node node;

        public BattleBox(Vector2 startPos)
        {
            this.startPos = startPos;
        }

        public void SetTexture(Texture2D texture)
        {
            node = new Node(startPos, texture);
        }

    }
}
