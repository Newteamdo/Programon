using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ProgramonEngine;
using System.Collections.Generic;

namespace Programon
{
    /// <summary>
    /// A class to display a options menu.
    /// </summary>
    public class OptionsMenu : IMenu
    {
        public IGuiItem[] Childs
        {
            get
            {
                return new IGuiItem[]
                {
                    Background,
                    BackBtn,
                    SaveChangesBtn, 
                    PrevButtonResolution,
                    NextButtonResolution,
                    ResolutionTextField,
                    MasterVolumeTextField,
                    ResolutionText,
                    MasterVolumeText,
                    OptionsText,
                    Slider,
                };
            }

            set { }
        }

        public Texture2D BackgroundTexture { get; set; }

        private string[] SupportedResolutions { get; set; }

        private MainWindow GameWindow { get; set; }
        private SpriteDrawer SpriteDrawer { get; set; }

        private Button BackBtn { get; set; }
        private Button SaveChangesBtn { get; set; }

        private Button PrevButtonResolution { get; set; }
        private Button NextButtonResolution { get; set; }

        private TextField ResolutionTextField { get; set; }
        private TextField MasterVolumeTextField { get; set; }

        private TextField ResolutionText { get; set; }
        private TextField MasterVolumeText { get; set; }

        private TextField OptionsText { get; set; }

        private Slider Slider { get; set; }

        private int CurrentIndexResolution { get; set; }

        private SpriteFont TextFont { get; set; }

        private int ScreenWidth { get; set; }
        private int ScreenHeight { get; set; }

