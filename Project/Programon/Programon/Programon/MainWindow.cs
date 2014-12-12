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
        private const string CONFIGLOCATION = "config.xml";

        private SpriteDrawer spriteDrawer { get; set; }
        private KeyHandler Keyhandler { get; set; }

        private Texture2D TestTexture { get; set; }
        private Texture2D TestTextBoxCenter { get; set; }

        public Dictionary<Vector2, Node> BackGround { get; set; }
        public Actor Player { get; set; }

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

            LoadSettingsFromXml();
            Content.RootDirectory = "Content";
            State = GameState.OPTIONS;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            BackGround = new Dictionary<Vector2, Node>();
            menuWindow.initialize();
            programonMenu.Initialize();

            DrawPlane = GraphicsDevice.Viewport.Bounds;
            MapBounds = new Rectangle(0, 0, 100, 100);

            OptionsMenu = new OptionsMenu(this, spriteDrawer);
            Player = new Actor(Vector2.Zero);

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
                    TestTexture = Content.Load<Texture2D>("TestNodeTextures/grass");
                    for (int y = 0; y < 100; y++)
                    {
                        for (int x = 0; x < 100; x++)
                        {
                            Vector2 curPos = new Vector2(x, y);
                            BackGround.Add(curPos, new Node(curPos, TestTexture));
                        }
                    }
                    break;
                case GameState.BATTLE:
                    testBattle.Load(Content);
                    break;
            }
        }

        protected override void UnloadContent()
        {
            BackGround.Clear();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.K))
                SetState(GameState.PROGRAMONSCREEN);

            this.Keyhandler.KeyPress();
            switch (State)
            {
                case GameState.MAINMENU:
                    menuWindow.Update();
                    break;
                case GameState.NEWGAME:
                    spriteDrawer.Update(BackGround, DrawPlane, Player);
                    break;
                case GameState.OPTIONS:
                    OptionsMenu.Update();
                    break;
                case GameState.OVERWORLD:
                    spriteDrawer.Update(BackGround, DrawPlane, Player);
                    break;
                case GameState.BATTLE:
                    spriteDrawer.Update(BackGround, DrawPlane, Player);
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

        /// <summary>
        /// Loads the settings from XML.
        /// </summary>
        private void LoadSettingsFromXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(CONFIGLOCATION);

            string width;
            width = xDoc.SelectSingleNode("Config/Resolution/width").InnerText;

            string height = xDoc.SelectSingleNode("Config/Resolution/height").InnerText;

            int screenWidth = 800;
            int screenHeight = 600;

            if (int.TryParse(width, out screenWidth) && int.TryParse(height, out screenHeight))
            {
                spriteDrawer.SetWindowSize(screenWidth, screenHeight);
            }
            else
            {
                spriteDrawer.SetWindowSize(screenWidth, screenHeight);
            }

            string volumeText = xDoc.SelectSingleNode("Config/Mastervolume").InnerText;

            double volumeValue;
            if (double.TryParse(volumeText, out volumeValue))
            {
                this.VolumeLevel = volumeValue;
            }
            else
            {
                this.VolumeLevel = 100;
            }
        }

        /// <summary>
        /// Saves the changes to XML.
        /// </summary>
        public void SaveChangesToXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(CONFIGLOCATION);

            xDoc.SelectSingleNode("Config/Resolution/width").InnerText = spriteDrawer.GetWindowWidth().ToString();
            xDoc.SelectSingleNode("Config/Resolution/height").InnerText = spriteDrawer.GetWindowHeight().ToString();

            xDoc.SelectSingleNode("Config/Mastervolume").InnerText = VolumeLevel.ToString();
            xDoc.Save(CONFIGLOCATION);
        }
    }
}
