using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProgramonEngine;

namespace Programon
{
    public class KeyHandler
    {
        private MainWindow GameWindow { get; set; }

        private double NextKeyStamp { get; set; }
        private double EventDelay { get; set; }

        public KeyHandler(MainWindow gameWindow)
        {
            this.GameWindow = gameWindow;
            NextKeyStamp = 0;
            
            // Delay between key press event in ms
            EventDelay = 90;
        }

        public void KeyPress(GameTime gameTime)
        {
            GameWindow.Player.Update();

            if (gameTime.TotalGameTime.TotalMilliseconds >= NextKeyStamp)
            {
                NextKeyStamp = gameTime.TotalGameTime.TotalMilliseconds + EventDelay;

                KeyboardState state = Keyboard.GetState();

                if (state.GetPressedKeys().Length <= 0) return;

                switch (state.GetPressedKeys()[0])
                {
                    case (Keys.Escape):
                        GameWindow.Exit();
                        break;
                    case (Keys.Up):
                        if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitY))
                            GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitY]);
                        break;
                    case (Keys.Down):
                        if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitY))
                            GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitY]);
                        break;
                    case (Keys.Left):
                        if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitX))
                            GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitX]);
                        break;
                    case (Keys.Right):
                        if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitX))
                            GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitX]);
                        break;
                    case (Keys.Z):
                        // action button or agree button
                        break;
                    case (Keys.X):
                        // cancel button or back button
                        break;
                    case (Keys.F1):
                        GameWindow.SetState(GameState.BATTLE);
                        break;
                    case (Keys.F2):
                        GameWindow.SetState(GameState.OVERWORLD);
                        break;
                    case (Keys.F3):
                        GameWindow.SetState(GameState.PROGRAMONSCREEN);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
