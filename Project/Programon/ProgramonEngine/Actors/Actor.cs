﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Actor
    {
        public Vector2 FixedPosition { get { return new Vector2(Transform.Position.X * Sprite.NodeWidth, Transform.Position.Y * Sprite.NodeWidth); } private set { } }
        public Transform Transform { get; set; }
        public Sprite Sprite { get; set; }
        public Map CurrentMap { get; set; }

        public delegate void OnEnterEventHandler(Actor actor, Node oldPos, Node newPos);
        public event OnEnterEventHandler OnEnter;

        public Actor(Vector2 startPos, Vector2 scale)
        {
            Transform = new Transform(startPos, scale);
            CurrentMap = null;
        }
        public Actor(Vector2 startPos, Vector2 scale, Map currentMap)
        {
            Transform = new Transform(startPos, scale);
            CurrentMap = currentMap;
        }

        /// <summary> Move the actor to a new node but only if the node is walkable. </summary>
        public virtual void Move(Node newPos)
        {
            if (!newPos.Walkable) return;
            Transform = new Transform(newPos.Transform.Position, Transform.Scale, Transform.Rotation);

            // Implementation check before event call
            if (CurrentMap != null)
                OnEnter(this, CurrentMap.MapDictionary[Transform.Position], newPos);
        }

        /// <summary> Load the spite of the node. </summary>
        public virtual void Load(ContentManager content, string textureName)
        {
            Sprite = new Sprite(content.Load<Texture2D>(textureName));
        }
    }
}
