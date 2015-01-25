using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProgramonEngine;
using System.Xml;
using System.IO;

namespace Programon
{
     public class MainWindow : Game
     {
          public const string CONFIGLOCATION = "config.xml";
          private const string MAPLOCATION = "Maps/mapnew1.xml";

          private Camera MainCamera { get; set; }
          private SpriteDrawer SpriteDrawer { get; set; }
          private KeyHandler Keyhandler { get; set; }

          private List<Creature> enemies;

          public Player Player { get; private set; }
          public Map Map { get; private set; }

          public double VolumeLevel { get; set; }
          public float DeltaTime { get; private set; }
          public int laststate;
          private Background introBackground;

          public GameState State { get; private set; }

          private BattleScreen testBattle;
          private MainMenuWindow menuWindow;
          private ProgramonMenu programonMenu;

          private InventoryMenu inventoryMenu;
          private InventoryMenu portableComtakDevice;
          private OptionsMenu OptionsMenu { get; set; }

          private ScreenAnimation ScreenAnimation;
          private bool ShowScreenAnimation;
          private GameState AfterScreenAnimation;

          public Actor[] actors;

          private DialogueBox dialog;

          bool firstRun = true;
          double NextTimeToSwitch = 0;

          public MainWindow()
          {
               enemies = new List<Creature> { };
               SpriteDrawer = new SpriteDrawer(this, new Rectangle(0, 0, 1024, 720), false);
               MainCamera = new Camera(Vector2.Zero);
               menuWindow = new MainMenuWindow(this);

               Keyhandler = new KeyHandler(this);
               XmlLoader.LoadSettings(this, SpriteDrawer, CONFIGLOCATION);
               Content.RootDirectory = "Content";
               State = GameState.INTRO;

               IsMouseVisible = true;

               ShowScreenAnimation = false;
          }

          protected override void Initialize()
          {
               menuWindow.initialize();
               programonMenu = new ProgramonMenu(SpriteDrawer, this);
               programonMenu.Initialize();
               MainCamera.Initialize(GraphicsDevice.Viewport.Bounds);

               Ability[] enemieRandomAbilities = new Ability[3];
               for (int i = 0; i < 10; i++)
               {
                    if (i < 3)
                    {
                         enemieRandomAbilities[i] = new Ability("Testability" + i, 10, Math.Min(i + 1, 2));
                    }
                    enemies.Add(new Creature(Vector2.Zero, "Programon " + i, (byte)Math.Min((Math.Round(i / 2f) - 2), 5), new Stats(Math.Min(i * 100, 500), Math.Min(i * 100, 500), Math.Min(i * 2, 10), Math.Min(i * 2, 5), 1, Math.Min(i * 2, 2), 1), new Stats(1, 1, 1, 1, 1, 1, 1), enemieRandomAbilities, Map, "This is a test programon"));
               }

               OptionsMenu = new OptionsMenu(this, SpriteDrawer);
               Player = new Player(new Vector2(19, 31), new Vector2(4, 4), Map);
               Player.OnEncounter += Player_OnEncounter;
               Player.Inventory.AddItem(new Item("This is an item in the inventory property of player.", "This is an nice item description. This is supposed to work.:)", new Sprite()), 1);

               portableComtakDevice = new InventoryMenu(Player, this);
               portableComtakDevice.Initialize();
               inventoryMenu = new InventoryMenu(Player, this);
               inventoryMenu.Initialize();

               Player.programons.Add(ProgramonLoader.LoadProgramon("Programons/testprogramon.xml"));
               testBattle = new BattleScreen(Player, GraphicsDevice, SpriteDrawer.BufferSize, ProgramonLoader.LoadProgramon("Programons/testprogramon2.xml"));

               introBackground = new Background(Content.Load<Texture2D>("ProgramonIntro"), SpriteDrawer.BufferSize);

               actors = new Actor[2];
               List<string> dialogTextsNPC1 = XmlLoader.LoadDialog(0, "Dialogs/Dialogs.xml");
               List<string> dialogTextsNPC2 = XmlLoader.LoadDialog(1, "Dialogs/Dialogs.xml");
               actors[0] = new NPC(SpriteDrawer.BufferSize, new Vector2(14, 34), new Vector2(4, 4), Map, new Vector2(12, 26), new Vector2(16, 36), Content.Load<SpriteFont>("Fonts/GuiFont_Medium"), Content.Load<Texture2D>("DialogueBox"), dialogTextsNPC1);
               actors[1] = new NPC(SpriteDrawer.BufferSize, new Vector2(2, 28), new Vector2(4, 4), Map, new Vector2(2, 32), new Vector2(5, 32), Content.Load<SpriteFont>("Fonts/GuiFont_Medium"), Content.Load<Texture2D>("DialogueBox"), dialogTextsNPC2);

               dialog = new DialogueBox("This is a test text for the dialog box. This needs to be changed a time!", Content.Load<SpriteFont>("Fonts/GuiFont_Medium"), false, SpriteDrawer.BufferSize, Content.Load<Texture2D>("DialogueBox"));
               base.Initialize();
          }

