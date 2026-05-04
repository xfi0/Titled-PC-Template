using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Tab
    {
        public static GUIStyle tabStyle = new GUIStyle();
        public static GUIStyle tabStyleActive = new GUIStyle();
        public static GUIStyle tabContainerStyle = new GUIStyle();
        public static string tabContainerBackgroundColor = "#080808";
        public static string tabBackgroundColor = "#222222";
        public static string tabActiveBackgroundColor = "#363e88";
        public static string tabTextColor = "#FFFFFF";

        public static void InitializeTabStyles()
        {
            tabContainerStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabContainerBackgroundColor));

            tabStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabBackgroundColor));
            tabStyle.padding = new RectOffset(10, 10, 5, 5);
            tabStyle.normal.textColor = Helpers.CreateColorFromHex(tabTextColor);

            tabStyleActive.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabActiveBackgroundColor));
            tabStyleActive.padding = new RectOffset(10, 10, 5, 5);
            tabStyleActive.normal.textColor = Helpers.CreateColorFromHex(tabTextColor);
        }
    }
}
