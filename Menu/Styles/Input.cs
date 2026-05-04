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
            inputStyle.padding = new RectOffset(10, 10, 5, 5);
        }
    }
}
