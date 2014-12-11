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
        private Texture2D TestTextBoxCenter { get; set; }
        public Rectangle DrawPlane { get; set; }
        public enum GameState { INTRO, MAINMENU, OPTIONS, LOADGAME, NEWGAME, OVERWORLD, PROGRAMONSCREEN, INVENTORY, BATTLE}
        public GameState State { get; set; }

        BattleScreen testBattle;

        public MainWindow()
        {
            spriteDrawer = new SpriteDrawer(this, 1024, 720, false);
            Keyhandler = new KeyHandler(this);
            Content.RootDirectory = "Content";
            State = GameState.BATTLE;
            testBattle = new BattleScreen(this);
        }

        protected override void Initialize()
        {
            BackGround = new Dictionary<Vector2, Node>();
            DrawPlane = GraphicsDevice.Viewport.Bounds;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteDrawer.LoadContent(new SpriteBatch(GraphicsDevice), Content);
            switch (State)
            {
                case GameState.OVERWORLD:
                    {
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
                    }
                case GameState.BATTLE:
                    {

                        testBattle.Load(Content);

                        break;
                    }
            }
        }

        protected override void UnloadContent()
        {
            BackGround.Clear();
        }

        protected override void Update(GameTime gameTime)
        {
            this.Keyhandler.KeyPress();
            switch(State)
            {
                case GameState.OVERWORLD:
                    {
                        spriteDrawer.Update(BackGround, DrawPlane);
                        break;
                    }
                case GameState.BATTLE:
                    {
                        spriteDrawer.Update(BackGround, DrawPlane);
                        break;
                    }
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
                case GameState.OVERWORLD:
                    {
                        spriteDrawer.Draw(DrawPlane);
                        break;
                    }
                case GameState.BATTLE:
                    {
                        spriteDrawer.DrawBattleScreen(testBattle.GuiList);
                        break;
                    }
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
