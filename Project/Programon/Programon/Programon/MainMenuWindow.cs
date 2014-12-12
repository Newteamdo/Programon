using ProgramonEngine;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Programon
{
    /// <summary>
    /// Class MainMenuWindow.
    /// </summary>
    class MainMenuWindow
    {
        private int scrWidth { get; set; }
        private int scrHeight { get; set; }

        private bool isLeaving = false;

        private MainWindow MainWindow { get; set; }
        private SpriteDrawer SpriteDrawer { get; set; }

        Rectangle PlayRect { get; set; }
        Rectangle LoadRect { get; set; }
        Rectangle OptionsRect { get; set; }
        Rectangle QuitRect { get; set; }
        Rectangle LeaveMenu { get; set; }
        Rectangle TxtLeaveRect { get; set; }
        Rectangle YesRect { get; set; }
        Rectangle NoRect { get; set; }

        TextField TxtFieldLeave { get; set; }

        Button BtnPlay { get; set; }
        Button BtnLoad { get; set; }
        Button BtnOptions {get; set;}
        Button BtnQuit { get; set; }
        Button BtnYes { get; set; }
        Button BtnNo { get; set; }

        int ButtonWidth { get; set; }
        int ButtonHeight { get; set; }
        int ButtonX { get; set; }
        int ButtonY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuWindow"/> class.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="s">The s.</param>
        public MainMenuWindow(MainWindow g, SpriteDrawer s)
        {
            MainWindow = g;
            SpriteDrawer = s;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void initialize()
        {
            scrWidth = MainWindow.Window.ClientBounds.Width;
            scrHeight = MainWindow.Window.ClientBounds.Height;

            ButtonWidth = scrWidth / 4;
            ButtonHeight = scrHeight / 10;

            ButtonX = scrWidth / 2 - ButtonWidth / 2;
            ButtonY = scrHeight / 6 - ButtonHeight / 2;

            PlayRect = new Rectangle(ButtonX, ButtonY * 2, ButtonWidth, ButtonHeight);
            LoadRect = new Rectangle(ButtonX, ButtonY * 3, ButtonWidth, ButtonHeight);
            OptionsRect = new Rectangle(ButtonX, ButtonY * 4, ButtonWidth, ButtonHeight);
            QuitRect = new Rectangle(ButtonX, ButtonY * 5, ButtonWidth, ButtonHeight);

            BtnPlay = new Button(PlayRect, MainWindow.Content.Load<Texture2D>("NewGameButton"), MainWindow);
            BtnLoad = new Button(LoadRect, MainWindow.Content.Load<Texture2D>("LoadGameButton"), MainWindow);
            BtnOptions = new Button(OptionsRect, MainWindow.Content.Load<Texture2D>("OptionsButton"), MainWindow);
            BtnQuit = new Button(QuitRect, MainWindow.Content.Load<Texture2D>("QuitButton"), MainWindow);

            BtnPlay.OnMouseClick += btnPlay_OnMouseClick;

            BtnPlay.OnMouseClick += play_OnMouseClick;
            BtnPlay.OnMouseEnter += play_OnMouseEnter;
            BtnPlay.OnMouseLeave += play_OnMouseLeave;

            BtnLoad.OnMouseEnter += play_OnMouseEnter;
            BtnLoad.OnMouseLeave += play_OnMouseLeave;

            BtnOptions.OnMouseClick += btnOptions_OnMouseClick;
            BtnOptions.OnMouseEnter += play_OnMouseEnter;
            BtnOptions.OnMouseLeave += play_OnMouseLeave;

            BtnQuit.OnMouseEnter += play_OnMouseEnter;
            BtnQuit.OnMouseLeave += play_OnMouseLeave;
            BtnQuit.OnMouseClick += btnQuit_OnMouseClick;
        }

        /// <summary>
        /// BTNs the play_ on mouse click.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void btnPlay_OnMouseClick(Button btn)
        {
            this.MainWindow.SetState(MainWindow.GameState.NEWGAME);
        }

        /// <summary>
        /// BTNs the options_ on mouse click.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void btnOptions_OnMouseClick(Button btn)
        {
            this.MainWindow.SetState(MainWindow.GameState.OPTIONS);
        }

        /// <summary>
        /// BTNs the quit_ on mouse click.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void btnQuit_OnMouseClick(Button btn)
        {
            isLeaving = true;
            int width = (scrWidth / 10) * 6;
            width = width / 2;
            LeaveMenu = new Rectangle((scrWidth / 2) - width, ButtonY * 4, (scrWidth / 10) * 6, (scrHeight / 6) * 2);
            YesRect = new Rectangle((LeaveMenu.X + LeaveMenu.Width) - ((LeaveMenu.Width / 100) * 40) - ((LeaveMenu.Width / 100) * 5), (LeaveMenu.Y + LeaveMenu.Height) - ((LeaveMenu.Height / 100) * 40) - ((LeaveMenu.Height / 100) * 10), (LeaveMenu.Width / 100) * 40, (LeaveMenu.Height / 100) * 40);
            NoRect = new Rectangle((LeaveMenu.X) + ((LeaveMenu.Width / 100) * 5), (LeaveMenu.Y + LeaveMenu.Height) - ((LeaveMenu.Height / 100) * 40) - ((LeaveMenu.Height / 100) * 10), (LeaveMenu.Width / 100) * 40, (LeaveMenu.Height / 100) * 40);
            TxtLeaveRect = new Rectangle(LeaveMenu.X + ((LeaveMenu.X / 100) * 10), LeaveMenu.Y + ((LeaveMenu.Y / 100) * 10), (LeaveMenu.Width / 100) * 80, 0);
            TxtFieldLeave = new TextField("Are you sure you want to quit?", Color.Black, Color.Transparent, Color.Transparent, TxtLeaveRect, MainWindow.Content.Load<SpriteFont>("PokemonFontSize50"));
            BtnYes = new Button(YesRect, MainWindow.Content.Load<Texture2D>("Yes"), MainWindow);
            BtnNo = new Button(NoRect, MainWindow.Content.Load<Texture2D>("No"), MainWindow);

            BtnPlay.isEnabled = false;
            BtnLoad.isEnabled = false;
            BtnOptions.isEnabled = false;
            BtnQuit.isEnabled = false;

            BtnYes.OnMouseEnter += play_OnMouseEnter;
            BtnYes.OnMouseLeave += play_OnMouseLeave;
            BtnYes.OnMouseClick += btnYes_OnMouseClick;
            BtnNo.OnMouseEnter += play_OnMouseEnter;
            BtnNo.OnMouseLeave += play_OnMouseLeave;
            BtnNo.OnMouseClick += btnNo_OnMouseClick;
        }

        /// <summary>
        /// BTNs the yes_ on mouse click.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void btnYes_OnMouseClick(Button btn)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// BTNs the no_ on mouse click.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void btnNo_OnMouseClick(Button btn)
        {
            isLeaving = false;
            BtnPlay.isEnabled = true;
            BtnLoad.isEnabled = true;
            BtnOptions.isEnabled = true;
            BtnQuit.isEnabled = true;
        }

        void btnLoad_OnMouseEnter(Button btn)
        {

        }

        /// <summary>
        /// Play_s the on mouse leave.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void play_OnMouseLeave(Button btn)
        {
            ShowHoverIndicator(btn, 10, false);
        }

        /// <summary>
        /// Play_s the on mouse enter.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void play_OnMouseEnter(Button btn)
        {
            ShowHoverIndicator(btn, 10, true);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            BtnPlay.Update();
            BtnLoad.Update();
            BtnOptions.Update();
            BtnQuit.Update();

            if (BtnYes != null)
            {
                BtnNo.Update();
                BtnYes.Update();
            }

        }

        /// <summary>
        /// Play_s the on mouse click.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        void play_OnMouseClick(Button btn)
        {

        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            BtnPlay.Draw(SpriteDrawer.SpriteBatch);
            BtnLoad.Draw(SpriteDrawer.SpriteBatch);
            BtnOptions.Draw(SpriteDrawer.SpriteBatch);
            BtnQuit.Draw(SpriteDrawer.SpriteBatch);


            if (isLeaving)
            {
                SpriteDrawer.SpriteBatch.Draw(MainWindow.Content.Load<Texture2D>("LeaveMenu"), LeaveMenu, Color.White);
                if (BtnYes != null)
                {
                    TxtFieldLeave.Draw(SpriteDrawer.SpriteBatch);
                    BtnYes.Draw(SpriteDrawer.SpriteBatch);
                    BtnNo.Draw(SpriteDrawer.SpriteBatch);
                }
            }

        }

        /// <summary>
        /// Shows the hover indicator.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        /// <param name="size">The size.</param>
        /// <param name="ScaleUp">if set to <c>true</c> [scale up].</param>
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