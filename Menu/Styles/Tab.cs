using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Tab
    {
        public static GUIStyle tabStyle = new GUIStyle();
        public static GUIStyle tabContainerStyle = new GUIStyle();
        public static string tabContainerBackgroundColor = "#080808";
        public static string tabBackgroundColor = "#222222";
        public static string tabActiveBackgroundColor = "#364088";
        public static string tabTextColor = "#FFFFFF";

        public static void InitializeTabStyles()
        {
            tabContainerStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabContainerBackgroundColor));

            tabStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabBackgroundColor));
            tabStyle.padding = new RectOffset(10, 10, 5, 5);
            tabStyle.active.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabActiveBackgroundColor));
            tabStyle.normal.textColor = Helpers.CreateColorFromHex(tabTextColor);
        }
    }
}
