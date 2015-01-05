using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaGuiItems.Items;

namespace ProgramonEngine
{
    public class ProgramonStatusBox : BattleBox
    {
        private Creature Creature { get; set; }
        private bool isEnemy { get; set; }
        public ProgramonStatusBox(Vector2 startPos, Creature creature)
            : base(startPos)
        {
            this.Creature = creature;
            //TextNodeList[0].position = StartPos + new Vector2(2, 1) * new Vector2(16);
            //TextNodeList[1].text = "Lvl" + creature.level;
            //TextNodeList[1].position = StartPos + new Vector2(128, 16) * new Vector2(16);
            //TextNodeList[2].text = "HP" + creature.programonBaseStats.health;
            //TextNodeList[2].position = StartPos + new Vector2(64, 16) * new Vector2(16);
            //TextNodeList[3].text = "XP" + creature.exp;
            //TextNodeList[3].position = StartPos + new Vector2(64, 16) * new Vector2(16);

        }

        public void Load(GraphicsDevice graphicsDevice, SpriteFont font, Texture2D fullBox)
        {
            base.Load(font, fullBox);
            TextList.Add(new Label(graphicsDevice, new Vector2(), new Rectangle((int)Node.FixedPosition.X, (int)Node.FixedPosition.Y, 100, 50), font));
            TextList[0].Text = Creature.name;
            TextList[0].ForeColor = Color.Black;
            TextList[0].BackColor = Color.Transparent;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < TextList.Count; i++)
            {
                TextList[i].Draw(spriteBatch);
            }
        }
    }
}
