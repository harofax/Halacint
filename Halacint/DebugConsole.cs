using SadConsole;
using System;

namespace Halacint
{
    // public because of eventual mod support maybe idk
    public class DebugConsole
    {
        private ScreenSurface surface;
        private int height;
        private int width;

        public DebugConsole(int width, int height)
        {
            this.width = width;
            this.height = height;

            surface = new ScreenSurface(width, height);
            surface.IsVisible = true;
        }
    }
}