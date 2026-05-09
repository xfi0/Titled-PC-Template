using System.Collections.Generic;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Helpers
    {
        private static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        public static Texture2D CreateTexture(Color color, string name)
        {
            string newName = $"{name}_{color.r}_{color.g}_{color.b}_{color.a}";

            if (textureCache.ContainsKey(newName))
                return textureCache[newName];

            Texture2D texture = new Texture2D(2, 2);
            texture.SetPixel(0, 0, color);
            texture.SetPixel(1, 0, color);
            texture.SetPixel(0, 1, color);
            texture.SetPixel(1, 1, color);
            texture.Apply();
           
            textureCache.Add(newName, texture);

            return texture;
        }
        public static Texture2D CreateTexture(int width, int height, Color color, string name)
        {
            string newName = $"{name}_{color.r}_{color.g}_{color.b}_{color.a}";

            if (textureCache.ContainsKey(newName))
                return textureCache[newName];

            Texture2D texture = new Texture2D(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
            textureCache.Add(newName, texture);

            return texture;
        }

        public static Texture2D CreateRoundedTexture(Color color, int width, int height, int radius, string name)
        {
            string newName = $"{name}_{color.r}_{color.g}_{color.b}_{color.a}";

            if (textureCache.ContainsKey(newName))
                return textureCache[newName];

            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            Color[] colors = new Color[width * height];

            float r = color.r, g = color.g, b = color.b;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float centerx = Mathf.Clamp(x, radius, width - radius - 1);
                    float centery = Mathf.Clamp(y, radius, height - radius - 1);

                    float distancex = x - centerx, dy = y - centery;
                    float distanceSq = distancex * distancex + dy * dy;
                    float threshold = radius - 1f;

                    float alpha = Mathf.Clamp01(1f - (Mathf.Sqrt(distanceSq) - threshold));

                    colors[y * width + x] = new Color(r, g, b, alpha);
                }
            }

            texture.SetPixels(colors);
            texture.Apply();
            textureCache.Add(newName, texture);

            return texture;
        }

        public static Color CreateColorFromHex(string v)
        {
            Color temp;
            UnityEngine.ColorUtility.TryParseHtmlString(v, out temp);

            return temp;
        }
    }
}
