using SadConsole;
using SadRogue.Primitives;

namespace Halacint
{
    internal class World
    {
        public CellSurface cells;
        private byte[,] walkable;
        public World(int width, int height) 
        {
            cells = new CellSurface(width, height);
            walkable = new byte[width, height];

            //GameLoop.Debug.Log("walkable?: " + walkable[2,2].ToString(), Color.Red);

            GenerateWorld();
            //MorphWorld();
        }

        private void GenerateWorld()
        {
            for (int x = 0; x < cells.Width; x++)
            {
                for (int y = 0; y < cells.Height; y++)
                {
                    var cell_appearance = new ColoredGlyph(Color.PaleGoldenrod, Color.SandyBrown, '"');
                    cells[x, y].CopyAppearanceFrom(cell_appearance);
                }
            }
        }

        public void MorphWorld()
        {
            foreach (var cell in cells)
            {
                cell.Background = new Color(Game.Instance.Random.Next(60, 150), Game.Instance.Random.Next(60, 150), Game.Instance.Random.Next(60, 155));
            }
        }

    }
}