        private Background Background { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsMenu"/> class.
        /// </summary>
        public OptionsMenu()
        {
            this.GameWindow = null;
            this.SpriteDrawer = null;
            this.TextFont = null;

            this.ScreenWidth = 0;
            this.ScreenHeight = 0;
            this.CurrentIndexResolution = 0;

            BackBtn = null;

            SaveChangesBtn = null;

            ResolutionText = null;
            MasterVolumeText = null;
            OptionsText = null;

            ResolutionTextField = null;
            MasterVolumeTextField = null;

            PrevButtonResolution = null;

            NextButtonResolution = null;

            BackgroundTexture = null;
            Slider = null;
            Background = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsMenu"/> class.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        /// <param name="sDrawer">The SpriteDrawer.</param>
        public OptionsMenu(MainWindow mainWindow, SpriteDrawer sDrawer)
        {
            this.GameWindow = mainWindow;
            this.SpriteDrawer = sDrawer;
            this.TextFont = mainWindow.Content.Load<SpriteFont>("Fonts/DebugFont");

            this.ScreenWidth = sDrawer.BufferSize.Width;
            this.ScreenHeight = sDrawer.BufferSize.Height;
            this.CurrentIndexResolution = 3;

            GetSupportedResolutions();

            BackBtn = new Button(new Rectangle(ScreenWidth - (ScreenWidth / 8 + 20), ScreenHeight - (ScreenHeight / 10 + 50), ScreenWidth / 8, ScreenHeight / 10), GameWindow.Content.Load<Texture2D>(@"Menus\OptionsMenu\BackButton"), mainWindow);

            BackBtn.OnMouseClick += backBtn_OnClick;

            SaveChangesBtn = new Button(new Rectangle(20, ScreenHeight - (ScreenHeight / 10 + 50), (int)((float)ScreenWidth / 2.5), ScreenHeight / 10), mainWindow.Content.Load<Texture2D>(@"Menus\OptionsMenu\SaveChangesButton"), mainWindow);
            SaveChangesBtn.OnMouseClick += saveChangesBtn_OnClick;

            ResolutionText = new TextField("Resolution:", Color.Black, Color.Transparent, Color.Transparent, new Rectangle(300, 300, 10, 20), TextFont);
            MasterVolumeText = new TextField("Master Volume:", Color.Black, Color.Transparent, Color.Transparent, new Rectangle(330, 400, 0, 20), TextFont);
            OptionsText = new TextField("Options", Color.Black, Color.Transparent, Color.Transparent, new Rectangle(520, 150, 0, 0), TextFont);

            ResolutionTextField = new TextField(SupportedResolutions[CurrentIndexResolution], Color.Red, Color.White, Color.Black, new Rectangle(500, 300, 10, 20), TextFont);
            MasterVolumeTextField = new TextField("100", Color.Red, Color.White, Color.Black, new Rectangle(500, 400, 300, 20), TextFont);

            PrevButtonResolution = new Button(new Rectangle(475, 300, 20, 20), mainWindow.Content.Load<Texture2D>(@"Menus\OptionsMenu\prevButton"), mainWindow);
            PrevButtonResolution.OnMouseClick += prevButtonResolution_OnMouseClick;

            NextButtonResolution = new Button(new Rectangle(800, 300, 20, 20), mainWindow.Content.Load<Texture2D>(@"Menus\OptionsMenu\nextButton"), mainWindow);
            NextButtonResolution.OnMouseClick += nextButtonResolution_OnMouseClick;

            BackgroundTexture = mainWindow.Content.Load<Texture2D>(@"Menus\OptionsMenu\Background");
            Slider = new Slider(new Rectangle((int)(ScreenWidth / 1.8), ScreenHeight / 2 + 2, ScreenWidth / 4, ScreenHeight / 35), 20, 0, 100, mainWindow.VolumeLevel, Color.Red, TextFont);
            Slider.OnMouseHold += slider_OnMouseHold;

            Background = new ProgramonEngine.Background(BackgroundTexture, ScreenWidth, ScreenHeight);

            SaveChangesBtn.OnMouseEnter += button_OnMouseEnter;
            SaveChangesBtn.OnMouseLeave += button_OnMouseLeave;

            BackBtn.OnMouseEnter += button_OnMouseEnter;
            BackBtn.OnMouseLeave += button_OnMouseLeave;
            BackBtn.OnMouseClick += BackBtn_OnMouseClick;

            CalculatePositions();
        }

        void BackBtn_OnMouseClick(Button btn)
        {
            GameWindow.SetState(GameState.MAINMENU);
        }


        /// <summary>
        /// Shows or hides the hover.
        /// </summary>
        /// <param name="btn">The button.</param>
        /// <param name="size">The size of the hover.</param>
        /// <param name="ScaleUp">If set to <c>true</c> it shows the hover.</param>
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

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            BackBtn.Update();
            //ControlsBtn.Update();
            SaveChangesBtn.Update();
            PrevButtonResolution.Update();
            NextButtonResolution.Update();
            Slider.Update();
        }

        /// <summary>
        /// Calculates the positions of all objects in the options menu.
        /// </summary>
        private void CalculatePositions()
        {
            Background.SetWidth(ScreenWidth);
            Background.SetHeight(ScreenHeight);
           
            BackBtn.SetPosition(ScreenWidth - (ScreenWidth / 8 + 20), ScreenHeight - (ScreenHeight / 10 + 20));
            BackBtn.SetSize(ScreenWidth / 8, ScreenHeight / 10);

            SaveChangesBtn.SetPosition(20, ScreenHeight - (ScreenHeight / 10 + 20));
            SaveChangesBtn.SetSize((int)((float)ScreenWidth / 2.5), ScreenHeight / 10);

            ResolutionText.SetPosition(ScreenWidth / 3, (int)(ScreenHeight / 2.5));
            ResolutionText.SetSize(0, ScreenHeight / 25);

            ResolutionTextField.SetPosition((int)(ScreenWidth / 1.8), (int)(ScreenHeight / 2.5) + 2);
            ResolutionTextField.SetSize(ScreenWidth / 4, ScreenHeight / 35);

            MasterVolumeText.SetPosition((int)(ScreenWidth / 2.8), ScreenHeight / 2);
            MasterVolumeText.SetSize(0, ScreenHeight / 25);

            MasterVolumeTextField.SetPosition((int)(ScreenWidth / 1.8), ScreenHeight / 2 + 2);
            MasterVolumeTextField.SetSize(ScreenWidth / 4, ScreenHeight / 35);

            OptionsText.SetPosition(ScreenWidth / 2, 30);
            OptionsText.SetSize(0, ScreenHeight / 8);

            PrevButtonResolution.SetSize(ScreenWidth / 60, ScreenHeight / 33);
            PrevButtonResolution.SetPosition((int)(ScreenWidth / 1.8) - PrevButtonResolution.GetRectangle().Width, (int)(ScreenHeight / 2.5) + 2);

            NextButtonResolution.SetSize(ScreenWidth / 60, ScreenHeight / 33);
            NextButtonResolution.SetPosition((int)(ScreenWidth / 1.8) + ResolutionTextField.GetRectangle().Width, (int)(ScreenHeight / 2.5) + 2);

            Slider.SetPosition((int)(ScreenWidth / 1.8), ScreenHeight / 2 + 2);
            Slider.SetSize(ScreenWidth / 4, ScreenHeight / 35);
        }

        private void GetSupportedResolutions()
        {
            List<DisplayMode> supportedDisplayModes = new List<DisplayMode>();
            foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (dm.Width >= 800 && dm.Height >= 600)
                {
                    if (dm.Width <= 1920 && dm.Height <= 1080)
                    {
                        supportedDisplayModes.Add(dm);
                    }
                }
            }

            SupportedResolutions = new string[supportedDisplayModes.Count];
            for (int i = 0; i < supportedDisplayModes.Count; i++)
            {
                SupportedResolutions[i] = supportedDisplayModes[i].Width + "x" + supportedDisplayModes[i].Height;
                if (supportedDisplayModes[i].Width == ScreenWidth && supportedDisplayModes[i].Height == ScreenHeight)
                {
                    CurrentIndexResolution = i;
                }
            }
        }

