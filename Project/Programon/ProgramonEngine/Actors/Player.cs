using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ProgramonEngine
{
    public class Player : Actor
    {
        public bool IsWalking { get; private set; }
        public Dictionary<AnimationTypes, Animation> Animations { get; private set; }
        public List<Creature> programons = new List<Creature>();
        public Inventory Inventory { get; set; }
        private int updateCount { get; set; }

        public Player(Vector2 startPos, Vector2 scale, Map currentMap)
            : base(startPos, scale, currentMap)
        {
            Animations = new Dictionary<AnimationTypes, Animation>();
            Inventory = new Inventory();
        }

        public void LoadAnimation(ContentManager content, AnimationTypes type, params string[] attrNames)
        {
            Animation animation = new Animation(type.ToString());
            animation.Load(content, attrNames);

            Animations.Add(type, animation);
        }

        public void Update()
        {
            if (updateCount > 0) IsWalking = false;
            updateCount++;
        }

        public override void Move(Node newPos)
        {
            IsWalking = true;
            updateCount = 0;
            base.Move(newPos);
        }

        public void AddProgramon(Creature programon)
        {
            programons.Add(programon);
        }

        public void RemoveProgramon(Creature programon)
        {
            programons.Remove(programon);
        }
    }
}
