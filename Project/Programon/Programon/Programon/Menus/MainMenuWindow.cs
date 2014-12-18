using ProgramonEngine;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Programon
{
    public class MainMenuWindow : IMenu
    {
        public IGuiItem[] Childs
        {
            get
            {
                if (isLeaving)
                {
                    return new IGuiItem[]
                    {
                        LeaveMenu,
                        txtFieldLeave,
                        btnYes,
                        btnNo
                    };
                }

                return new IGuiItem[]
                {
                    btnPlay,
                    btnLoad,
                    btnOptions,
                    btnQuit
                };
            }
            set { }
        }

        private int scrWidth;
        private int scrHeight;
        private bool isLeaving = false;

        private MainWindow mainWindow;

        private Rectangle playRect;
        private Rectangle loadRect;
        private Rectangle optionsRect;
        private Rectangle quitRect;
        private Rectangle txtLeaveRect;
        private Rectangle yesRect;
        private Rectangle noRect;

        private TextField txtFieldLeave;

        private Button btnPlay;
        private Button btnLoad;
        private Button btnOptions;
        private Button btnQuit;
        private Button btnYes;
        private Button btnNo;

        private Background LeaveMenu;

        private int buttonWidth;
        private int buttonHeight;
        private int buttonX;
        private int buttonY;

        public MainMenuWindow(MainWindow g)
        {
            mainWindow = g;
        }

        public void initialize()
        {
            scrWidth = mainWindow.Window.ClientBounds.Width;
            scrHeight = mainWindow.Window.ClientBounds.Height;

            buttonWidth = scrWidth / 4;
            buttonHeight = scrHeight / 10;

            buttonX = scrWidth / 2 - buttonWidth / 2;
            buttonY = scrHeight / 6 - buttonHeight / 2;

            LeaveMenu = new Background(mainWindow.Content.Load<Texture2D>("LeaveMenu"), new Rectangle(0,0,100,100));

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

        private void btnPlay_OnMouseClick(Button btn)
        {
            this.mainWindow.SetState(GameState.NEWGAME);
        }

        private void btnOptions_OnMouseClick(Button btn)
        {
            this.mainWindow.SetState(GameState.OPTIONS);
        }

        private void btnQuit_OnMouseClick(Button btn)
        {
            isLeaving = true;
            int width = (scrWidth / 10) * 6;
            width = width / 2;

            LeaveMenu.Rectangle = new Rectangle((scrWidth / 2) - width, buttonY * 4, (scrWidth / 10) * 6, (scrHeight / 6) * 2);
            yesRect = new Rectangle((LeaveMenu.Rectangle.X + LeaveMenu.Rectangle.Width) - ((LeaveMenu.Rectangle.Width / 100) * 40) - ((LeaveMenu.Rectangle.Width / 100) * 5), (LeaveMenu.Rectangle.Y + LeaveMenu.Rectangle.Height) - ((LeaveMenu.Rectangle.Height / 100) * 40) - ((LeaveMenu.Rectangle.Height / 100) * 10), (LeaveMenu.Rectangle.Width / 100) * 40, (LeaveMenu.Rectangle.Height / 100) * 40);
            noRect = new Rectangle((LeaveMenu.Rectangle.X) + ((LeaveMenu.Rectangle.Width / 100) * 5), (LeaveMenu.Rectangle.Y + LeaveMenu.Rectangle.Height) - ((LeaveMenu.Rectangle.Height / 100) * 40) - ((LeaveMenu.Rectangle.Height / 100) * 10), (LeaveMenu.Rectangle.Width / 100) * 40, (LeaveMenu.Rectangle.Height / 100) * 40);
            txtLeaveRect = new Rectangle(LeaveMenu.Rectangle.X + ((LeaveMenu.Rectangle.X / 100) * 10), LeaveMenu.Rectangle.Y + ((LeaveMenu.Rectangle.Y / 100) * 10), (LeaveMenu.Rectangle.Width / 100) * 80, 0);
            txtFieldLeave = new TextField("Are you sure you want to quit?", Color.Black, Color.Transparent, Color.Transparent, txtLeaveRect, mainWindow.Content.Load<SpriteFont>("DebugFont"));
            btnYes = new Button(yesRect, mainWindow.Content.Load<Texture2D>("Yes"), mainWindow);
            btnNo = new Button(noRect, mainWindow.Content.Load<Texture2D>("No"), mainWindow);

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

        private void btnYes_OnMouseClick(Button btn)
        {
            Environment.Exit(0);
        }

        private void btnNo_OnMouseClick(Button btn)
        {
            isLeaving = false;
            btnPlay.isEnabled = true;
            btnLoad.isEnabled = true;
            btnOptions.isEnabled = true;
            btnQuit.isEnabled = true;
        }

        private void btnLoad_OnMouseEnter(Button btn)
        {

        }

        private void play_OnMouseLeave(Button btn)
        {
            ShowHoverIndicator(btn, 10, false);
        }

        private void play_OnMouseEnter(Button btn)
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

        private void play_OnMouseClick(Button btn)
        {

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