        /// <summary>
        /// The event handle method for the mouse click event of nextButtonResolution.
        /// </summary>
        /// <param name="btn">The Button.</param>
        private void nextButtonResolution_OnMouseClick(Button btn)
        {
            if ((CurrentIndexResolution + 1) > (SupportedResolutions.Length - 1))
            {
                CurrentIndexResolution = SupportedResolutions.Length - 1;
            }
            else
            {
                CurrentIndexResolution++;
            }

            ResolutionTextField.SetText(SupportedResolutions[CurrentIndexResolution]);
        }

        /// <summary>
        /// The event handle method for the mouse click event of prevButtonResolution.
        /// </summary>
        /// <param name="btn">The Button.</param>
        private void prevButtonResolution_OnMouseClick(Button btn)
        {
            if ((CurrentIndexResolution - 1) < 0)
            {
                CurrentIndexResolution = 0;
            }
            else
            {
                CurrentIndexResolution--;
            }

            ResolutionTextField.SetText(SupportedResolutions[CurrentIndexResolution]);
        }

        /// <summary>
        /// The event handle method for the mouse click event of saveChangesBtn.
        /// </summary>
        /// <param name="btn">The Button.</param>
        private void saveChangesBtn_OnClick(Button btn)
        {
            string[] resolution = SupportedResolutions[CurrentIndexResolution].Split('x');
            int width = Convert.ToInt16(resolution[0]);
            int height = Convert.ToInt16(resolution[1]);

            this.ScreenWidth = width;
            this.ScreenHeight = height;

            GameWindow.VolumeLevel = Slider.GetSliderValue();
            SpriteDrawer.SetWindowSize(width, height);
            XmlLoader.SaveSettings(GameWindow, SpriteDrawer,MainWindow.CONFIGLOCATION);

            CalculatePositions();

            GameWindow.ReInit();
        }

        /// <summary>
        /// The event handle method for the mouse click event of backBtn.
        /// </summary>
        /// <param name="btn">The Button.</param>
        private void backBtn_OnClick(Button btn)
        {
            GameWindow.SetState(GameState.MAINMENU);
        }

        /// <summary>
        /// The event handle method for the OnMouseHold event of slider.
        /// </summary>
        /// <param name="slider">The slider.</param>
        /// <param name="mouseState">State of the mouse.</param>
        private void slider_OnMouseHold(Slider slider, MouseState mouseState)
        {
            slider.SetSliderValue(mouseState.X);
        }

        /// <summary>
        /// The event handle method for the OnMouseEnter event of button.
        /// </summary>
        /// <param name="btn">The button.</param>
        private void button_OnMouseEnter(Button btn)
        {
            ShowHoverIndicator(btn, 10, true);
        }

        /// <summary>
        /// The event handle method for the OnMouseLeave event of button.
        /// </summary>
        /// <param name="btn">The button.</param>
        private void button_OnMouseLeave(Button btn)
        {
            ShowHoverIndicator(btn, 10, false);
        }
    }
}
