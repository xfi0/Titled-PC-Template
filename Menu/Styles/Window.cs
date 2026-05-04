using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Window
    {
        public static string windowBackgroundColor = "#101010";
        public static Rect windowRect = new Rect(Screen.width / 2 - 400, Screen.height / 2 - 250, 800, 500);
        public static GUIStyle windowStyle = new GUIStyle();
        public static void InitializeWindowStyles()
        {
            windowStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(windowBackgroundColor));
        }
    }
}