          private void Player_OnEncounter(Actor actor)
          {
               if (actor is Player)
               {
                    testBattle = new BattleScreen(actor as Player, GraphicsDevice, SpriteDrawer.BufferSize, ProgramonLoader.LoadProgramon("Programons/testprogramon2.xml"));
                    AnimateScreen(GameState.BATTLE);
               }
          }

          public void ReInit()
          {
               MainCamera.Initialize(SpriteDrawer.BufferSize);
          }

          private int GetlastState()
          {
               if (GameState.PROGRAMONSCREEN != State)
               {
                    laststate = (int)State;
               }
               return (int)State;
          }

          protected override void LoadContent()
          {
               SpriteDrawer.LoadContent(new SpriteBatch(GraphicsDevice), Content);

               switch (State)
               {
                    case GameState.MAINMENU:
                         menuWindow.initialize();
                         break;
                    case GameState.NEWGAME:
                         for (int i = 0; i < actors.Length; i++)
                         {
                              if (actors[i] is NPC)
                              {
                                   NPC npc = actors[i] as NPC;
                                   npc.Load(Content, "Actors/NPC/NPCDown0");
                                   npc.LoadAnimation(Content, AnimationTypes.WalkingLeft, "Actors/NPC/NPCLeft0", "Actors/NPC/NPCLeft1", "Actors/NPC/NPCLeft0", "Actors/NPC/NPCLeft2");
                                   npc.LoadAnimation(Content, AnimationTypes.WalkingUp, "Actors/NPC/NPCUp0", "Actors/NPC/NPCUp1", "Actors/NPC/NPCUp0", "Actors/NPC/NPCUp2");
                                   npc.LoadAnimation(Content, AnimationTypes.WalkingDown, "Actors/NPC/NPCDown0", "Actors/NPC/NPCDown1", "Actors/NPC/NPCDown0", "Actors/NPC/NPCDown2");
                              }
                         }

                         Player.Load(Content, "Actors/Player/PlayerDown0");
                         Player.Animations.Clear();
                         Player.LoadAnimation(Content, AnimationTypes.WalkingLeft, "Actors/Player/PlayerLeft0", "Actors/Player/PlayerLeft1", "Actors/Player/PlayerLeft0", "Actors/Player/PlayerLeft2");
                         Player.LoadAnimation(Content, AnimationTypes.WalkingUp, "Actors/Player/PlayerUp0", "Actors/Player/PlayerUp1", "Actors/Player/PlayerUp0", "Actors/Player/PlayerUp2");
                         Player.LoadAnimation(Content, AnimationTypes.WalkingDown, "Actors/Player/PlayerDown0", "Actors/Player/PlayerDown1", "Actors/Player/PlayerDown0", "Actors/Player/PlayerDown2");
                         Map = XmlLoader.LoadMap(this, MAPLOCATION);
                         break;
                    case GameState.BATTLE:
                         testBattle.Load(Content, "Fonts/GuiFont_Large", "Fonts/GuiFont_Medium");
                         testBattle.InitializeEvents(
                         (sender, e) =>
                         {
                              SetState(GameState.PROGRAMONSCREEN);
                              Update(new GameTime());
                         },
                         (sender, e) =>
                         {
                              SetState(GameState.INVENTORY);
                         },
                         (sender, e) =>
                         {
                              SetState(GameState.OVERWORLD);
                         });
                         break;
               }
          }

