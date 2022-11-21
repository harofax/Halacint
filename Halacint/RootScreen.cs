using System;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Input;
using SadConsole.UI;
using SadRogue.Primitives;
using Color = SadRogue.Primitives.Color;

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

        public RootScreen()
        {
            ColoredGlyph playerGlyph = new ColoredGlyph(Color.Orange, Color.Transparent, '@');
            player = new Entity( playerGlyph, 50);
            player.Position = new Point(2, 2);


            world = new World( 200, 200 );
            cam = new Camera(ref world, Game.Instance.ScreenCellsX, Game.Instance.ScreenCellsY);
            cam.Position = (2,2);

            cam.Resize(90, 30, 200, 200, false);


            var borderParams = Border.BorderParameters.GetDefault()
                .AddTitle("CAMERA", Color.Black, Color.White)
                .ChangeBorderColors(Color.White, Color.Black)
                .AddShadow();

            Border border = new(cam, borderParams);

            cam.Target = player;
            cam.FollowTarget = true;

            entityManager = new Manager();
            var worldEntity = new Manager();

            //worldEntity.Add(player);
            //world.SadComponents.Add(worldEntity);
            //world.SadComponents.Add(entityManager);
            cam.SadComponents.Add(entityManager);
            entityManager.Add(player);

            cam.SadComponents.Add(new SadConsole.Components.SurfaceComponentFollowTarget() { Target = player });


            player.IsVisible = true;

            //world.IsVisible = true;
            cam.IsVisible = true;

            debug_console = new DebugConsole(40, Game.Instance.ScreenCellsY - 4);

            //var mainview = new ScreenSurface(60, 23);
            //var subview = new ScreenSurface(mainview.Surface.GetSubSurface(new Rectangle(0,0,23,20)));

            //_map = new ScreenSurface(Game.Instance.ScreenCellsX, Game.Instance.ScreenCellsY);
            //_map.UseMouse = false;
            //_map.FocusOnMouseClick = false;

            //_debug = new Console(40, Game.Instance.ScreenCellsY - 2);
            //_debug.Position = new Point(1, 1);
            //_debug.FocusOnMouseClick = true;
            //_debug.IsVisible = false;
            //_debug.DefaultBackground = Color.AnsiBlack;
            
            //Border.BorderParameters borderParams = Border.BorderParameters.GetDefault();
            //borderParams.AddTitle("[ D E B U G ]");
            //borderParams.DrawBorder = true;
            //borderParams.ChangeBorderForegroundColor(Color.Yellow);
            //borderParams.TitleBackground = Color.Yellow;
            //borderParams.TitleForeground = Color.AnsiBlack;
            //
            //_debug.Print(2, 2, new ColoredString("yo"));
            //
            //var border = new Border(_debug, borderParams);

            //FillBackground();

            //Children.Add(world);
            Children.Add(cam);
            Children.Add(player);
            //Children.Add(_debug);

            Children.MoveToTop(player);

            //_controlledObject = new GameObject(new ColoredGlyph(Color.Yellow, Color.Black, 2), new Point(5, 5), world);
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
                cam.TargetPoint = cam.Surface.Area.Center;
                newPosition = player.Position;
                handled = true;
            }

            if (handled)
            {
                if (cam.Surface.Area.Contains(newPosition))
                {
                    cam.Surface.SetGlyph(player.Position.X, player.Position.Y, 250, Color.Purple);
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
            cam.Update(delta);
        }

        public override void Render(TimeSpan delta)
        {
            base.Render(delta);
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
