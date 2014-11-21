using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public class Node
    {
        public Transform Transform { get; private set; }
        public Sprite Sprite { get; private set; }

        public Node(Vector2 position, Texture2D texture)
        {
            Transform = new Transform(position, Vector2.One);
            Sprite = new Sprite(texture, Color.White);
        }

        public Node(Transform transform, Sprite sprite)
        {
            Transform = transform;
            Sprite = sprite;
        }
    }
}
