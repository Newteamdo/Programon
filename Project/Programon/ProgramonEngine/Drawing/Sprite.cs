using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramonEngine
{
    public struct Sprite
    {
        public const int NodeHeight = 64;
        public const int NodeWidth = 64;

        public Texture2D Texture { get { return Ptexture; } private set { Ptexture = value; } }
        public Color Tint { get { return Ptint; } private set { Ptint = value; } }

        private Texture2D Ptexture;
        private Color Ptint;

        public Sprite(Texture2D texture, Color tint)
        {
            Ptexture = texture;
            Ptint = tint;
        }

        public Sprite(Texture2D texture)
        {
            Ptexture = texture;
            Ptint = Color.White;
        }

        /// <summary> Returns a Sprite with a static color. </summary>
        /// <param name="staticColor"> The color of the texture. </param>
        /// <param name="tint"> A tint for the Sprite. </param>
        /// <returns> Static Color Sprite. </returns>
        public static Sprite FromStaticColor(Color staticColor, Color tint, GraphicsDevice device)
        {
            Texture2D texture = new Texture2D(device, NodeWidth, NodeHeight);

            Color[] colorData = new Color[NodeWidth * NodeHeight];

            for (int y = 0; y < NodeHeight; y++)
            {
                for (int x = 0; x < NodeWidth; x++)
                {
                    colorData[(NodeHeight * y) + x] = staticColor;
                }
            }

            texture.SetData<Color>(colorData);

            return new Sprite(texture, tint);
        }

        /// <summary> Returns a Sprite with the riven text. </summary>
        /// <param name="color"> The color of the text. </param>
        /// <param name="scale"> The scale of the texture. </param>
        /// <returns> Text Sprite </returns>
        public static Sprite FromString(string text, SpriteFont font, Color color, Vector2 scale, GraphicsDevice device)
        {
            Vector2 textSize = font.MeasureString(text) * scale;
            RenderTarget2D target = new RenderTarget2D(device, (int)textSize.X, (int)textSize.Y);
            SpriteBatch sb = new SpriteBatch(device);

            device.SetRenderTarget(target);
            device.Clear(Color.Transparent);

            sb.Begin();
            sb.DrawString(font, text, Vector2.Zero, color, 0f,Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.End();

            device.SetRenderTarget(null);

            Texture2D texture = new Texture2D(device, target.Width, target.Height);
            Color[] colorData = new Color[target.Width * target.Height];
            target.GetData<Color>(colorData);
            texture.SetData<Color>(colorData);

            target.Dispose();

            return new Sprite(texture);
        }
    }
}
