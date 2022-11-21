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
            get
            {
                return _target;
            }
            set
            {
                _target = value;
                FollowTarget = true;
            }
        }

        private Point _targetPoint;
        public Point TargetPoint
        {
            get
            {
                return _targetPoint;
            }
            set
            {
                _targetPoint = value;
                FollowPoint = true;
            }
        }

        private bool _followTarget;
        public bool FollowTarget
        {
            get { return _followTarget; }
            set {
                if (value == true && _followPoint == true) { FollowPoint = false; }
                _followTarget = value; 
            }
        }

        private bool _followPoint;
        public bool FollowPoint
        {
            get { return _followPoint; }
            set {
                if (value == true && _followTarget == true) { FollowTarget = false; }
                _followPoint = value; 
            }
        }

        public IScreenSurface displaySurface;

        private World world;

        public Camera(ref World world, int width, int height) : base(world.cells)
        {
            Resize(width, height, world.cells.Width, world.cells.Height, false);
            this.world = world;
            displaySurface = this;

            

            

        }

        public void SetCameraPos()
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
                if (Target != null)
                {
                    if (Target is Entity ent)
                    {
                        displaySurface.Surface.View = displaySurface.Surface.View.WithCenter(ent.AbsolutePosition);
                    } 
                    else if (Target is IScreenSurface screenSurface)
                    {
                        displaySurface.Surface.View = displaySurface.Surface.View.WithCenter(screenSurface.UsePixelPositioning ? screenSurface.AbsolutePosition / displaySurface.FontSize : screenSurface.AbsolutePosition);
                    }
                    else
                    {
                        displaySurface.Surface.View = displaySurface.Surface.View.WithCenter(Target.Position);
                    }
                }
            }
            else if (_followPoint)
            {
                displaySurface.Surface.View = displaySurface.Surface.View.WithCenter(TargetPoint);
            }
        }
    }
}