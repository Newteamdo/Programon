using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programon
{
    class KeyHandler
    {
        private MainWindow GameWindow { get; set; }

        private int KeyCooldownTimer { get; set; }

        public KeyHandler(MainWindow gameWindow)
        {
            this.GameWindow = gameWindow;

        }

        public void KeyPress()
        {
            KeyboardState state = Keyboard.GetState();

            Keys[] PressedKeys = state.GetPressedKeys();

            if (PressedKeys.Length == 0) return;

            switch(PressedKeys[0])
            {
                case Keys.Up:
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X, GameWindow.DrawPlane.Y + 1, GameWindow.DrawPlane.Width, GameWindow.DrawPlane.Height + 1);
                    break;
                case Keys.Down:
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X, GameWindow.DrawPlane.Y - 1, GameWindow.DrawPlane.Width, GameWindow.DrawPlane.Height - 1);
                    break;
                case Keys.Left:
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X + 1, GameWindow.DrawPlane.Y, GameWindow.DrawPlane.Width + 1, GameWindow.DrawPlane.Height);
                    break;
                case Keys.Right:
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X - 1, GameWindow.DrawPlane.Y, GameWindow.DrawPlane.Width - 1, GameWindow.DrawPlane.Height);
                    break;
                case Keys.Escape:
                    GameWindow.Exit();
                    break;
                case Keys.Z:
                    // action button or agree button
                    break;
                case Keys.X:
                    // cancel button or back button
                    break;
            }
        }
    }
}
