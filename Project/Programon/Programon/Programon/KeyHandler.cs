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

            if (state.IsKeyDown(Keys.Up))
            {
                if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitY))
                    GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitY]);
            }

            else if (state.IsKeyDown(Keys.Down))
            {
                if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitY))
                    GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitY]);
            }

            else if (state.IsKeyDown(Keys.Left))
            {
                if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitX))
                    GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitX]);
            }

            else if (state.IsKeyDown(Keys.Right))
            {
                if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitX))
                    GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitX]);
            }

            if (state.IsKeyDown(Keys.Escape))
            {
                GameWindow.Exit();
            }

            if (state.IsKeyDown(Keys.Z))
            {
                // action button or agree button
            }

            if (state.IsKeyDown(Keys.X))
            {
                // cancel button or back button
            }

            if(state.IsKeyDown(Keys.F1))
            {
                GameWindow.SetState(GameState.BATTLE);
            }
            
            if(state.IsKeyDown(Keys.F2))
            {
                GameWindow.SetState(GameState.OVERWORLD);
            }

            if(state.IsKeyDown(Keys.F3))
            {
                GameWindow.SetState(GameState.PROGRAMONSCREEN);
            }
        }
    }
}
