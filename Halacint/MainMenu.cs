using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halacint
{
    internal class MainMenu : ControlsConsole
    {
        public MainMenu(ICellSurface surface, IFont font = null, Point? fontSize = null) : base(surface, font, fontSize)
        {
        }
    }
}
