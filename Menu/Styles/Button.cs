using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Button
    {
        public static string buttonPlatformBackgroundColor = "#090909";
        public static string buttonBackgroundColor = "#222222";
        public static GUIStyle buttonPlatformStyle = new GUIStyle();
        public static GUIStyle buttonStyle = new GUIStyle();

        public static void InitializeButtonStyles()
        {
            buttonPlatformStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(buttonPlatformBackgroundColor));
            buttonStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(buttonBackgroundColor));
        }
    }
}
