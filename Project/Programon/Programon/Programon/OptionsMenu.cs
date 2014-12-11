using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ProgramonEngine;

namespace Programon
{
    /// <summary>
    /// A class to display a options menu.
    /// </summary>
    class OptionsMenu
    {
        private string[] resolutions = { "640x360", "800x600", "1024x768", "1280x720" };

        private MainWindow GameWindow { get; set; }
        private SpriteDrawer drawer { get; set; }

        private Button backBtn { get; set; }
        private Button controlsBtn { get; set; }
        private Button saveChangesBtn { get; set; }

        private Button prevButtonResolution { get; set; }
        private Button nextButtonResolution { get; set; }

        private TextField resolutionTextField { get; set; }
        private TextField masterVolumeTextField { get; set; }

        private TextField resolutionText { get; set; }
        private TextField masterVolumeText { get; set; }

        private TextField optionsText { get; set; }

        private Slider slider { get; set; }

        private Texture2D backgroundTexture { get; set; }

        private int currentIndex { get; set; }

        private SpriteFont textFont { get; set; }

        private int screenWidth { get; set; }
        private int screenHeight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsMenu"/> class.
        /// </summary>
        public OptionsMenu()
        {
            this.GameWindow = null;
            this.drawer = null;
            this.textFont = null;

            this.screenWidth = 0;
            this.screenHeight = 0;
            this.currentIndex = 0;

            backBtn = null;
            controlsBtn = null;

            saveChangesBtn = null;

            resolutionText = null;
            masterVolumeText = null;
            optionsText = null;

            resolutionTextField = null;
            masterVolumeTextField = null;

            prevButtonResolution = null;

            nextButtonResolution = null;

            backgroundTexture = null;
            slider = null;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsMenu"/> class.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        /// <param name="sDrawer">The SpriteDrawer.</param>
        public OptionsMenu(MainWindow mainWindow, SpriteDrawer sDrawer)
        {
            this.GameWindow = mainWindow;
            this.drawer = sDrawer;
            this.textFont = mainWindow.Content.Load<SpriteFont>("PokemonFontSize50");

            this.screenWidth = mainWindow.Window.ClientBounds.Width;
            this.screenHeight = mainWindow.Window.ClientBounds.Height;
            this.currentIndex = 3;

            backBtn = new Button(new Rectangle(screenWidth - (screenWidth / 8 + 20), screenHeight - (screenHeight / 10 + 50), screenWidth / 8, screenHeight / 10), GameWindow.Content.Load<Texture2D>(@"OptionsMenu\BackButton"), mainWindow);

            backBtn.OnMouseClick += backBtn_OnClick;

            controlsBtn = new Button(new Rectangle(screenWidth / 2 + 20, screenHeight - (screenHeight / 10 + 50), screenWidth / 4, screenHeight / 10), mainWindow.Content.Load<Texture2D>(@"OptionsMenu\ControlsButton"), mainWindow);

            saveChangesBtn = new Button(new Rectangle(20, screenHeight - (screenHeight / 10 + 50), (int)((float)screenWidth / 2.5), screenHeight / 10), mainWindow.Content.Load<Texture2D>(@"OptionsMenu\SaveChangesButton"), mainWindow);
            saveChangesBtn.OnMouseClick += saveChangesBtn_OnClick;

            resolutionText = new TextField("Resolution:", Color.Black, Color.Transparent, Color.Transparent, new Rectangle(300, 300, 10, 20), textFont);
            masterVolumeText = new TextField("Master Volume:", Color.Black, Color.Transparent, Color.Transparent, new Rectangle(330, 400, 0, 20), textFont);
            optionsText = new TextField("Options", Color.Black, Color.Transparent, Color.Transparent, new Rectangle(520, 150, 0, 0), textFont);

            resolutionTextField = new TextField(resolutions[currentIndex], Color.Red, Color.White, Color.Black, new Rectangle(500, 300, 10, 20), textFont);
            masterVolumeTextField = new TextField("100", Color.Red, Color.White, Color.Black, new Rectangle(500, 400, 300, 20), textFont);

            prevButtonResolution = new Button(new Rectangle(475, 300, 20, 20), mainWindow.Content.Load<Texture2D>(@"OptionsMenu\prevButton"), mainWindow);
            prevButtonResolution.OnMouseClick += prevButtonResolution_OnMouseClick;

            nextButtonResolution = new Button(new Rectangle(800, 300, 20, 20), mainWindow.Content.Load<Texture2D>(@"OptionsMenu\nextButton"), mainWindow);
            nextButtonResolution.OnMouseClick += nextButtonResolution_OnMouseClick;

            backgroundTexture = mainWindow.Content.Load<Texture2D>(@"OptionsMenu\Background");
            slider = new Slider(new Rectangle((int)(screenWidth / 1.8), screenHeight / 2 + 2, screenWidth / 4, screenHeight / 35), 20, 0, 100, 100, Color.Red, textFont, GameWindow);
            slider.OnMouseHold += slider_OnMouseHold;

            saveChangesBtn.OnMouseEnter += button_OnMouseEnter;
            saveChangesBtn.OnMouseLeave += button_OnMouseLeave;

            controlsBtn.OnMouseEnter += button_OnMouseEnter;
            controlsBtn.OnMouseLeave += button_OnMouseLeave;

            backBtn.OnMouseEnter += button_OnMouseEnter;
            backBtn.OnMouseLeave += button_OnMouseLeave;

            CalculatePositions();
        }

        private void saveChangesBtn_OnClick(Button btn)
        {
            string[] resolution = resolutions[currentIndex].Split('x');
            int width = Convert.ToInt16(resolution[0]);
            int height = Convert.ToInt16(resolution[1]);

            this.screenWidth = width;
            this.screenHeight = height;

            drawer.SetWindowSize(width, height);
            CalculatePositions();
        }

        private void backBtn_OnClick(Button btn)
        {
            GameWindow.State = MainWindow.GameState.MAINMENU;
        }

        private void button_OnMouseLeave(Button btn)
        {
            ShowHoverIndicator(btn, 10, false);
        }

        private void button_OnMouseEnter(Button btn)
        {
            ShowHoverIndicator(btn, 10, true);
        }

        private void nextButtonResolution_OnMouseClick(Button btn)
        {
            if ((currentIndex + 1) > (resolutions.Length - 1))
            {
                currentIndex = resolutions.Length - 1;
            }
            else
            {
                currentIndex++;
            }

            resolutionTextField.SetText(resolutions[currentIndex]);
        }

        private void prevButtonResolution_OnMouseClick(Button btn)
        {
            if ((currentIndex - 1) < 0)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex--;
            }

            resolutionTextField.SetText(resolutions[currentIndex]);
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
            backBtn.Update();
            controlsBtn.Update();
            saveChangesBtn.Update();
            prevButtonResolution.Update();
            nextButtonResolution.Update();
            slider.Update();
        }

