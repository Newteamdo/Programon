using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProgramonEngine;

namespace Programon
{
    public class KeyHandler
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

            if (state.GetPressedKeys().Length <= 0) return;

            switch (state.GetPressedKeys()[0])
            {
                case (Keys.Escape):
                    GameWindow.Exit();
                    return;
                case (Keys.Up):
                    if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitY))
                        GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitY]);
                    return;
                case (Keys.Down):
                    if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitY))
                        GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitY]);
                    return;
                case (Keys.Left):
                    if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitX))
                        GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitX]);
                    return;
                case (Keys.Right):
                    if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitX))
                        GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitX]);
                    return;
                case (Keys.Z):
                    // action button or agree button
                    return;
                case (Keys.X):
                    // cancel button or back button
                    return;
                case (Keys.F1):
                    GameWindow.SetState(GameState.BATTLE);
                    return;
                case (Keys.F2):
                    GameWindow.SetState(GameState.OVERWORLD);
                    return;
                case (Keys.F3):
                    GameWindow.SetState(GameState.PROGRAMONSCREEN);
                    return;
                default:
                    return;
            }
        }
    }
}
