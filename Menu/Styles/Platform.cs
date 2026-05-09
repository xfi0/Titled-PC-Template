using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Platform
    {
        public static GUIStyle PlatformStyle = new GUIStyle();
        public static GUIStyle PlatformBreakStyle = new GUIStyle();

        private static readonly string platformBackgroundColor = "#090909";
        private static readonly string platformBreakBackgroundColor = "#222222";
        public static readonly int platformWidth = 200;
        public static readonly int platformHeight = 75;
        private static readonly int bevel = 5;

        public static void InitializePlatformStyles()
        {
            PlatformStyle.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(platformBackgroundColor), platformWidth, platformHeight, bevel, "button-platform-normal-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(platformBackgroundColor), "button-platform-normal-background");
            PlatformBreakStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(platformBreakBackgroundColor), "separator-normal-background");
        }
    }
}
