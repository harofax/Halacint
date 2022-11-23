using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace Halacint
{
    internal class ViewBorder : Console
    {
        public int BorderWidth, BorderHeight;
        public ViewBorder(int width, int height) : base(width + 2, height + 2)
        {
            DefaultBackground = Color.Transparent;
            BorderWidth = width + 2;
            BorderHeight = height + 2;

            DrawBorder();
        }
        
        public void DrawBorder()
        {
            Surface.DrawBox(new Rectangle(0, 0, BorderWidth, BorderHeight),
                ShapeParameters.CreateStyledBox((int[])ICellSurface.ConnectedLineThin,
                    new ColoredGlyph(Color.Orange, Color.AnsiBlack)));

            IsDirty = true;
        }
        
    }
}
