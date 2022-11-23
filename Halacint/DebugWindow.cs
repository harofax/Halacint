using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using System;
using SadConsole.Components;
using SadConsole.Input;
using SadConsole.UI.Controls;
using Console = SadConsole.Console;

namespace Halacint
{
    // public because of eventual mod support maybe idk
    public class DebugWindow : Window
    {
        public int ViewableWidth, ViewableHeight;

        private int i = 0;

        public int ScrollOffset { get; set; } = 0;


        public readonly SadConsole.UI.Controls.ScrollBar _scrollBar;


        private readonly DebugConsole _log;

        public DebugWindow(int width, int height, int historyLength = 1000) : base(width, height)
        {
            _log = new DebugConsole(width-2, height-2, historyLength);
            _log.Position = (1, 1);

            _scrollBar = new SadConsole.UI.Controls.ScrollBar(Orientation.Vertical, height - 2);
            _scrollBar.IsEnabled = false;
            _scrollBar.ValueChanged += ScrollBar_ValueChanged;
            _scrollBar.Position = (width - 2, 1);

            this.Controls.Add(_scrollBar);

            UseKeyboard = true;
            UseMouse = true;

            DefaultBackground = Color.AnsiBlue;
            this.Clear();

            _log.IsFocused = true;

            //ViewBorder viewBorder = new ViewBorder(width, height);
            //viewBorder.Position = (-1, -1);
            //Children.Add(viewBorder);

            ViewableWidth = width;
            ViewableHeight = height;

            //FocusedMode = FocusBehavior.Push;
            Title = "[ D E B U G ]";
            IsModalDefault = true;

            Children.Add(_log);
            Children.MoveToTop(_log);
            
            //Children.MoveToTop(_controlsContainer);
        }

        private void OnMouseEnter(object sender, MouseScreenObjectState e)
        {
            Title = "WOWOWOWOWO";
            IsDirty = true;
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _log.View = new Rectangle(0, _scrollBar.Value, _log.Width, _log.ViewHeight);
        }


        //protected override void DrawBorder()
        //{
        //    // override the border drawing, since we only wanna draw the title,
        //    // the border we make ourselves with ViewBorder
        //
        //    string adjustedText = "";
        //    int adjustedWidth = Width - 2;
        //    TitleAreaLength = 0;
        //    TitleAreaX = 0;
        //
        //    if (!string.IsNullOrEmpty(Title))
        //    {
        //        if (Title.Length > adjustedWidth)
        //            adjustedText = Title[..^(Title.Length - adjustedWidth)];
        //        else
        //            adjustedText = Title;
        //    }
        //
        //    if (!string.IsNullOrEmpty(adjustedText))
        //    {
        //        TitleAreaLength = adjustedText.Length;
        //
        //        if (TitleAlignment == HorizontalAlignment.Left)
        //            TitleAreaX = 1;
        //        else if (TitleAlignment == HorizontalAlignment.Center)
        //            TitleAreaX = ((adjustedWidth - adjustedText.Length) / 2) + 1;
        //        else
        //            TitleAreaX = Width - 1 - adjustedText.Length;
        //
        //        Surface.Print(TitleAreaX, TitleAreaY, adjustedText, Color.Yellow);
        //    }
        //
        //    IsDirty = true;
        //}

        public void ToggleDebugConsole()
        {
            if (IsVisible) HideDebugConsole();
            else ShowDebugConsole();
        }

        public void ShowDebugConsole()
        {
            Show();
            _log.IsFocused = true;
        }

        public void HideDebugConsole()
        {
            Hide();
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            base.ProcessKeyboard(keyboard);
            if (keyboard.IsKeyPressed(Keys.Escape))
            {
                HideDebugConsole();
                return true;
            }
            
            return _log.ProcessKeyboard(keyboard);
        }

        

        public override void Update(TimeSpan delta)
        {
            // Update our console and then update the scroll bar
            base.Update(delta);

            //If cursor position exceeds our displayable content viewport, 
            //move the ScrollOffset automatically to display new content.
            if (_log.TimesShiftedUp != 0 | _log.Cursor.Position.Y >= _log.ViewHeight + ScrollOffset)
            {
                //Scollbar has to be enabled to read previous content.
                _scrollBar.IsEnabled = true;
                //Cursor offset cannot exceed our viewable data end row.
                //Think about it, we would reach infinity and empty space D:
                if (ScrollOffset < _log.Height - _log.ViewHeight)
                {
                    //Automatically calculate our content viewport by scrolling the cursor
                    //Based on how much content is inaccessible.
                    ScrollOffset += _log.TimesShiftedUp != 0 ? _log.TimesShiftedUp : 1;
                }
                _scrollBar.Maximum = (_log.Height + ScrollOffset) - _log.Height;

                //This will follow the cursor since we move the render area in the event.
                _scrollBar.Value = ScrollOffset;

                // Reset the shift amount.
                _log.TimesShiftedUp = 0;
            }
        }

        
    }
}