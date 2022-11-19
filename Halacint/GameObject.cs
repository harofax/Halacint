using SadConsole;
using SadRogue.Primitives;

namespace Halacint
{
    public class GameObject
    {
        public Point Position { get; private set; }
        public ColoredGlyph Appearance { get; set; }
        private ColoredGlyph _underTile = new ColoredGlyph();

        public GameObject(ColoredGlyph appearance, Point position, IScreenSurface hostingSurface)
        {
            Appearance = appearance;
            Position = position;

            //store map tile
            hostingSurface.Surface[position].CopyAppearanceTo(_underTile);

            DrawGameObject(hostingSurface);
        }

        private void DrawGameObject(IScreenSurface screenSurface)
        {
            Appearance.CopyAppearanceTo(screenSurface.Surface[Position]);
            screenSurface.IsDirty = true;
        }

        public void Move(Point newPosition, IScreenSurface screenSurface)
        {
            // restore the tile we occupied
            _underTile.CopyAppearanceTo(screenSurface.Surface[Position]);

            // store the new occupied tile
            screenSurface.Surface[newPosition].CopyAppearanceTo(_underTile);

            Position = newPosition;
            DrawGameObject(screenSurface);
        }

    }
}
