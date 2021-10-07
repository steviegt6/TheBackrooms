using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TheBackrooms.Core.Assets
{
    public static class GeneratedAssets
    {
        public static Texture2D LineEnding;
        public static Texture2D LineMiddle;

        public static void GenerateAssets()
        {
            Color w = Color.White;
            Color b = Color.Black;
            Color t = Color.Transparent;

            LineEnding = new Texture2D(Main.graphics.GraphicsDevice, 2, 6);
            LineMiddle = new Texture2D(Main.graphics.GraphicsDevice, 2, 6);

            Color[] data =
            {
                t, t,
                t, t,
                b, b,
                b, b,
                t, t,
                t, t
            };
            LineEnding.SetData(data);
            
            data = new[]
            {
                b, b,
                b, b,
                w, w,
                w, w,
                b, b,
                b, b
            };
            LineMiddle.SetData(data);
        }
    }
}