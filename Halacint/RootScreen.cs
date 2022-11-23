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
        private DebugConsole debug_console;
        private World world;
        private UIManager UIManager;
        private Manager entityManager;
        private GameObject _controlledObject;
        private Camera cam;
        private Entity player;

        public const int WORLD_SIZE_X = 200;
        public const int WORLD_SIZE_Y = 200;

        public const int CAM_SIZE_X = 90;
        public const int CAM_SIZE_Y = 30;

        public RootScreen()
        {
            ColoredGlyph playerGlyph = new ColoredGlyph(Color.Orange, Color.Transparent, '@');
            player = new Entity( playerGlyph, 50)
            {
                Position = new Point(2, 2)
            };

            // -------- WORLD ---------
            world = new World( WORLD_SIZE_X, WORLD_SIZE_Y);
            
            // -------- CAMERA --------
            cam = new Camera(ref world, CAM_SIZE_X, CAM_SIZE_Y);
            cam.Position = (1,1);
            cam.Target = player;
            cam.FollowTarget = true;
            

            // -------- STATUS CONSOLE --------
            Console statusConsole = new Console(Game.Instance.ScreenCellsX - CAM_SIZE_X - 3, 30);
            statusConsole.Position = (CAM_SIZE_X + 2, 1);
            statusConsole.FillWithRandomGarbage(255);

            entityManager = new Manager();
            cam.SadComponents.Add(entityManager);
            entityManager.Add(player);

            // -------- DEBUG CONSOLE ---------
            debug_console = new DebugConsole(40, Game.Instance.ScreenCellsY - 10);
            debug_console.Position = (1,1);

            Children.Add(statusConsole);
            Children.Add(cam);
            Children.Add(player);
            Children.Add(debug_console);
            Children.MoveToTop(statusConsole);
            //Children.MoveToTop(cam);
            Children.MoveToTop(player);
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            bool handled = false;
            Point newPosition = (0, 0);

            if (keyboard.IsKeyPressed(Keys.Up))
            {
                //_controlledObject.Move(_controlledObject.Position + Direction.Up, world);
                newPosition = player.Position + (Direction.Up);
                handled = true;
            }
            else if (keyboard.IsKeyPressed(Keys.Down))
            {
                //_controlledObject.Move(_controlledObject.Position + Direction.Down, world);
                newPosition = player.Position + (Direction.Down);
                handled = true;
            }
            
            if (keyboard.IsKeyPressed(Keys.Left))
            {
                //_controlledObject.Move(_controlledObject.Position + Direction.Left, world);
                newPosition = player.Position + (Direction.Left);
                handled = true;
            }
            else if (keyboard.IsKeyPressed(Keys.Right))
            {
                //_controlledObject.Move(_controlledObject.Position + Direction.Right, world);
                newPosition = player.Position + (Direction.Right);
                handled = true;
            }

            if (keyboard.IsKeyPressed(Keys.Q))
            {
                //this.IsFocused = true;
                //FillBackground();
                //_debug.IsVisible = true;
                cam.Target = player;
                cam.SetCameraPos();
                newPosition = player.Position;
                handled = true;
            }

            if (keyboard.IsKeyPressed(Keys.E))
            {
                //this.IsFocused = true;
                //FillBackground();
                //_debug.IsVisible = true;
                //cam.TargetPoint = cam.Surface.Area.Center;
                newPosition = player.Position;
                handled = true;
            }

            if (keyboard.IsKeyPressed(Keys.K))
            {
                debug_console.ShowDebugConsole();
                //debug_console.ToggleDebugConsole();
                return true;
            }

            if (handled)
            {
                if (world.cells.Area.Contains(newPosition))
                {
                    world.cells.SetGlyph(player.Position.X, player.Position.Y, 250, Color.Purple);
                    player.Position = newPosition;
                    //cam.Surface.View = cam.Surface.Area.WithCenter(newPosition);
                    //cam.displaySurface.Surface.View = cam.displaySurface.Surface.Area.WithCenter(newPosition);
                    cam.IsDirty = true;
                }
            }

            return handled;
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
