using Unity.XR.CoreUtils.Collections;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Tab
    {
        public static GUIStyle tabStyle = new GUIStyle();
        public static GUIStyle tabStyleActive = new GUIStyle();
        public static GUIStyle tabContainerStyle = new GUIStyle();
        private static readonly string tabContainerBackgroundColor = "#080808";
        private static readonly string tabBackgroundColor = "#222222";
        private static readonly string tabBackgroundColorHover = "#333232";
        private static readonly string tabActiveBackgroundColor = "#363e88";
        private static readonly string tabTextColor = "#FFFFFF";
        private static readonly int tabHeight = 35;
        private static readonly int tabWidth = 130;
        private static readonly int bevel = 3;

        public static void InitializeTabStyles()
        {
            tabContainerStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabContainerBackgroundColor), "tab-container-background");

            tabStyle.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(tabBackgroundColor), tabWidth, tabHeight, bevel, "tab-normal-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(tabBackgroundColor), "tab-normal-background");
            tabStyle.normal.textColor = Helpers.CreateColorFromHex(tabTextColor);
            tabStyle.hover.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(tabBackgroundColorHover), tabWidth, tabHeight, bevel, "tab-hover-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(tabBackgroundColorHover), "tab-hover-background");
            tabStyle.hover.textColor = Helpers.CreateColorFromHex(tabTextColor);
            tabStyle.padding = new RectOffset(10, 10, 5, 5);

            tabStyleActive.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(tabActiveBackgroundColor), tabWidth, tabHeight, 3, "tab-active-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(tabActiveBackgroundColor), "tab-active-background");
            tabStyleActive.padding = new RectOffset(10, 10, 5, 5);
            tabStyleActive.normal.textColor = Helpers.CreateColorFromHex(tabTextColor);
        }
    }
}
