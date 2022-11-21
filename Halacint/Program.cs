using System;
using Halacint;
using SadConsole;
using SadConsole.Components;
using SadConsole.UI;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace Halacint
{
    public static class Program
    {
        private static int default_width = 128;
        private static int default_height = 38;

        static void Main()
        {
            // Setup the engine and create the main window.
            Game.Create(default_width, default_height);

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

            // Start the game.
            Game.Instance.Run();


            // Won't run until game window closes
            Game.Instance.Dispose();
        }

        static void Init()
        {
            var mainScreen = new RootScreen();

            Game.Instance.Screen = mainScreen;
            Game.Instance.Screen.IsFocused = true;

            Game.Instance.DestroyDefaultStartingConsole();

        }
    }
}