        /// <summary>
        /// Draws the options menu with the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            optionsText.Draw(spriteBatch);

            prevButtonResolution.Draw(spriteBatch);
            nextButtonResolution.Draw(spriteBatch);

            backBtn.Draw(spriteBatch);
            controlsBtn.Draw(spriteBatch);
            saveChangesBtn.Draw(spriteBatch);

            resolutionText.Draw(spriteBatch);
            masterVolumeText.Draw(spriteBatch);

            resolutionTextField.Draw(spriteBatch);
            masterVolumeTextField.Draw(spriteBatch);

            slider.Draw(spriteBatch);
        }

        /// <summary>
        /// Calculates the positions of all objects in the options menu.
        /// </summary>
        private void CalculatePositions()
        {
            backBtn.SetPosition(screenWidth - (screenWidth / 8 + 20), screenHeight - (screenHeight / 10 + 20));
            backBtn.SetSize(screenWidth / 8, screenHeight / 10);

            controlsBtn.SetPosition(screenWidth / 2 + 20, screenHeight - (screenHeight / 10 + 20));
            controlsBtn.SetSize(screenWidth / 4, screenHeight / 10);

            saveChangesBtn.SetPosition(20, screenHeight - (screenHeight / 10 + 20));
            saveChangesBtn.SetSize((int)((float)screenWidth / 2.5), screenHeight / 10);

            resolutionText.SetPosition(screenWidth / 3, (int)(screenHeight / 2.5));
            resolutionText.SetSize(0, screenHeight / 25);

            resolutionTextField.SetPosition((int)(screenWidth / 1.8), (int)(screenHeight / 2.5) + 2);
            resolutionTextField.SetSize(screenWidth / 4, screenHeight / 35);

            masterVolumeText.SetPosition((int)(screenWidth / 2.8), screenHeight / 2);
            masterVolumeText.SetSize(0, screenHeight / 25);

            masterVolumeTextField.SetPosition((int)(screenWidth / 1.8), screenHeight / 2 + 2);
            masterVolumeTextField.SetSize(screenWidth / 4, screenHeight / 35);

            optionsText.SetPosition(screenWidth / 2, 30);
            optionsText.SetSize(0, screenHeight / 8);

            prevButtonResolution.SetSize(screenWidth / 60, screenHeight / 33);
            prevButtonResolution.SetPosition((int)(screenWidth / 1.8) - prevButtonResolution.GetRectangle().Width, (int)(screenHeight / 2.5) + 2);

            nextButtonResolution.SetSize(screenWidth / 60, screenHeight / 33);
            nextButtonResolution.SetPosition((int)(screenWidth / 1.8) + resolutionTextField.GetRectangle().Width, (int)(screenHeight / 2.5) + 2);

            slider.SetPosition((int)(screenWidth / 1.8), screenHeight / 2 + 2);
            slider.SetSize(screenWidth / 4, screenHeight / 35);
        }

        /// <summary>
        /// The event handle method for the mouseclick event of nextButtonResolution.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="btn">The Button.</param>
        private void nextButtonResolution_OnMouseClick(Game game, Button btn)
        {

        }

        /// <summary>
        /// The event handle method for the mouseclick event of prevButtonResolution.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="btn">The Button.</param>
        private void prevButtonResolution_OnMouseClick(Game game, Button btn)
        {
            if ((currentIndex - 1) < 0)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex--;
            }

            resolutionTextField.SetText(resolutions[currentIndex]);
        }

        /// <summary>
        /// The event handle method for the the mouseclick event of saveChangesBtn.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="btn">The Button.</param>
        private void saveChangesBtn_OnClick(Game game, Button btn)
        {

        }

        /// <summary>
        /// The event handle method for the the mouseclick event of backBtn.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="btn">The Button.</param>
        private void backBtn_OnClick(Game game, Button btn)
        {

        }

        /// <summary>
        /// The event handle method for the mouseHold event of slider.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="slider">The slider.</param>
        /// <param name="mouseState">State of the mouse.</param>
        private void slider_OnMouseHold(Game game, Slider slider, MouseState mouseState)
        {
            slider.SetSliderValue(mouseState.X);
        }

        /// <summary>
        /// The event handle method for the OnMouseEnter event of button.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="btn">The BTN.</param>
        private void button_OnMouseEnter(Game game, Button btn)
        {

        }

        /// <summary>
        /// The event handle method for the OnMouseLeave event of button.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="btn">The button.</param>
        void button_OnMouseLeave(Game game, Button btn)
        {

        }
    }
}
