using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Titled_PC_Template.Menu.Styles;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Window
    {
        public static Rect windowRect = new Rect(100, 100, 800, 500);
        public static GUIStyle windowStyle = new GUIStyle();
        public static GUIStyle buttonPlatformStyle = new GUIStyle();
        public static GUIStyle buttonStyle = new GUIStyle();
        public static void InitializeWindowStyles()
        {
            windowStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex("#101010"));
            buttonPlatformStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex("#090909"));
            buttonStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex("#222222"));
        }
    
    }
}
