using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ProgramonEngine
{
    public enum BossStage { StartDialogue, Fight, EndDialogue }

    public class Boss : Creature
    {
        public string Name { get; private set; }
        public BossStage Stage { get; private set; }

        private Dictionary<BossStage, string> Dialogue { get; set; }

        public Boss(string name, Vector2 startPos, byte level, Stats stats, List<Ability> abilities)
            : base(startPos, level, stats, null, abilities)
        {
            Name = name;
            Dialogue = new Dictionary<BossStage, string>();
            Stage = BossStage.StartDialogue;
        }

        public string GetDialogue()
        {
            return Dialogue[Stage];
        }

        public void Load(ContentManager content, string textureName, string startDialogueName, string endDialogueName)
        {
            base.Sprite = new Sprite(content.Load<Texture2D>(textureName));
            Dialogue.Add(BossStage.StartDialogue, content.Load<string>(startDialogueName));
            Dialogue.Add(BossStage.EndDialogue, content.Load<string>(endDialogueName));
        }
    }
}
