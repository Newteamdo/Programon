using ProgramonEngine;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Programon
{
    class MainMenuWindow
    {
        private int scrWidth;
        private int scrHeight;
        private bool isLeaving = false;

        private MainWindow mainWindow;
        private SpriteDrawer spriteDrawer;

        Rectangle playRect;
        Rectangle loadRect;
        Rectangle optionsRect;
        Rectangle quitRect;
        Rectangle leaveMenu;
        Rectangle txtLeaveRect;
        Rectangle yesRect;
        Rectangle noRect;

        TextField txtFieldLeave;

        Button btnPlay;
        Button btnLoad;
        Button btnOptions;
        Button btnQuit;
        Button btnYes;
        Button btnNo;

        int buttonWidth;
        int buttonHeight;
        int buttonX;
        int buttonY;

        public MainMenuWindow(MainWindow g, SpriteDrawer s)
        {
            mainWindow = g;
            spriteDrawer = s;
        }

        public void initialize()
        {
            scrWidth = mainWindow.Window.ClientBounds.Width;
            scrHeight = mainWindow.Window.ClientBounds.Height;

            buttonWidth = scrWidth / 4;
            buttonHeight = scrHeight / 10;

            buttonX = scrWidth / 2 - buttonWidth / 2;
            buttonY = scrHeight / 6 - buttonHeight / 2;

            playRect = new Rectangle(buttonX, buttonY * 2, buttonWidth, buttonHeight);
            loadRect = new Rectangle(buttonX, buttonY * 3, buttonWidth, buttonHeight);
            optionsRect = new Rectangle(buttonX, buttonY * 4, buttonWidth, buttonHeight);
            quitRect = new Rectangle(buttonX, buttonY * 5, buttonWidth, buttonHeight);

            btnPlay = new Button(playRect, mainWindow.Content.Load<Texture2D>("NewGameButton"), mainWindow);
            btnLoad = new Button(loadRect, mainWindow.Content.Load<Texture2D>("LoadGameButton"), mainWindow);
            btnOptions = new Button(optionsRect, mainWindow.Content.Load<Texture2D>("OptionsButton"), mainWindow);
            btnQuit = new Button(quitRect, mainWindow.Content.Load<Texture2D>("QuitButton"), mainWindow);

            btnPlay.OnMouseClick += btnPlay_OnMouseClick;

            btnPlay.OnMouseClick += play_OnMouseClick;
            btnPlay.OnMouseEnter += play_OnMouseEnter;
            btnPlay.OnMouseLeave += play_OnMouseLeave;

            btnLoad.OnMouseEnter += play_OnMouseEnter;
            btnLoad.OnMouseLeave += play_OnMouseLeave;

            btnOptions.OnMouseClick += btnOptions_OnMouseClick;
            btnOptions.OnMouseEnter += play_OnMouseEnter;
            btnOptions.OnMouseLeave += play_OnMouseLeave;

            btnQuit.OnMouseEnter += play_OnMouseEnter;
            btnQuit.OnMouseLeave += play_OnMouseLeave;
            btnQuit.OnMouseClick += btnQuit_OnMouseClick;
        }

        void btnPlay_OnMouseClick(Game game, Button btn)
        {
            this.mainWindow.GameState = MainWindow.State.NEWGAME;
        }

        void btnOptions_OnMouseClick(Game game, Button btn)
        {
            this.mainWindow.GameState = MainWindow.State.OPTIONS;
        }

        void btnQuit_OnMouseClick(Game game, Button btn)
        {
            isLeaving = true;
            int width = (scrWidth / 10) * 6;
            width = width / 2;
            leaveMenu = new Rectangle((scrWidth / 2) - width, buttonY * 4, (scrWidth / 10) * 6, (scrHeight / 6) * 2);
            yesRect = new Rectangle((leaveMenu.X + leaveMenu.Width) - ((leaveMenu.Width / 100) * 40) - ((leaveMenu.Width / 100) * 5), (leaveMenu.Y + leaveMenu.Height) - ((leaveMenu.Height / 100) * 40) - ((leaveMenu.Height / 100) * 10), (leaveMenu.Width / 100) * 40, (leaveMenu.Height / 100) * 40);
            noRect = new Rectangle((leaveMenu.X) + ((leaveMenu.Width / 100) * 5), (leaveMenu.Y + leaveMenu.Height) - ((leaveMenu.Height / 100) * 40) - ((leaveMenu.Height / 100) * 10), (leaveMenu.Width / 100) * 40, (leaveMenu.Height / 100) * 40);
            txtLeaveRect = new Rectangle(leaveMenu.X + ((leaveMenu.X / 100) * 10), leaveMenu.Y + ((leaveMenu.Y / 100) * 10), (leaveMenu.Width / 100) * 80, 0);
            txtFieldLeave = new TextField("Are you sure you want to quit?", Color.Black, Color.Transparent, Color.Transparent,txtLeaveRect, game.Content.Load<SpriteFont>("PokemonFontSize50"));
            btnYes = new Button(yesRect, game.Content.Load<Texture2D>("Yes"), game);
            btnNo = new Button(noRect, game.Content.Load<Texture2D>("No"), game);

            btnPlay.isEnabled = false;
            btnLoad.isEnabled = false;
            btnOptions.isEnabled = false;
            btnQuit.isEnabled = false;

            btnYes.OnMouseEnter += play_OnMouseEnter;
            btnYes.OnMouseLeave += play_OnMouseLeave;
            btnYes.OnMouseClick += btnYes_OnMouseClick;
            btnNo.OnMouseEnter += play_OnMouseEnter;
            btnNo.OnMouseLeave += play_OnMouseLeave;
            btnNo.OnMouseClick += btnNo_OnMouseClick;
        }

        void btnYes_OnMouseClick(Game game, Button btn)
        {
            Environment.Exit(0);
        }

        void btnNo_OnMouseClick(Game game, Button btn)
        {
            isLeaving = false;
            btnPlay.isEnabled = true;
            btnLoad.isEnabled = true;
            btnOptions.isEnabled = true;
            btnQuit.isEnabled = true;
        }

        void btnLoad_OnMouseEnter(Game game, Button btn)
        {

        }

        void play_OnMouseLeave(Game game, Button btn)
        {
            ShowHoverIndicator(btn, 10, false);
        }

        void play_OnMouseEnter(Game game, Button btn)
        {
            ShowHoverIndicator(btn, 10, true);
        }

        public void Update()
        {
            btnPlay.Update();
            btnLoad.Update();
            btnOptions.Update();
            btnQuit.Update();

            if (btnYes != null)
            {
                btnNo.Update();
                btnYes.Update();
            }

        }

        void play_OnMouseClick(Game game, Button btn)
        {

        }

        public void Draw()
        {
            btnPlay.Draw(spriteDrawer.SpriteBatch);
            btnLoad.Draw(spriteDrawer.SpriteBatch);
            btnOptions.Draw(spriteDrawer.SpriteBatch);
            btnQuit.Draw(spriteDrawer.SpriteBatch);


            if (isLeaving)
            {
                spriteDrawer.SpriteBatch.Draw(mainWindow.Content.Load<Texture2D>("LeaveMenu"), leaveMenu, Color.White);
                if (btnYes != null)
                {
                    txtFieldLeave.Draw(spriteDrawer.SpriteBatch);
                    btnYes.Draw(spriteDrawer.SpriteBatch);
                    btnNo.Draw(spriteDrawer.SpriteBatch);
                }
            }

        }

        public void ShowHoverIndicator(Button btn, int size, bool ScaleUp)
        {
            if (ScaleUp)
            {
                int xPos = btn.GetRectangle().X - size / 2;
                int yPos = btn.GetRectangle().Y - size / 2;

                int width = btn.GetRectangle().Width + size;
                int height = btn.GetRectangle().Height + size;

                btn.SetPosition(xPos, yPos);
                btn.SetSize(width, height);
            }
            else
            {
                int xPos = btn.GetRectangle().X + size / 2;
                int yPos = btn.GetRectangle().Y + size / 2;

                int width = btn.GetRectangle().Width - size;
                int height = btn.GetRectangle().Height - size;

                btn.SetPosition(xPos, yPos);
                btn.SetSize(width, height);
            }
        }
    }
}