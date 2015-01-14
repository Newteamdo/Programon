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

        public GameState State { get; private set; }

        private BattleScreen testBattle;
        private MainMenuWindow menuWindow;
        private ProgramonMenu programonMenu;

        private InventoryMenu inventoryMenu;
        private OptionsMenu OptionsMenu { get; set; }

        public MainWindow()
        {
            SpriteDrawer = new SpriteDrawer(this, new Rectangle(0, 0, 1024, 720), false);
            MainCamera = new Camera(Vector2.Zero);
            menuWindow = new MainMenuWindow(this);
            programonMenu = new ProgramonMenu(SpriteDrawer, this);
            Keyhandler = new KeyHandler(this);

            XmlLoader.LoadSettings(this, SpriteDrawer, CONFIGLOCATION);
            Content.RootDirectory = "Content";
            State = GameState.MAINMENU;

            IsMouseVisible = true;


            /*Debugging!*/
            SpriteDrawer.Debug = true;
        }

        protected override void Initialize()
        {
            menuWindow.initialize();
            programonMenu.Initialize();
            MainCamera.Initialize(GraphicsDevice.Viewport.Bounds);

            OptionsMenu = new OptionsMenu(this, SpriteDrawer);
            Player = new Player(new Vector2(1, 1), new Vector2(4, 4), Map);
            Player.Inventory.AddItem(new Item("This is an item in the inventory property of player.", "This is an nice item description. This is supposed to work.:)", new Sprite()),10);
            inventoryMenu = new InventoryMenu(Player, this);
            inventoryMenu.Initialize();
            base.Initialize();
        }

        public void ReInit()
        {
            MainCamera.Initialize(SpriteDrawer.BufferSize);
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
                    Player.Load(Content, "Player/TempPlayer_Stand");
                    Player.LoadAnimation(Content, AnimationTypes.Walking, "Player/TempPlayer_Walk01", "Player/TempPlayer_Walk02", "Player/TempPlayer_Walk03", "Player/TempPlayer_Walk04", "Player/TempPlayer_Walk05");
                    Map = XmlLoader.LoadMap(this, MAPLOCATION);
                    break;
                case GameState.OVERWORLD:
                    Map = XmlLoader.LoadMap(this, MAPLOCATION);
                    break;
                case GameState.BATTLE:
                    testBattle.Load(Content, "Fonts/GuiFont_Large", "Fonts/GuiFont_Medium");
                    break;
            }
        }

        protected override void UnloadContent()
        {
            Map = null;
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.Keyhandler.KeyPress(gameTime);

            switch (State)
            {
                case GameState.MAINMENU:
                    menuWindow.Update();
                    break;
                case GameState.OPTIONS:
                    OptionsMenu.Update();
                    break;
                case GameState.OVERWORLD:
                case GameState.NEWGAME:
                    MainCamera.Update(Player.FixedPosition, Map.Size);
                    SpriteDrawer.Update(Map.MapDictionary, MainCamera);
                    break;
                case GameState.BATTLE:
                    testBattle.Update(DeltaTime);
                    break;
                case GameState.PROGRAMONSCREEN:
                    programonMenu.Update();
                    break;

                case GameState.INVENTORY:
                    inventoryMenu.Update();
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
                case GameState.MAINMENU:
                    SpriteDrawer.DrawGUI(menuWindow);
                    break;
                case GameState.OPTIONS:
                    SpriteDrawer.DrawGUI(OptionsMenu);
                    break;
                case GameState.OVERWORLD:
                case GameState.NEWGAME:
                    SpriteDrawer.Draw(MainCamera, Player);
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
