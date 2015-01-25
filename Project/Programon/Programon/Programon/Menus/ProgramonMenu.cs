using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramonEngine;

namespace Programon
{
    public class ProgramonMenu : IGuiItem
    {
        private Stats stats { get; set; }
        private SpriteDrawer spritedrawer { get; set; }
        private Rectangle[] hpBackgroundBars { get; set; }
        private Rectangle[] hpBars { get; set; }
        private Rectangle[] xpBackgroundBars { get; set; }
        private Rectangle fillFullScreenRectangle { get; set; }
        private Rectangle[] designLine { get; set; }
        private Rectangle[] arrayRect { get; set; }
        private Rectangle[] xpBars { get; set; }
        private TextField[] hpTextField { get; set; }
        private TextField[] levelTextField { get; set; }
        private TextField[] nameTextField { get; set; }
        private Texture2D outlinedSquare { get; set; }
        private Button btnQuit { get; set; }
        //private mainwindow mainwindow { get; set; }
        private Creature creature { get; set; }
        private int screenWidth { get; set; }
        private int screenHeight { get; set; }
        private int amountOfProgramons { get; set; }
        private int maxHealth { get; set; }
        private int currentHealth { get; set; }
        private int level { get; set; }
        private int gamestate { get; set; }
        MainWindow mainwindow { get; set; }

        public ProgramonMenu()
        {
            amountOfProgramons = 3;
            this.spritedrawer = null;
            this.fillFullScreenRectangle = new Rectangle();
            xpBars = new Rectangle[amountOfProgramons];
            designLine = new Rectangle[amountOfProgramons];
            arrayRect = new Rectangle[amountOfProgramons];
            hpTextField = new TextField[amountOfProgramons];
            levelTextField = new TextField[amountOfProgramons];
            xpBackgroundBars = new Rectangle[amountOfProgramons];
            hpBars = new Rectangle[amountOfProgramons];
            hpBackgroundBars = new Rectangle[amountOfProgramons];
            nameTextField = new TextField[amountOfProgramons];
            Texture2D texture = mainwindow.Content.Load<Texture2D>("QuitButton"); 
            btnQuit = new Button(new Rectangle(100, 100, 100, 100), texture, mainwindow);
            btnQuit.OnMouseClick += btnQuit_OnMouseClick;
            //maxHealth = stats.maxHealth;
            //currentHealth = stats.health;
            //level = creature.level;
        }

        public ProgramonMenu(SpriteDrawer spritedrawer, MainWindow mainwindow)
        {
            amountOfProgramons = 3;
            this.spritedrawer = spritedrawer;
            xpBars = new Rectangle[amountOfProgramons];
            designLine = new Rectangle[amountOfProgramons];
            arrayRect = new Rectangle[amountOfProgramons];
            hpTextField = new TextField[amountOfProgramons];
            levelTextField = new TextField[amountOfProgramons];
            xpBackgroundBars = new Rectangle[amountOfProgramons];
            hpBars = new Rectangle[amountOfProgramons];
            hpBackgroundBars = new Rectangle[amountOfProgramons];
            nameTextField = new TextField[amountOfProgramons];
            this.gamestate = gamestate;
            Texture2D texture = mainwindow.Content.Load<Texture2D>("QuitButton"); 
            btnQuit = new Button(new Rectangle(500,500,200,100), texture, mainwindow);
            btnQuit.OnMouseClick += btnQuit_OnMouseClick;
            this.mainwindow = mainwindow;
            //btnQuit = new Button(new Rectangle(10,10,10,10),)
            //maxHealth = stats.maxHealth;
            //currentHealth = stats.health;
            //level = creature.level;
        }

        void btnQuit_OnMouseClick(Button btn)
        {
            this.mainwindow.SetState((GameState)mainwindow.laststate);
        }

        public void Initialize()
        {
            outlinedSquare = mainwindow.Content.Load<Texture2D>("vierkant");
            fillFullScreenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
        }

        public void Update()
        {
            btnQuit.Update();
            screenWidth = spritedrawer.BufferSize.Width;
            screenHeight = spritedrawer.BufferSize.Height;
            CheckAmountOfProgramon();
        }


