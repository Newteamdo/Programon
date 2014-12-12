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
        private const string MAPLOCATION = "map.xml";

        private SpriteDrawer spriteDrawer { get; set; }
        private KeyHandler Keyhandler { get; set; }

        private Texture2D TestTexture { get; set; }
        private Texture2D TestTextBoxCenter { get; set; }

        public Actor Player { get; set; }
        public Map Map { get; private set; }

        public Rectangle DrawPlane { get; set; }
        public Rectangle MapBounds { get; set; }

        public double VolumeLevel { get; set; }

        public GameState State { get; set; }

        private BattleScreen testBattle;
        private MainMenuWindow menuWindow;
        private ProgramonMenu programonMenu;

        private OptionsMenu OptionsMenu { get; set; }

        public MainWindow()
        {
            spriteDrawer = new SpriteDrawer(this, new Rectangle(0, 0, 1024, 720), false);
            menuWindow = new MainMenuWindow(this);
            programonMenu = new ProgramonMenu(spriteDrawer, this);
            Keyhandler = new KeyHandler(this);
            testBattle = new BattleScreen(this);

            XmlLoader.LoadSettings(this, spriteDrawer, CONFIGLOCATION);
            Content.RootDirectory = "Content";
            State = GameState.OPTIONS;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Map = new Map();
            menuWindow.initialize();
            programonMenu.Initialize();

            DrawPlane = GraphicsDevice.Viewport.Bounds;
            MapBounds = new Rectangle(0, 0, 100, 100);

            OptionsMenu = new OptionsMenu(this, spriteDrawer);
            Player = new Actor(new Vector2(1,1));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteDrawer.LoadContent(new SpriteBatch(GraphicsDevice), Content);
            Player.Sprite = Sprite.FromStaticColor(Color.Blue, Color.White, GraphicsDevice);

            switch (State)
            {
                case GameState.MAINMENU:
                    menuWindow.initialize();
                    break;
                case GameState.OVERWORLD:
                case GameState.NEWGAME:
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
                    spriteDrawer.Update(Map.MapDictionary, DrawPlane, Player);
                    break;
                case GameState.OPTIONS:
                    OptionsMenu.Update();
                    break;
                case GameState.OVERWORLD:
                    spriteDrawer.Update(Map.MapDictionary, DrawPlane, Player);
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
            spriteDrawer.BeginDraw();

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (State)
            {
                case GameState.MAINMENU:
                    spriteDrawer.DrawGUI(menuWindow);
                    break;
                case GameState.NEWGAME:
                    spriteDrawer.Draw(Player);
                    break;
                case GameState.OPTIONS:
                    spriteDrawer.DrawGUI(OptionsMenu);
                    break;
                case GameState.OVERWORLD:
                    spriteDrawer.Draw(Player);
                    break;
                case GameState.BATTLE:
                    spriteDrawer.DrawNodes(testBattle.GuiList);
                    break;
                case GameState.PROGRAMONSCREEN:
                    spriteDrawer.DrawGUI(new CheaterClass() { Childs = new IGuiItem[] { programonMenu } });
                    break;
            }

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            spriteDrawer.EndDraw();

            base.EndDraw();
        }
    }
}
