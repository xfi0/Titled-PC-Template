using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Window
    {
        public static string windowBackgroundColor = "#101010";
        public static Rect windowRect = new Rect(Screen.width / 2 - 400, Screen.height / 2 - 250, 800, 500);
        public static GUIStyle windowStyle = new GUIStyle();
        private static int windowHeight = (int)windowRect.height;
        private static int windowWidth = (int)windowRect.width;
        private static int bevel = 3;
        public static void InitializeWindowStyles()
        {
            windowStyle.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(windowBackgroundColor), windowWidth, windowHeight, bevel, "window-normal-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(windowBackgroundColor), "window-normal-background");
        }
    }
}
