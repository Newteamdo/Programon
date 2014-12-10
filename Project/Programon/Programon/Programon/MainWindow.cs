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

namespace Programon
{
    class MainWindow : Game
    {
        private SpriteDrawer spriteDrawer { get; set; }
        private KeyHandler Keyhandler { get; set; }

        private Dictionary<Vector2, Node> BackGround { get; set; }
        private Texture2D TestTexture { get; set; }
        public Rectangle DrawPlane { get; set; }

        private MainMenuWindow MainMenu { get; set; }
        private OptionsMenu OptionsMenu { get; set; }

        public State GameState { get; set; }

        public enum State
        {
            INTRO,
            MAINMENU,
            NEWGAME,
            GAME,
            LOADGAME,
            OPTIONS
        }

        public MainWindow()
        {
            spriteDrawer = new SpriteDrawer(this, 1024, 720, false);
            Keyhandler = new KeyHandler(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            MainMenu = new MainMenuWindow(this, spriteDrawer);
            BackGround = new Dictionary<Vector2, Node>();
            DrawPlane = GraphicsDevice.Viewport.Bounds;
            MainMenu.initialize();
            OptionsMenu = new Programon.OptionsMenu(this, spriteDrawer);
            GameState = State.INTRO;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteDrawer.LoadContent(new SpriteBatch(GraphicsDevice), Content);
            TestTexture = Content.Load<Texture2D>("TestNodeTextures/grass");

            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    Vector2 curPos = new Vector2(x, y);
                    BackGround.Add(curPos, new Node(curPos, TestTexture));
                }
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                GameState = State.MAINMENU;
            }
            this.Keyhandler.KeyPress();
            switch (GameState)
            {
                case State.INTRO:
                    break;
                case State.MAINMENU:
                    MainMenu.Update();
                    break;
                case State.NEWGAME:
                    spriteDrawer.Update(BackGround, DrawPlane);
                    break;
                case State.GAME:
                    break;
                case State.LOADGAME:
                    break;
                case State.OPTIONS:
                    OptionsMenu.Update();
                    break;
                default:
                    break;
            }
            this.Keyhandler.KeyPress();
            spriteDrawer.Update(BackGround, DrawPlane);
            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            spriteDrawer.BeginDraw();

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (GameState)
            {
                case State.MAINMENU:
                    MainMenu.Draw();
                    break;
                case State.NEWGAME:
                    spriteDrawer.Draw(DrawPlane);
                    break;
                case State.GAME:
                    break;
                case State.LOADGAME:
                    break;
                case State.OPTIONS:
                    OptionsMenu.Draw(spriteDrawer.SpriteBatch);
                    break;
                default:
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
