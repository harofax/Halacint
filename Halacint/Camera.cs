using SadConsole;
using SadConsole.Entities;
using SadConsole.UI;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using System;
using Console = SadConsole.Console;

namespace Halacint
{
    internal class Camera : Console
    {
        private IScreenObject _target;
        public IScreenObject Target
        {
            get => _target;
            set
            {
                _target = value;
                FollowTarget = true;
            }
        }

        private Point _targetPoint;
        public Point TargetPoint
        {
            get => _targetPoint;
            set
            {
                _targetPoint = value;
                FollowPoint = true;
            }
        }

        private bool _followTarget;
        public bool FollowTarget
        {
            get => _followTarget;
            set {
                if (value == true && _followPoint == true) { FollowPoint = false; }
                _followTarget = value; 
            }
        }

        private bool _followPoint;
        public bool FollowPoint
        {
            get => _followPoint;
            set {
                if (value == true && _followTarget == true) { FollowTarget = false; }
                _followPoint = value; 
            }
        }

        public IScreenSurface DisplaySurface;

        public Camera(ref World world, int width, int height) : base(world.cells)
        {
            Resize(width, height, world.cells.Width, world.cells.Height, false);
            DisplaySurface = this;
        }

        public void UpdateCameraPos()
        {
            Entity ent = (Entity)Target;
            Surface.View = Surface.View.WithCenter(ent.AbsolutePosition);
            IsDirty = true;
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            if (_followTarget)
            {
                if (Target == null) return;
                if (Target is Entity ent)
                {
                    DisplaySurface.Surface.View = DisplaySurface.Surface.View.WithCenter(ent.AbsolutePosition);
                } 
                else if (Target is IScreenSurface screenSurface)
                {
                    DisplaySurface.Surface.View = DisplaySurface.Surface.View.WithCenter(screenSurface.UsePixelPositioning ? screenSurface.AbsolutePosition / DisplaySurface.FontSize : screenSurface.AbsolutePosition);
                }
                else
                {
                    DisplaySurface.Surface.View = DisplaySurface.Surface.View.WithCenter(Target.Position);
                }
            }
            else if (_followPoint)
            {
                DisplaySurface.Surface.View = DisplaySurface.Surface.View.WithCenter(TargetPoint);
            }
        }
    }
}