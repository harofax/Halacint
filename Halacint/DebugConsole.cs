using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using System;
using SadConsole.Input;
using Console = SadConsole.Console;

namespace Halacint
{
    // public because of eventual mod support maybe idk
    public class DebugConsole : Window
    {
        public int ViewableWidth, ViewableHeight;

        public DebugConsole(int width, int height, int historyLength = 1000) : base(width, height, width, historyLength)
        {
            ViewableWidth = width;
            ViewableHeight = height;

            FocusOnMouseClick = true;
            
            FocusedMode = FocusBehavior.Push;
            Title = "[ D E B U G ]";
            IsModalDefault = true;
            
        }

        protected override void DrawBorder()
        {
            Surface.DrawBox(new Rectangle(0, 0, ViewableWidth, ViewableHeight), 
                ShapeParameters.CreateStyledBox((int[])ICellSurface.ConnectedLineThin, 
                    new ColoredGlyph(Color.Orange, Color.AnsiBlack)));

            // Draw title
            string adjustedText = "";
            int adjustedWidth = Width - 2;
            TitleAreaLength = 0;
            TitleAreaX = 0;

            if (!string.IsNullOrEmpty(Title))
            {
                if (Title.Length > adjustedWidth)
                    adjustedText = Title[..^(Title.Length - adjustedWidth)];
                else
                    adjustedText = Title;
            }

            if (!string.IsNullOrEmpty(adjustedText))
            {
                TitleAreaLength = adjustedText.Length;

                if (TitleAlignment == HorizontalAlignment.Left)
                    TitleAreaX = 1;
                else if (TitleAlignment == HorizontalAlignment.Center)
                    TitleAreaX = ((adjustedWidth - adjustedText.Length) / 2) + 1;
                else
                    TitleAreaX = Width - 1 - adjustedText.Length;

                Surface.Print(TitleAreaX, TitleAreaY, adjustedText);
            }

            IsDirty = true;
        }

        public void ToggleDebugConsole()
        {
            if (IsVisible) HideDebugConsole();
            else ShowDebugConsole();
        }

        public void ShowDebugConsole()
        {
            Show();
            //DrawBorder();
            //
            //IsVisible = true;
            //IsFocused = true;
        }

        public void HideDebugConsole()
        {
            Hide();
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            if (keyboard.IsKeyPressed(Keys.K))
            {
                HideDebugConsole();
                return true;
            }

            return false;
        }
    }
}