using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProgramonEngine
{
    public enum Direction
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3
    }

    public class NPC : Actor
    {
        private double NextKeyStamp = 0;
        Vector2 maxUpperLeftPosition;
        Vector2 maxDownerRightPosition;
        private bool AllowedWalking = false;
        public DialogueBox Dialogue { get; private set; }
        SpriteFont dialogFont;
        Texture2D dialogBoxTexture;
        private Rectangle bufferSize;
        int textIndex = 0;
        public bool DrawDialog { get; private set; }

        private IEnumerable<string> dialogTexts;

        public NPC(Rectangle bufferSize, Vector2 startPos, Vector2 scale, Map currentMap, Vector2 maxUpperLeftPosition, Vector2 maxDownerRightPosition, SpriteFont font, Texture2D boxTexture, IEnumerable<string> dialogTexts)
            : base(startPos, scale, currentMap)
        {
            this.maxUpperLeftPosition = maxUpperLeftPosition;
            this.maxDownerRightPosition = maxDownerRightPosition;
            this.DrawDialog = false;
            this.dialogFont = font;
            this.dialogBoxTexture = boxTexture;
            this.bufferSize = bufferSize;
            List<string> texts = new List<string>();
            for (int i = 0; i < dialogTexts.Count(); i++)
            {
                 texts.Add(dialogTexts.ElementAt(i).WrapText(boxTexture.Width - 20, font));
            }
            this.dialogTexts = texts;
        }

        public void Update(Player player, Map map, GameTime gameTime)
        {
            base.Update();

            Vector2 newPosition = Transform.Position;
            Vector2 playerPos = player.Transform.Position;

            if (newPosition + new Vector2(0f, 1f) == playerPos || newPosition + new Vector2(0f, -1f) == playerPos || newPosition + new Vector2(-1f, 0f) == playerPos || newPosition + new Vector2(1f, 0f) == playerPos)
            {
                // Player is next to NPC
                // Disable walking so its possible to interact
                AllowedWalking = false;
            }
            else
            {
                // Player is not next to NPC.
                // NPC can walk.
                DrawDialog = false;
                textIndex = 0;
                AllowedWalking = true;
            }

            if (gameTime.TotalGameTime.TotalSeconds >= NextKeyStamp)
            {
                NextKeyStamp = gameTime.TotalGameTime.TotalSeconds + 1;

                if (AllowedWalking)
                {
                    Vector2 direction = new Vector2(0, 0);

                    Random rnd = new Random();
                    int dir = rnd.Next(0, 4);
                    switch (dir)
                    {
                        case (int) Direction.UP:
                            direction = new Vector2(0f, 1f);
                            break;
                        case (int) Direction.DOWN:
                            direction = new Vector2(0f, -1f);
                            break;
                        case (int) Direction.LEFT:
                            direction = new Vector2(-1f, 0f);
                            break;
                        case (int) Direction.RIGHT:
                            direction = new Vector2(1f, 0f);
                            break;
                    }

                    //Check if x is between two points
                    if (Transform.Position.X + direction.X < maxUpperLeftPosition.X || Transform.Position.X + direction.X > maxDownerRightPosition.X)
                    {
                        direction.X *= -1f;
                    }

                    //Check if y is between two points
                    if (Transform.Position.Y + direction.Y < maxUpperLeftPosition.Y || Transform.Position.Y + direction.Y > maxDownerRightPosition.Y)
                    {
                        direction.Y *= -1f;
                    }

                    newPosition = Transform.Position + direction;

                    if (newPosition == player.Transform.Position)
                    {
                        newPosition = newPosition - direction;
                    }
                }
                else
                {
                    return;
                }

                if (map.MapDictionary.ContainsKey(newPosition))
                {
                    Move(map.MapDictionary[newPosition], player);
                }
            }
        }

        public void DisplayDialog()
        {
            if (!(textIndex + 1 > dialogTexts.Count()))
            {
                if (!DrawDialog)
                {
                    DrawDialog = true;
                }

                if (Dialogue != null)
                {
                    Dialogue.SetText(dialogTexts.ElementAt(textIndex));
                }
                else
                {
                    Dialogue = new DialogueBox(dialogTexts.ElementAt(textIndex), dialogFont, true, bufferSize, dialogBoxTexture);
                }
                textIndex++;
            }
            else
            {
                DrawDialog = false;
                textIndex = 0;
            }
        }
    }
}