        /// <summary>
        /// This will check and set the position of the programon items.
        /// </summary>
        public void CheckAmountOfProgramon()
        {
            if (amountOfProgramons == 1 || amountOfProgramons <= 3)
            {
                arrayRect[0] = new Rectangle(screenWidth / 20, screenHeight / 23, screenWidth / 6, screenHeight / 5);
                SetXPBar(90, 100, 0);
                SetXPLabels(90, 100, 0);
                SetLevelLabel(30, 0);
                SetHPBar(10, 100, 0);
                SetNameLabel("Sir programon 1", 0);
            }
            if (amountOfProgramons == 2 || amountOfProgramons <= 3 && amountOfProgramons != 1)
            {
                arrayRect[1] = new Rectangle(screenWidth / 20, ((screenHeight / 2) - (screenHeight / 5)) - ((screenWidth / 24) / 2), screenWidth / 6, screenHeight / 5);
                SetXPBar(40, 100, 1);
                SetXPLabels(40, 100, 1);
                SetLevelLabel(30, 1);
                SetHPBar(100, 100, 1);
                SetNameLabel("Sir programon 2", 1);
            }
            if (amountOfProgramons <= 3 && amountOfProgramons != 2 && amountOfProgramons != 1)
            {
                arrayRect[2] = new Rectangle(screenWidth / 20, screenHeight / 2, screenWidth / 6, screenHeight / 5);
                SetXPBar(99, 100, 2);
                SetXPLabels(99, 100, 2);
                SetHPBar(99, 100, 2);
                SetLevelLabel(50, 2);
                SetHPBar(30, 100, 2);
                SetNameLabel("Sir programon 3", 2);
            }
        }

