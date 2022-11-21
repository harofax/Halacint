using SadConsole;
using SadRogue.Primitives;

namespace Halacint
{
    internal class World
    {
        public CellSurface cells;
        public World(int width, int height) 
        {
            cells = new CellSurface(width, height);
            GenerateWorld();
        }

        private void GenerateWorld()
        {
            for (int x = 0; x < cells.Width; x++)
            {
                for (int y = 0; y < cells.Height; y++)
                {
                    var cell_appearance = new ColoredGlyph(Color.SandyBrown, Color.SaddleBrown, 48 + 13);
                    cells[x, y].CopyAppearanceFrom(cell_appearance);
                }
            }
        }

    }
}