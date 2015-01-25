using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramonEngine
{
    [Serializable]
    public class Actor
    {
        [System.Xml.Serialization.XmlIgnore]
        public Vector2 FixedPosition { get { return new Vector2(Transform.Position.X * Sprite.NodeWidth, Transform.Position.Y * Sprite.NodeWidth); } private set { } }
        public Transform Transform { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Sprite Sprite { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Map CurrentMap { get; set; }
        public bool CanEncounter { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Direction Direction { get; private set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<AnimationTypes, Animation> Animations { get; private set; }
        [System.Xml.Serialization.XmlIgnore]
        public bool IsWalking { get; private set; }
        [System.Xml.Serialization.XmlIgnore]
        private int updateCount { get; set; }

        public delegate void OnEnterEventHandler(Actor actor, Node oldPos, Node newPos);
        public event OnEnterEventHandler OnEnter;

        public delegate void OnEncounterEventHandler(Actor actor);
        public event OnEncounterEventHandler OnEncounter;

        protected internal Actor()
        {
            this.FixedPosition = new Vector2();
            this.Transform = new Transform();
            this.Sprite = new Sprite();
            this.CurrentMap = new Map();
            this.CanEncounter = false;
            this.Direction = ProgramonEngine.Direction.RIGHT;
            this.Animations = new Dictionary<AnimationTypes, Animation>();
        }

        public Actor(Vector2 startPos, Vector2 scale)
        {
            Animations = new Dictionary<AnimationTypes, Animation>();
            Transform = new Transform(startPos, scale);
            CurrentMap = null;
        }
        public Actor(Vector2 startPos, Vector2 scale, Map currentMap, bool canEncounter = false)
        {
            Transform = new Transform(startPos, scale);
            CurrentMap = currentMap;
            Animations = new Dictionary<AnimationTypes, Animation>();
            this.CanEncounter = canEncounter;
        }

        public virtual void Update()
        {
            if (updateCount > 0)
                IsWalking = false;
            updateCount++;
        }

        /// <summary> Move the actor to a new node but only if the node is walkable. </summary>
        public virtual void Move(Node newPos)
        {
            IsWalking = true;
            updateCount = 0;

            if (newPos.Transform.Position.Y < Transform.Position.Y)
            {
                //Moving up
                Direction = ProgramonEngine.Direction.UP;
                if (Animations.ContainsKey(AnimationTypes.WalkingUp) && Animations[AnimationTypes.WalkingUp] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingUp][0];
                }
            }

            if (newPos.Transform.Position.Y > Transform.Position.Y)
            {
                //Moving down
                Direction = ProgramonEngine.Direction.DOWN;
                if (Animations.ContainsKey(AnimationTypes.WalkingDown) && Animations[AnimationTypes.WalkingDown] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingDown][0];
                }
            }

            if (newPos.Transform.Position.X < Transform.Position.X)
            {
                //Moving left
                Direction = ProgramonEngine.Direction.LEFT;
                if (Animations.ContainsKey(AnimationTypes.WalkingLeft) && Animations[AnimationTypes.WalkingLeft] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingLeft][0];
                }
            }

            if (newPos.Transform.Position.X > Transform.Position.X)
            {
                //Moving right
                Direction = ProgramonEngine.Direction.RIGHT;
                if (Animations.ContainsKey(AnimationTypes.WalkingLeft) && Animations[AnimationTypes.WalkingLeft] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingLeft][0];
                }
            }

            if (!newPos.Walkable)
                return;

            Transform = new Transform(newPos.Transform.Position, Transform.Scale, Transform.Rotation);

            // Encounter implementation
            if (CanEncounter && newPos.IsTallGrass)
            {
                if ((new LCGRandom(DateTime.Now.Second).NextShort() & 15) == 0)
                {
                    if (OnEncounter != null)
                    {
                        OnEncounter(this);
                    }
                }
            }

            // Implementation check before event call
            if (CurrentMap != null && OnEnter != null)
                OnEnter(this, CurrentMap.MapDictionary[Transform.Position], newPos);
        }

        public virtual void Move(Node newPos, IEnumerable<Actor> actors = null)
        {
            IsWalking = true;
            updateCount = 0;

            if (newPos.Transform.Position.Y < Transform.Position.Y)
            {
                //Moving up
                Direction = ProgramonEngine.Direction.UP;
                if (Animations.ContainsKey(AnimationTypes.WalkingUp) && Animations[AnimationTypes.WalkingUp] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingUp][0];
                }
            }

            if (newPos.Transform.Position.Y > Transform.Position.Y)
            {
                //Moving down
                Direction = ProgramonEngine.Direction.DOWN;
                if (Animations.ContainsKey(AnimationTypes.WalkingDown) && Animations[AnimationTypes.WalkingDown] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingDown][0];
                }
            }

            if (newPos.Transform.Position.X < Transform.Position.X)
            {
                //Moving left
                Direction = ProgramonEngine.Direction.LEFT;
                if (Animations.ContainsKey(AnimationTypes.WalkingLeft) && Animations[AnimationTypes.WalkingLeft] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingLeft][0];
                }
            }

            if (newPos.Transform.Position.X > Transform.Position.X)
            {
                //Moving right
                Direction = ProgramonEngine.Direction.RIGHT;
                if (Animations.ContainsKey(AnimationTypes.WalkingLeft) && Animations[AnimationTypes.WalkingLeft] != null)
                {
                    Sprite = Animations[AnimationTypes.WalkingLeft][0];
                }
            }

            // Encounter implementation
            if (CanEncounter && newPos.IsTallGrass)
            {
                //Optimalised modulo 16
                if ((new LCGRandom(DateTime.Now.Second).NextShort() & 15) == 0)
                {
                    if (OnEncounter != null)
                    {
                        OnEncounter(this);
                    }
                }
            }

            if (actors != null)
            {
                for (int i = 0; i < actors.Count(); i++)
                {
                    Actor ac = actors.ElementAt(i);

                    if (newPos.Transform.Position == ac.Transform.Position)
                        return;
                }
            }

            if (!newPos.Walkable)
                return;
            Transform = new Transform(newPos.Transform.Position, Transform.Scale, Transform.Rotation);

            // Implementation check before event call
            if (CurrentMap != null && OnEnter != null)
                OnEnter(this, CurrentMap.MapDictionary[Transform.Position], newPos);
        }

        /// <summary> Load the spite of the node. </summary>
        public virtual void Load(ContentManager content, string textureName)
        {
            Sprite = new Sprite(content.Load<Texture2D>(textureName));
        }

        public virtual void LoadAnimation(ContentManager content, AnimationTypes type, params string[] attrNames)
        {
            Animation animation = new Animation(type.ToString());
            animation.Load(content, attrNames);

            Animations.Add(type, animation);
        }
    }
}