        /// <summary>
        /// Sets the current xp bar and background xp bar.
        /// </summary>
        /// <param name="currentXP">Current programon XP</param>
        /// <param name="maxXP">Max programon XP</param>
        /// <param name="programonPlace">Place in player inventory.</param>
        public void SetXPBar(int currentXP, int maxXP, int programonPlace)
        {
            if (currentXP <= maxXP || programonPlace >= 0 && programonPlace <= 2)
            {
                switch (programonPlace)
                {
                    case 0:
                        xpBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 23) + ((screenHeight / 5) / 2), (((screenWidth / 3) * 2) / maxXP) * currentXP, screenHeight / 60);
                        xpBackgroundBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 23) + ((screenHeight / 5) / 2), ((screenWidth / 3) * 2) / maxXP * maxXP, screenHeight / 60);
                        break;
                    case 1:
                        xpBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 5) + (screenHeight / 6), (((screenWidth / 3) * 2) / maxXP) * currentXP, screenHeight / 60);
                        xpBackgroundBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 5) + (screenHeight / 6), (((screenWidth / 3) * 2) / maxXP) * maxXP, screenHeight / 60);
                        break;
                    case 2:
                        xpBars[programonPlace] = new Rectangle(screenWidth / 4, screenHeight / 2 + screenHeight / 10, (((screenWidth / 3) * 2) / maxXP) * currentXP, screenHeight / 60);
                        xpBackgroundBars[programonPlace] = new Rectangle(screenWidth / 4, screenHeight / 2 + screenHeight / 10, (((screenWidth / 3) * 2) / maxXP) * maxXP, screenHeight / 60);
                        break;
                }
            }
        }
        /// <summary>
        /// Sets the name label.
        /// </summary>
        /// <param name="nameProgramon">The programon name</param>
        /// <param name="programonPlace">Place in player inventory.</param>
        public void SetNameLabel(string nameProgramon, int programonPlace)
        {
            if (programonPlace >= 0 && programonPlace <= 2)
            {
                switch (programonPlace)
                {
                    case 0:
                        nameTextField[programonPlace] = new TextField(nameProgramon, Color.Black, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 4, screenHeight / 13, screenWidth / 5, 0), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                    case 1:
                        nameTextField[programonPlace] = new TextField(nameProgramon, Color.Black, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 4, screenHeight / 4 + screenHeight / 19, screenWidth / 5, 0), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                    case 2:
                        nameTextField[programonPlace] = new TextField(nameProgramon, Color.Black, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 4, screenHeight / 3 + screenHeight / 5, screenWidth / 5, 0), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                }
            }
        }

        /// <summary>
        /// Will set the sice based on the current HP.
        /// </summary>
        /// <param name="currentHP">The current programon HP.</param>
        /// <param name="maxHP">The maximal programon HP.</param>
        /// <param name="pokemonPlace">The programon lace (min 1, max 3).</param>
        public void SetHPBar(int currentHP, int maxHP, int programonPlace)
        {
            if (currentHP <= maxHP || programonPlace >= 0 && programonPlace <= 2)
            {
                switch (programonPlace)
                {
                    case 0:
                        hpBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 17) + ((screenHeight / 10) / 2), (((screenWidth / 3) * 2) / maxHP) * currentHP, screenHeight / 60);
                        hpBackgroundBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 17) + ((screenHeight / 10) / 2), ((screenWidth / 3) * 2) / maxHP * maxHP, screenHeight / 60);
                        break;
                    case 1:
                        hpBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 6) * 2, (((screenWidth / 3) * 2) / maxHP) * currentHP, screenHeight / 60);
                        hpBackgroundBars[programonPlace] = new Rectangle(screenWidth / 4, (screenHeight / 6) * 2, (((screenWidth / 3) * 2) / maxHP) * maxHP, screenHeight / 60);
                        break;
                    case 2:
                        hpBars[programonPlace] = new Rectangle(screenWidth / 4, screenHeight / 2 + screenHeight / 14, (((screenWidth / 3) * 2) / maxHP) * currentHP, screenHeight / 60);
                        hpBackgroundBars[programonPlace] = new Rectangle(screenWidth / 4, screenHeight / 2 + screenHeight / 14, (((screenWidth / 3) * 2) / maxHP) * maxHP, screenHeight / 60);
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the level labels.
        /// </summary>
        /// <param name="level">current programon level</param>
        /// <param name="programonPlace">The programon lace (min 1, max 3). </param>
        private void SetLevelLabel(int level, int programonPlace)
        {
            if (level <= 99)
            {
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("Level:{0}", level.ToString());
                switch (programonPlace)
                {
                    case 0:
                        levelTextField[programonPlace] = new TextField(strbld.ToString(), Color.Brown, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 15 * 10, screenHeight / 6, 0, screenHeight / 24), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                    case 1:
                        levelTextField[programonPlace] = new TextField(strbld.ToString(), Color.Brown, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 15 * 10, (screenHeight / 33 * 11) + screenHeight / 14, 0, screenHeight / 24), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                    case 2:
                        levelTextField[programonPlace] = new TextField(strbld.ToString(), Color.Brown, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 15 * 10, screenHeight / 22 * 14 + screenHeight / 200, 0, screenHeight / 24), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the xp labels.
        /// </summary>
        /// <param name="currentXP">Current programon XP</param>
        /// <param name="maxXP">Max programon XP</param>
        /// <param name="programonPlace">Place in player inventory.</param>
        private void SetXPLabels(int currentXP, int maxXP, int programonPlace)
        {
            if (currentXP <= maxXP || programonPlace >= 0 && programonPlace <= 2)
            {
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("XP:{0}/{1}", currentXP, maxXP);
                switch (programonPlace)
                {
                    case 0:
                        hpTextField[programonPlace] = new TextField(strbld.ToString(), Color.Black, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 14 * 5, screenHeight / 6, 0, screenHeight / 24), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                    case 1:
                        hpTextField[programonPlace] = new TextField(strbld.ToString(), Color.Black, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 14 * 5, (screenHeight / 8) + (screenHeight / 8) + (screenHeight / 7), 0, screenHeight / 24), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                    case 2:
                        hpTextField[programonPlace] = new TextField(strbld.ToString(), Color.Black, Color.Transparent, Color.Transparent, new Rectangle(screenWidth / 14 * 5, screenHeight / 2 + screenHeight / 8, 0, screenHeight / 24), mainwindow.Content.Load<SpriteFont>("Fonts/DebugFont"));
                        break;
                }
            }
        }
        /// <summary>
        /// draws everything on this menu
        /// </summary>
        /// <param name="spritebatch">Spritebatch</param>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Sprite.FromStaticColor(Color.Black, Color.White, spritebatch.GraphicsDevice).Texture, fillFullScreenRectangle, Color.White * 0.7f);
            if (amountOfProgramons < 4)
            {
                for (int i = 0; i < amountOfProgramons; i++)
                {
                    spritebatch.Draw(Sprite.FromStaticColor(Color.DarkRed, Color.White, spritebatch.GraphicsDevice).Texture, hpBackgroundBars[i], Color.White);
                    spritebatch.Draw(Sprite.FromStaticColor(Color.DarkBlue, Color.White, spritebatch.GraphicsDevice).Texture, xpBackgroundBars[i], Color.White);

                    spritebatch.Draw(Sprite.FromStaticColor(Color.Red, Color.White, spritebatch.GraphicsDevice).Texture, hpBars[i], Color.White);
                    spritebatch.Draw(Sprite.FromStaticColor(Color.Blue, Color.White, spritebatch.GraphicsDevice).Texture, xpBars[i], Color.White);

                    spritebatch.Draw(outlinedSquare, arrayRect[i], Color.White);
                    hpTextField[i].Draw(spritebatch);
                    levelTextField[i].Draw(spritebatch);
                    nameTextField[i].Draw(spritebatch);
                }
            }
            btnQuit.Draw(spritebatch);
        }
    }

    public class CheaterClass : IMenu
    {
        public IGuiItem[] Childs
        {
            get
            {
                return childs;
            }
            set
            {
                childs = value;
            }
        }

        private IGuiItem[] childs;
    }
}
