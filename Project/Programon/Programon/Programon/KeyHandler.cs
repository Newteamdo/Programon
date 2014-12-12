using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (GameWindow.Player.FixedPosition.Y - GameWindow.GraphicsDevice.Viewport.Height / 2 > 0 &&
                    GameWindow.MapBounds.Height * Sprite.TextureHeight - GameWindow.Player.FixedPosition.Y > GameWindow.GraphicsDevice.Viewport.Height / 2)
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X, GameWindow.DrawPlane.Y + 1, GameWindow.DrawPlane.Width, GameWindow.DrawPlane.Height + 1);

                if (GameWindow.BackGround.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitY))
                    GameWindow.Player.Move(GameWindow.BackGround[GameWindow.Player.Transform.Position - Vector2.UnitY]);
            }

            else if (state.IsKeyDown(Keys.Down))
            {
                if (GameWindow.Player.FixedPosition.Y - GameWindow.GraphicsDevice.Viewport.Height / 2 > 0 &&
                    GameWindow.MapBounds.Height * Sprite.TextureHeight - GameWindow.Player.FixedPosition.Y > GameWindow.GraphicsDevice.Viewport.Height / 2)
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X, GameWindow.DrawPlane.Y - 1, GameWindow.DrawPlane.Width, GameWindow.DrawPlane.Height - 1);

                if (GameWindow.BackGround.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitY))
                    GameWindow.Player.Move(GameWindow.BackGround[GameWindow.Player.Transform.Position + Vector2.UnitY]);
            }

            else if (state.IsKeyDown(Keys.Left))
            {
                if (GameWindow.Player.FixedPosition.X - GameWindow.GraphicsDevice.Viewport.Width / 2 > 0 &&
                    GameWindow.MapBounds.Width * Sprite.TextureWidth - GameWindow.Player.FixedPosition.X > GameWindow.GraphicsDevice.Viewport.Width / 2)
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X + 1, GameWindow.DrawPlane.Y, GameWindow.DrawPlane.Width + 1, GameWindow.DrawPlane.Height);

                if (GameWindow.BackGround.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitX))
                    GameWindow.Player.Move(GameWindow.BackGround[GameWindow.Player.Transform.Position - Vector2.UnitX]);
            }

            else if (state.IsKeyDown(Keys.Right))
            {
                if (GameWindow.Player.FixedPosition.X - GameWindow.GraphicsDevice.Viewport.Width / 2 > 0 &&
                    GameWindow.MapBounds.Width * Sprite.TextureWidth - GameWindow.Player.FixedPosition.X > GameWindow.GraphicsDevice.Viewport.Width / 2)
                    GameWindow.DrawPlane = new Rectangle(GameWindow.DrawPlane.X - 1, GameWindow.DrawPlane.Y, GameWindow.DrawPlane.Width - 1, GameWindow.DrawPlane.Height);

                if (GameWindow.BackGround.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitX))
                    GameWindow.Player.Move(GameWindow.BackGround[GameWindow.Player.Transform.Position + Vector2.UnitX]);
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
