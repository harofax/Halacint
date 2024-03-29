﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace Halacint
{
    internal class DebugLog : Console
    {
        private readonly ClassicConsoleKeyboardHandler _keyboardHandler;

        public delegate void NotifyConsoleCleared();

        public event NotifyConsoleCleared ConsoleCleared;

        public DebugLog(int width, int height, int bufferHeight) : base(width - 1, height, width - 1, bufferHeight)
        {
            this.DefaultBackground = Color.AnsiBlack;
            this.Clear();

            _keyboardHandler = new ClassicConsoleKeyboardHandler(">");
            _keyboardHandler.EnterPressedAction = EnterPressedAction;
            SadComponents.Add(_keyboardHandler);

            Cursor.IsVisible = true;
            Cursor.IsEnabled = true;
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            _keyboardHandler.ProcessKeyboard(this, keyboard, out bool handled);

            return handled;
        }

        public void ClearText()
        {
            this.Clear();
            Cursor.Position = new Point(0, 0);
            _keyboardHandler.CursorLastY = 0;
            OnConsoleCleared();
        }

        protected virtual void OnConsoleCleared()
        {
            ConsoleCleared?.Invoke();
        }

        public void EnterPressedAction(ClassicConsoleKeyboardHandler keyboardComponent, Cursor cursor, string value)
        {
            switch (value.ToLower())
            {
                case "help":
                    Cursor.NewLine().
                        Print("  HELP ").NewLine().
                        Print("  =======================================").NewLine().NewLine().
                        Print("  help      - Display this help info").NewLine().
                        Print("  ver       - Display version info").NewLine().
                        Print("  cls       - Clear the screen").NewLine().
                        Print("  ").NewLine();
                    break;
                case "ver":
                    Cursor.Print("v.0.0.0.0.1 Halacint").NewLine();
                    break;
                case "cls":
                    ClearText();
                    break;
                case "exit":
                    ClearText();
                    Parent.IsVisible = false;
                    break;
                default:
                    Cursor.Print(value + " is an unknown command").NewLine();
                    break;
            }
        }

        
    }
}
