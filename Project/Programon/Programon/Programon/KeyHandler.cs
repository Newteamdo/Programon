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
               EventDelay = 140;
          }

          public void KeyPress(GameTime gameTime)
          {
               GameWindow.Player.Update();

               if (gameTime.TotalGameTime.TotalMilliseconds >= NextKeyStamp)
               {
                    NextKeyStamp = gameTime.TotalGameTime.TotalMilliseconds + EventDelay;

                    KeyboardState state = Keyboard.GetState();

                    if (state.GetPressedKeys().Length <= 0)
                         return;

                    switch (state.GetPressedKeys()[0])
                    {
                         case (Keys.Escape):
                              GameWindow.Exit();
                              break;
                         case (Keys.Up):
                              if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitY))
                                   GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitY], GameWindow.actors);
                              break;
                         case (Keys.Down):
                              if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitY))
                                   GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitY], GameWindow.actors);
                              break;
                         case (Keys.Left):
                              if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position - Vector2.UnitX))
                                   GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position - Vector2.UnitX], GameWindow.actors);
                              break;
                         case (Keys.Right):
                              if (GameWindow.Map.MapDictionary.ContainsKey(GameWindow.Player.Transform.Position + Vector2.UnitX))
                                   GameWindow.Player.Move(GameWindow.Map.MapDictionary[GameWindow.Player.Transform.Position + Vector2.UnitX], GameWindow.actors);
                              break;
                         case (Keys.Z):
                              Vector2 playerPosition = GameWindow.Player.Transform.Position;
                              for (int i = 0; i < GameWindow.actors.Length; i++)
                              {
                                   Vector2 npcPosition = GameWindow.actors[i].Transform.Position;
                                   if (playerPosition + new Vector2(0f, 1f) == npcPosition || playerPosition + new Vector2(0f, -1f) == npcPosition || playerPosition + new Vector2(-1f, 0f) == npcPosition || playerPosition + new Vector2(1f, 0f) == npcPosition)
                                   {
                                        //Player is next to npc and interact button has been pressed.
                                        if (GameWindow.actors[i] is NPC)
                                        {
                                             NPC npc = GameWindow.actors[i] as NPC;
                                             npc.DisplayDialog();
                                        }
                                   }
                              }

                              // action button or agree button
                              break;
                         case (Keys.X):
                              // cancel button or back button
                              break;
                         case Keys.I:
                              GameWindow.SetState(GameState.INVENTORY);
                              break;
                         case Keys.P:
                              GameWindow.SetState(GameState.PORTABLECOMTAKDEVICE);
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
                         case Keys.F4:
                              GameWindow.SetState(GameState.DIALOG);
                              break;
                         case Keys.F5:
                              GameWindow.SetState(GameState.MAINMENU);
                              break;
                         default:
                              break;
                    }
               }
          }
     }
}