          protected override void Update(GameTime gameTime)
          {
               if (ShowScreenAnimation)
               {
                    if (ScreenAnimation.Update(gameTime))
                    {
                         ShowScreenAnimation = false;
                         SetState(AfterScreenAnimation);
                    }
                    return;
               }
               GetlastState();
               if (firstRun)
               {
                    NextTimeToSwitch = gameTime.TotalGameTime.TotalSeconds + 5;
                    firstRun = false;
               }
               DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
               switch (State)
               {
                    case GameState.INTRO:
                         if (gameTime.TotalGameTime.TotalSeconds >= NextTimeToSwitch)
                         {
                              SetState(GameState.MAINMENU);
                         }
                         break;
                    case GameState.MAINMENU:
                         menuWindow.Update();
                         break;
                    case GameState.OPTIONS:
                         OptionsMenu.Update();
                         break;
                    case GameState.NEWGAME:
                         State = GameState.OVERWORLD;
                         goto case GameState.OVERWORLD;
                    case GameState.OVERWORLD:
                         for (int i = 0; i < actors.Length; i++)
                         {
                              if (actors[i] is NPC)
                              {
                                   NPC npc = actors[i] as NPC;
                                   npc.Update(Player, Map, gameTime);
                              }
                         }

                         this.Keyhandler.KeyPress(gameTime);
                         MainCamera.Update(Player.FixedPosition, Map.Size);
                         SpriteDrawer.Update(Map.MapDictionary, MainCamera);
                         break;
                    case GameState.BATTLE:
                         if (testBattle.Update(DeltaTime))
                         {
                              Creature.GetBattleXp(testBattle.enemy, testBattle.curProgramon);
                              testBattle.curProgramon.UpdateStats();
                              testBattle.curProgramon.UpdateBuffDuration();
                              System.Threading.Thread.Sleep(10);
                              SetState(GameState.OVERWORLD);
                         }
                         break;
                    case GameState.PROGRAMONSCREEN:
                         programonMenu.Update();
                         break;
                    case GameState.INVENTORY:
                         inventoryMenu.Update();
                         break;
                    case GameState.PORTABLECOMTAKDEVICE:
                         portableComtakDevice.Update();
                         break;
               }

               base.Update(gameTime);
          }

          public void SetState(GameState newState)
          {
               State = newState;
               UnloadContent();
               LoadContent();
          }


          protected override bool BeginDraw()
          {
               SpriteDrawer.BeginDraw();

               return base.BeginDraw();
          }

          protected override void Draw(GameTime gameTime)
          {
               switch (State)
               {
                    case GameState.INTRO:
                         SpriteDrawer.DrawGUIItem(introBackground);
                         break;
                    case GameState.MAINMENU:
                         SpriteDrawer.DrawGUI(menuWindow);
                         break;
                    case GameState.OPTIONS:
                         SpriteDrawer.DrawGUI(OptionsMenu);
                         break;
                    case GameState.OVERWORLD:
                    case GameState.NEWGAME:
                         SpriteDrawer.Draw(MainCamera, Player, DeltaTime, null, actors);
                         break;
                    case GameState.BATTLE:
                         testBattle.Draw(SpriteDrawer.SpriteBatch);
                         break;
                    case GameState.PROGRAMONSCREEN:
                         SpriteDrawer.DrawGUIItem(programonMenu);
                         break;
                    case GameState.INVENTORY:
                         SpriteDrawer.DrawGUI(inventoryMenu);
                         break;
                    case GameState.PORTABLECOMTAKDEVICE:
                         SpriteDrawer.DrawGUI(portableComtakDevice);
                         break;
                    case GameState.DIALOG:
                         SpriteDrawer.DrawGUIItem(dialog);
                         break;
               }

               if (ShowScreenAnimation)
               {
                    ScreenAnimation.Draw();
               }

               base.Draw(gameTime);
          }

          protected override void EndDraw()
          {
               SpriteDrawer.EndDraw();

               base.EndDraw();
          }

          public void AnimateScreen(GameState afterAnimation)
          {
               ScreenAnimation = new ScreenAnimation(GraphicsDevice, SpriteDrawer.SpriteBatch, Color.Black);
               AfterScreenAnimation = afterAnimation;
               ShowScreenAnimation = true;
          }
     }
}
