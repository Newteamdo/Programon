using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace ProgramonEngine
{
    public class AbilityBox : BattleBox
    {
        public List<Ability> SelectionList;
        public  int selectionID;


        public AbilityBox(Vector2 startPos, List<Ability> selectionList)
        {
            this.startPos = startPos;
            SelectionList = new List<Ability>(4);
            this.SelectionList = selectionList;
        }
    }
}
