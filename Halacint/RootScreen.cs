using System;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Input;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;
using Color = SadRogue.Primitives.Color;
using Console = SadConsole.Console;

namespace Halacint
{
    public class RootScreen : ScreenObject
    {
        private readonly DebugWindow _debugWindow;
        private readonly World _world;
        private UIManager UIManager;
        private readonly Manager _entityManager;
        private readonly Camera _cam;
        private readonly Entity _player;

        public const int WORLD_SIZE_X = 200;
        public const int WORLD_SIZE_Y = 200;

        public const int CAM_SIZE_X = 90;
        public const int CAM_SIZE_Y = 30;

        public RootScreen()
        {
            ColoredGlyph playerGlyph = new ColoredGlyph(Color.Yellow, Color.Transparent, '@');
            _player = new Entity(playerGlyph, 50)
            {
                Position = new Point(2, 2)
            };

            // -------- WORLD ---------
            _world = new World(WORLD_SIZE_X, WORLD_SIZE_Y);

            // -------- CAMERA --------
            _cam = new Camera(ref _world, CAM_SIZE_X, CAM_SIZE_Y);
            _cam.Position = (1, 1);
            _cam.Target = _player;
            _cam.FollowTarget = true;


            // -------- STATUS CONSOLE --------
            Console statusConsole = new Console(Game.Instance.ScreenCellsX - CAM_SIZE_X - 3, 30);
            statusConsole.Position = (CAM_SIZE_X + 2, 1);
            statusConsole.FillWithRandomGarbage(255);

            _entityManager = new Manager();
            _cam.SadComponents.Add(_entityManager);
            _entityManager.Add(_player);

            // -------- DEBUG CONSOLE ---------
            _debugWindow = new DebugWindow(50, Game.Instance.ScreenCellsY - 10);
            _debugWindow.Position = (1, 1);

            Children.Add(statusConsole);
            Children.Add(_cam);
            Children.Add(_player);
            Children.Add(_debugWindow);
            Children.MoveToTop(statusConsole);
            //Children.MoveToTop(cam);
            Children.MoveToTop(_player);
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            bool playerMoved = false;
            Direction moveDir = Direction.None;

            if (keyboard.IsKeyPressed(Keys.Up))
            {
                moveDir = Direction.Up;
                playerMoved = true;
            }
            else if (keyboard.IsKeyPressed(Keys.Down))
            {
                moveDir = Direction.Down;
                playerMoved = true;
            }

            if (keyboard.IsKeyPressed(Keys.Left))
            {
                moveDir = Direction.Left;
                playerMoved = true;
            }
            else if (keyboard.IsKeyPressed(Keys.Right))
            {
                moveDir = Direction.Right;
                playerMoved = true;
            }

            if (keyboard.IsKeyPressed(Keys.Q))
            {
                _cam.Target = _player;
                _cam.UpdateCameraPos();
                return true;
            }

            if (keyboard.IsKeyPressed(Keys.E))
            {
                _cam.TargetPoint = _cam.Surface.Area.Center;
                _cam.UpdateCameraPos();
                return true;
            }

            if (keyboard.IsKeyPressed(Keys.K))
            {
                _debugWindow.ShowDebugConsole();
                //_debugWindow.ToggleDebugConsole();
                return true;
            }

            Point newPos = _player.Position + moveDir;
            if (_world.cells.Area.Contains(newPos))
            {
                Point glyph = (3, 1); // glyph.ToIndex(16)
                _world.cells.SetGlyph(_player.Position.X, _player.Position.Y, ',', Color.PaleGoldenrod);
                _player.Position = newPos;
            }

            return playerMoved;
        }



        public override void Update(TimeSpan delta)
        {
            base.Update(delta);
        }



        //private void FillBackground()
        //{
        //    Color[] colors = new[] { Color.Yellow, Color.Crimson, Color.Yellow, Color.Crimson, Color.Yellow};
        //    float[] colorStops = new[] { 0f, 0.25f, 0.5f, 0.75f, 1f };
        //
        //    Algorithms.GradientFill(world.FontSize,
        //                            world.Surface.Area.Center,
        //                            world.Surface.Width / 5,
        //                            90,
        //                            world.Surface.Area,
        //                            new Gradient(colors, colorStops),
        //                            (x, y, color) => world.Surface[x, y].Background = color);
        //}
    }
}
