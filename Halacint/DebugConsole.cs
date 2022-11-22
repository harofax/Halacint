using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using System;
using SadConsole.Input;
using Console = SadConsole.Console;

namespace Halacint
{
    // public because of eventual mod support maybe idk
    public class DebugConsole : Console
    {
        public DebugConsole(int width, int height) : base(width, height, width, 1000)
        {
            FocusOnMouseClick = true;
            IsVisible = false;
            DefaultBackground = SadRogue.Primitives.Color.AnsiBlack;
            FocusedMode = FocusBehavior.Push;

            Border.BorderParameters borderParams = Border.BorderParameters.GetDefault();
            borderParams.AddTitle("[ D E B U G ]");
            borderParams.DrawBorder = true;
            borderParams.ChangeBorderForegroundColor(Color.Yellow);
            borderParams.TitleBackground = Color.Yellow;
            borderParams.TitleForeground = Color.AnsiBlack;
            

            this.Print(2, 2, new ColoredString("yo"));
            
            var border = new Border(this, borderParams);

            //OnVisibleChanged += HideDebugConsole;
        }

        public void ToggleDebugConsole()
        {
            if (IsVisible) HideDebugConsole();
            else ShowDebugConsole();
        }

        public void ShowDebugConsole()
        {
            IsVisible = true;
            IsFocused = true;
        }

        public void HideDebugConsole()
        {
            IsVisible = false;
            IsFocused = false;
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