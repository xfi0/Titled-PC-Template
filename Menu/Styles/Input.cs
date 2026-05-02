using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Input
    {
        private static string inputBackgroundColor = "#202020";
        public static GUIStyle inputStyle;

        public static void InitializeInputStyles()
        {
            inputStyle = new GUIStyle();
            inputStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(inputBackgroundColor));
            inputStyle.normal.textColor = Color.white;
        }
    }
}
