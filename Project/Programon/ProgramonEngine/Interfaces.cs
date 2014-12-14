using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public interface IGuiItem
    {
        void Draw(SpriteBatch spriteBatch);
    }

    public interface IMenu
    {
        IGuiItem[] Childs { get; set; }
    }
}
