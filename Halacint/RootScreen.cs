using SadConsole;
using SadConsole.Input;
using SadConsole.UI;
using SadRogue.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Halacint
{
    public class RootScreen : ScreenObject
    {
        private ScreenSurface _map; 
        private GameObject _controlledObject;

        public RootScreen()
        {
            _map = new ScreenSurface(300, 300);
            _map.UseMouse = false;

            FillBackground();



            Children.Add(_map);

            _controlledObject = new GameObject(new ColoredGlyph(Color.Yellow, Color.Black, 2), _map.Surface.Area.Center, _map);
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            bool handled = false;

            if (keyboard.IsKeyPressed(Keys.Up))
            {
                _controlledObject.Move(_controlledObject.Position + Direction.Up, _map);
                handled = true;
            }
            else if (keyboard.IsKeyPressed(Keys.Down))
            {
                _controlledObject.Move(_controlledObject.Position + Direction.Down, _map);
                handled = true;
            }

            if (keyboard.IsKeyPressed(Keys.Left))
            {
                _controlledObject.Move(_controlledObject.Position + Direction.Left, _map);
                handled = true;
            }
            else if (keyboard.IsKeyPressed(Keys.Right))
            {
                _controlledObject.Move(_controlledObject.Position + Direction.Right, _map);
                handled = true;
            }

            return handled;
        }

        private void FillBackground()
        {
            Color[] colors = new[] { Color.Yellow, Color.Crimson };
            float[] colorStops = new[] { 0f, 1f };

            Algorithms.GradientFill(_map.FontSize,
                                    _map.Surface.Area.Center,
                                    _map.Surface.Width / 3,
                                    45,
                                    _map.Surface.Area,
                                    new Gradient(colors, colorStops),
                                    (x, y, color) => _map.Surface[x, y].Background = color);
        }
    }
}
