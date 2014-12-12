using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public interface IMenu
    {
        IGuiItem[] Childs { get; set; }
    }
}
