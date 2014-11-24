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

            switch(state.GetPressedKeys()[0])
            {
                case Keys.Up:
                    // walking or moving menu cursor up
                    break;
                case Keys.Down:
                    // walking or moving menu cursor down
                    break;
                case Keys.Left:
                    // walking or moving menu cursor left
                    break;
                case Keys.Right:
                    // walking or moving menu cursor right
                    break;
                case Keys.Escape:
                    // opening pause menu
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
