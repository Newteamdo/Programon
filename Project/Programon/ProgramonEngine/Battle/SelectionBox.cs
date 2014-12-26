using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace ProgramonEngine
{
    public class SelectionBox : BattleBox
    {
        public List<string> SelectionList;
        public int selectionID;

        public SelectionBox(Vector2 startPos, Vector2 endPos)
            :base(startPos)
        {
            SelectionList = new List<string>(4);
            SelectionList.Add("Abilities");
            SelectionList.Add("Programon");
            SelectionList.Add("Bag");
            SelectionList.Add("Run");
        }
    }
}
