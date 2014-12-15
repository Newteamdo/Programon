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

        public GameState State { get; private set; }

        private BattleScreen testBattle;
        private MainMenuWindow menuWindow;
        private ProgramonMenu programonMenu;

        private OptionsMenu OptionsMenu { get; set; }

        public MainWindow()
        {
            SpriteDrawer = new SpriteDrawer(this, new Rectangle(0, 0, 1024, 720), false);
            MainCamera = new Camera(Vector2.Zero);
            menuWindow = new MainMenuWindow(this);
            programonMenu = new ProgramonMenu(SpriteDrawer, this);
            Keyhandler = new KeyHandler(this);
            testBattle = new BattleScreen(this);

            XmlLoader.LoadSettings(this, SpriteDrawer, CONFIGLOCATION);
            Content.RootDirectory = "Content";
            State = GameState.OPTIONS;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            menuWindow.initialize();
            programonMenu.Initialize();
            MainCamera.Initialize(GraphicsDevice.Viewport.Bounds);

            OptionsMenu = new OptionsMenu(this, SpriteDrawer);
            Player = new Player(new Vector2(1, 1), new Vector2(4, 4));

            base.Initialize();
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
                    testBattle.Load(Content);
                    break;
            }
        }

        protected override void UnloadContent()
        {
            Map = null;
        }

        protected override void Update(GameTime gameTime)
        {
            this.Keyhandler.KeyPress();

            switch (State)
            {
                case GameState.MAINMENU:
                    menuWindow.Update();
                    break;
                case GameState.NEWGAME:
                    MainCamera.Update(Player.FixedPosition, Map.Size);
                    SpriteDrawer.Update(Map.MapDictionary, MainCamera);
                    break;
                case GameState.OPTIONS:
                    OptionsMenu.Update();
                    break;
                case GameState.OVERWORLD:
                    MainCamera.Update(Player.FixedPosition, Map.Size);
                    SpriteDrawer.Update(Map.MapDictionary, MainCamera);
                    break;
                case GameState.BATTLE:
                    break;
                case GameState.PROGRAMONSCREEN:
                    programonMenu.Update();
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
                case GameState.NEWGAME:
                    SpriteDrawer.Draw(MainCamera, Player);
                    break;
                case GameState.OPTIONS:
                    SpriteDrawer.DrawGUI(OptionsMenu);
                    break;
                case GameState.OVERWORLD:
                    SpriteDrawer.Draw(MainCamera, Player);
                    break;
                case GameState.BATTLE:
                    SpriteDrawer.DrawNodes(testBattle.GuiList, MainCamera);
                    break;
                case GameState.PROGRAMONSCREEN:
                    SpriteDrawer.DrawGUI(new CheaterClass() { Childs = new IGuiItem[1] { programonMenu } });
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
