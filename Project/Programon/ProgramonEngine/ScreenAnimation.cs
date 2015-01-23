using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class ScreenAnimation
    {
        Texture2D pixel;
        GraphicsDevice gd;
        SpriteBatch sb;

        Vector2 position;
        int screenWidth;
        int screenHeight;
        int recWidth;
        int recHeight;

        public Color AnimationColor { get; private set; }
        public bool Finished { get; private set; }

        public ScreenAnimation(GraphicsDevice gfxDevice, SpriteBatch spriteBatch, Color animationColor)
        {
            gd = gfxDevice;
            sb = spriteBatch;
            recWidth = 0;
            recHeight = 0;
            screenWidth = gd.Viewport.Width;
            screenHeight = gd.Viewport.Height;
            position = new Vector2(screenWidth / 2, screenHeight / 2);
            Finished = false;
            AnimationColor = animationColor;

            pixel = new Texture2D(gd, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// Update animation
        /// </summary>
        /// <param name="gameTime">The current GameTime</param>
        /// <returns>Returns true if the animation is finished</returns>
        public bool Update(GameTime gameTime)
        {
            if (Finished)
            {
                Finished = false;
                recWidth = 0;
                recHeight = 0;
            }
            else
            {
                if (!Finished && recWidth >= screenWidth && recHeight >= screenHeight)
                {
                    Finished = true;
                }
                else
                {
                    recWidth += 24;
                    recHeight += 24;
                }
            }

            return Finished;
        }
        
        public void Draw()
        {
            int startX = ((int)position.X) - (recWidth / 2);
            int startY = ((int)position.Y) - (recHeight / 2);

            //for (int x = startX; x <= (startX + recWidth); x++)
            //{
            //    for (int y = startY; y <= (startY + recHeight); y++)
            //    {
            //        sb.Draw(pixel, new Vector2(x, y), AnimationColor);
            //    }
            //}

            Rectangle rec = new Rectangle(startX, startY, recWidth, recHeight);
            sb.Draw(pixel, rec, AnimationColor);
        }
    }
}