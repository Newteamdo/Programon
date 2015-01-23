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
        private const string MAPLOCATION = "Maps/map.xml";

        private Camera MainCamera { get; set; }
        private SpriteDrawer SpriteDrawer { get; set; }
        private KeyHandler Keyhandler { get; set; }

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

        public List<Actor> actors = new List<Actor>();

        DialogueBox dialog;

        bool firstRun = true;
        double NextTimeToSwitch = 0;

        public MainWindow()
        {
            SpriteDrawer = new SpriteDrawer(this, new Rectangle(0, 0, 1024, 720), false);
            MainCamera = new Camera(Vector2.Zero);
            menuWindow = new MainMenuWindow(this);

            Keyhandler = new KeyHandler(this);
            XmlLoader.LoadSettings(this, SpriteDrawer, CONFIGLOCATION);
            Content.RootDirectory = "Content";
            State = GameState.INTRO;

            IsMouseVisible = true;

            /*Debugging!*/

            SpriteDrawer.Debug = true;
        }

        protected override void Initialize()
        {
            menuWindow.initialize();
            programonMenu = new ProgramonMenu(SpriteDrawer, this);
            programonMenu.Initialize();
            MainCamera.Initialize(GraphicsDevice.Viewport.Bounds);



            OptionsMenu = new OptionsMenu(this, SpriteDrawer);
            Player = new Player(new Vector2(1, 1), new Vector2(4, 4), Map);
            Player.OnEncounter += Player_OnEncounter;
            Player.Inventory.AddItem(new Item("This is an item in the inventory property of player.", "This is an nice item description. This is supposed to work.:)", new Sprite()), 1);

            //Ability[] ability = new Ability[3] { Ability.Load(Content, "Abilities/Sync_first"), Ability.Load(Content, "Abilities/Terraria"), Ability.Load(Content, "Abilities/Vector_layer") };
            //ProgramonLoader.SaveProgramon(new Creature(new Vector2(0, 0), "TestProgramon", 0x01, new Stats(10, 100, 5, 5, 5, 5, 10), new Stats(), ability, Map, "This is a test programon"), Directory.GetCurrentDirectory() + "/testprogramon");
            //string programonName = ProgramonLoader.LoadProgramon(Directory.GetCurrentDirectory() + "/testprogramon.xml").name;
            //byte programonLevel = ProgramonLoader.LoadProgramon(Directory.GetCurrentDirectory() + "/testprogramon.xml").level;
            //string description = ProgramonLoader.LoadProgramon(Directory.GetCurrentDirectory() + "/testprogramon.xml").description;
            //Player.PortableComtakDevie.AddItem(new Item(programonName, description, new Sprite()), programonLevel);

            portableComtakDevice = new InventoryMenu(Player, this);
            portableComtakDevice.Initialize();
            inventoryMenu = new InventoryMenu(Player, this);
            inventoryMenu.Initialize();



            Player.programons.Add(ProgramonLoader.LoadProgramon("Programons/testprogramon.xml"));
            testBattle = new BattleScreen(Player, GraphicsDevice, SpriteDrawer.BufferSize, ProgramonLoader.LoadProgramon("Programons/testprogramon2.xml"));

            introBackground = new Background(Content.Load<Texture2D>("ProgramonIntro"), SpriteDrawer.BufferSize);

            List<string> dialogTextsNPC1 = XmlLoader.LoadDialog(0, "Dialogs/Dialogs.xml");
            List<string> dialogTextsNPC2 = XmlLoader.LoadDialog(1, "Dialogs/Dialogs.xml");
            actors.Add(new NPC(SpriteDrawer.BufferSize, new Vector2(5, 32), new Vector2(4, 4), Map, new Vector2(4, 25), new Vector2(8, 35), Content.Load<SpriteFont>("Fonts/GuiFont_Medium"), Content.Load<Texture2D>("TestGuiTextures/TestBox"), dialogTextsNPC2));
            actors.Add(new NPC(SpriteDrawer.BufferSize, new Vector2(10, 5), new Vector2(4, 4), Map, new Vector2(8, 3), new Vector2(12, 7), Content.Load<SpriteFont>("Fonts/GuiFont_Medium"), Content.Load<Texture2D>("TestGuiTextures/TestBox"), dialogTextsNPC1));

            dialog = new DialogueBox("This is a test text for the dialog box. This needs to be changed a time!", Content.Load<SpriteFont>("Fonts/GuiFont_Medium"), false, SpriteDrawer.BufferSize, Content.Load<Texture2D>("TestGuiTextures/TestBox"));
            base.Initialize();
        }

        private void Player_OnEncounter(Actor actor)
        {
            if (actor is Player)
            {
                testBattle = new BattleScreen(actor as Player, GraphicsDevice, SpriteDrawer.BufferSize, ProgramonLoader.LoadProgramon("Programons/testprogramon2.xml"));
                SetState(GameState.BATTLE);
            }     
        }

        public void ReInit()
        {
            MainCamera.Initialize(SpriteDrawer.BufferSize);
        }

        private int GetlastState()
        {
            if(GameState.PROGRAMONSCREEN != State)
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
                    for (int i = 0; i < actors.Count; i++)
                    {
                        if (actors[i] is NPC)
                        {
                            NPC npc = actors[i] as NPC;
                            npc.Load(Content, "TempNPC");
                        }
                    }

                    Player.Load(Content, "Player/TempPlayer_Stand");
                    Player.Animations.Clear();
                    Player.LoadAnimation(Content, AnimationTypes.Walking, "Player/TempPlayer_Walk01", "Player/TempPlayer_Walk02", "Player/TempPlayer_Walk03", "Player/TempPlayer_Walk04", "Player/TempPlayer_Walk05");
                    Map = XmlLoader.LoadMap(this, MAPLOCATION);
                    break;
                case GameState.OVERWORLD:
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

        protected override void UnloadContent()
        {
            //Map = null;
        }

        protected override void Update(GameTime gameTime)
        {
            GetlastState();
            if (firstRun)
            {
                NextTimeToSwitch = gameTime.TotalGameTime.TotalSeconds + 5;
                firstRun = false;
            }
            DeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
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
                case GameState.OVERWORLD:
                case GameState.NEWGAME:
                    for (int i = 0; i < actors.Count; i++)
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
                    SpriteDrawer.Draw(MainCamera, Player, null, actors);
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

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            SpriteDrawer.EndDraw();

            base.EndDraw();
        }
    }
}
