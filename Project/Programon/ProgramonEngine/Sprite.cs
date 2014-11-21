using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public struct Sprite
    {
        public static const int TextureHeight = 16;
        public static const int TextureWidth = 16;

        public Texture2D Texture { get; private set; }
        public Color Tint { get; private set; }

        public Sprite(Texture2D texture, Color tint)
        {
            Texture = texture;
            Tint = tint;
        }

        /// <summary> Returns a Sprite with a static color. </summary>
        /// <param name="staticColor"> The color of the texture. </param>
        /// <param name="tint"> A tint for the Sprite. </param>
        /// <param name="device"> The graphicsDevice to display the Sprite. </param>
        /// <returns> Static Color Sprite. </returns>
        public static Sprite FromStaticColor(Color staticColor, Color tint, GraphicsDevice device)
        {
            Texture2D texture = new Texture2D(device, TextureWidth, TextureHeight);

            Color[] colorData = new Color[TextureWidth * TextureHeight];

            for (int y = 0; y < TextureHeight; y++)
            {
                for (int x = 0; x < TextureWidth; x++)
                {
                    colorData[(TextureHeight * y) + x] = staticColor;
                }
            }

            texture.SetData<Color>(colorData);

            return new Sprite(texture, tint);
        }
    }
}
