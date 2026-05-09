using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Button
    {
        public static GUIStyle ButtonStyle = new GUIStyle();
        public static GUIStyle NavigationButtonStyle = new GUIStyle();
        public static GUIStyle ButtonStyleActive = new GUIStyle();
        private static readonly string buttonBackgroundColor = "#222222";
        private static readonly string buttonBackgroundColorHover = "#333232";
        private static readonly string buttonBackgroundColorActive = "#363E88";
        private static readonly string buttonTextColor = "#FFFFFF";
        public static readonly int buttonWidth = 25;
        public static readonly int navigationButtonWidth = 120;
        private static readonly int bevel = 5;
        public static void InitializeButtonStyles()
        {            
            ButtonStyle.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(buttonBackgroundColor), buttonWidth, buttonWidth, bevel, "button-normal-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(buttonBackgroundColor), "button-normal-background");
            ButtonStyle.hover.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(buttonBackgroundColorHover), buttonWidth, buttonWidth, bevel, "button-hover-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(buttonBackgroundColorHover), "button-hover-background");
            ButtonStyle.hover.textColor =  Helpers.CreateColorFromHex(buttonTextColor);
            ButtonStyle.normal.textColor =  Helpers.CreateColorFromHex(buttonTextColor);
            ButtonStyle.padding = new RectOffset(10, 10, 5, 5);
            
            ButtonStyleActive.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(buttonBackgroundColorActive), buttonWidth, buttonWidth, bevel, "button-active-normal-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(buttonBackgroundColorActive), "button-active-normal-background");
            ButtonStyleActive.normal.textColor =  Helpers.CreateColorFromHex(buttonTextColor);
            ButtonStyleActive.padding = new RectOffset(10, 10, 5, 5);

            NavigationButtonStyle.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(buttonBackgroundColor), navigationButtonWidth, buttonWidth, bevel, "navigation-button-normal-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(buttonBackgroundColor), "navigation-button-normal-background");
            NavigationButtonStyle.hover.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(buttonBackgroundColorHover), navigationButtonWidth, buttonWidth, bevel, "navigation-button-hover-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(buttonBackgroundColorHover), "navigation-button-hover-background");
            NavigationButtonStyle.normal.textColor = Helpers.CreateColorFromHex(buttonTextColor);
            NavigationButtonStyle.hover.textColor = Helpers.CreateColorFromHex(buttonTextColor);
            NavigationButtonStyle.padding = new RectOffset(10, 10, 5, 5);
        }
    }
}
