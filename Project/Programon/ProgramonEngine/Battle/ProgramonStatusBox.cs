using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public class ProgramonStatusBox : BattleBox
    {

        public ProgramonStatusBox(Vector2 startPos, Creature creature)
            : base(startPos)
        {

            //TextNodeList[0].position = StartPos + new Vector2(2, 1) * new Vector2(16);
            //TextNodeList[1].text = "Lvl" + creature.level;
            //TextNodeList[1].position = StartPos + new Vector2(128, 16) * new Vector2(16);
            //TextNodeList[2].text = "HP" + creature.programonBaseStats.health;
            //TextNodeList[2].position = StartPos + new Vector2(64, 16) * new Vector2(16);
            //TextNodeList[3].text = "XP" + creature.exp;
            //TextNodeList[3].position = StartPos + new Vector2(64, 16) * new Vector2(16);

        }

        public override void Load(SpriteFont font, Texture2D fullBox)
        {
            base.Load(font, fullBox);
            //TextNodeList[0] = new Node(new Vector2(2, 1), Sprite.FromString(creature.name, Font, Color.Black, Vector2(2), ), false);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
}
