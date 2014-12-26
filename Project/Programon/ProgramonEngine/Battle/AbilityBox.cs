using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace ProgramonEngine
{
    public class AbilityBox : BattleBox
    {
        public List<Ability> SelectionList;
        public  int selectionID;


        public AbilityBox(Vector2 startPos,Vector2 endPos, List<Ability> selectionList)
            :base(startPos)
        {
            SelectionList = new List<Ability>(4);
            this.SelectionList = selectionList;
        }
    }
}
