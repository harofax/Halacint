using SadConsole;
using SadRogue.Primitives;

namespace Halacint
{
    internal class World
    {
        public CellSurface cells;
        private byte[,] obstructed;
        public World(int width, int height) 
        {
            cells = new CellSurface(width, height);
            obstructed = new byte[width, height];

            //GameLoop.Debug.Log("walkable?: " + walkable[2,2].ToString(), Color.Red);
            var wheatTile = new ColoredGlyph(Color.PaleGoldenrod, Color.SandyBrown, '"');
            GenerateFill(wheatTile, false);
            GenerateTerrain();
            //MorphWorld();
        }

        private void GenerateTerrain()
        {
            ColoredGlyph borderGlyph = new ColoredGlyph(Color.CornflowerBlue, Color.AnsiBlackBright, '#');
            ColoredGlyph fillGlyph = new ColoredGlyph(Color.Gray, Color.AnsiBlackBright, ':');

            Rectangle area = new Rectangle(20, 20, 20, 20);

            cells.DrawCircle(area, ShapeParameters.CreateFilled(borderGlyph, fillGlyph));
            
            Algorithms.Ellipse(area.X, area.Y, area.MaxExtentX, area.MaxExtentY, (x, y) =>
            {
                //Point cell = (x, y);
                obstructed[x, y] = 1;
            });
        }

        public byte IsWalkable(int x, int y)
        {
            return obstructed[x, y];
        }

        private void GenerateFill(ColoredGlyph tile, bool isObstructed)
        {
            for (int x = 0; x < cells.Width; x++)
            {
                for (int y = 0; y < cells.Height; y++)
                {
                    
                    cells[x, y].CopyAppearanceFrom(tile);
                    obstructed[x, y] = (byte) (isObstructed ? 1 : 0);
                }
            }
        }

        public void GarbleWorld()
        {
            foreach (var cell in cells)
            {
                cell.Background = new Color(Game.Instance.Random.Next(60, 150), Game.Instance.Random.Next(60, 150), Game.Instance.Random.Next(60, 155));
            }
        }

    }
}