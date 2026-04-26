using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Helpers
    {
        public static Texture2D CreateTexture(Color color)
        {
            var texture = new Texture2D(2, 2);
            texture.SetPixel(0, 0, color);

            return texture;
        }

        internal static Color CreateColorFromHex(string v)
        {
            Color temp;
            UnityEngine.ColorUtility.TryParseHtmlString(v, out temp);

            return temp;
        }
    }
}
