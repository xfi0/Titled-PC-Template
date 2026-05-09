using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Input
    {
        public static GUIStyle inputStyle;
        private static string InputBackgroundColor = "#202020";
        private static string InputTextColor = "#FFFFFF";
        public static readonly int inputWidth = 125;
        public static readonly int inputHeight = 25;
        private static readonly int bevel = 3;

        public static void InitializeInputStyles()
        {
            inputStyle = new GUIStyle();
            inputStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(InputBackgroundColor), "input-norma-background");
            inputStyle.normal.textColor = Helpers.CreateColorFromHex(InputTextColor);
            inputStyle.padding = new RectOffset(10, 10, 5, 5);
        }
    }
}
