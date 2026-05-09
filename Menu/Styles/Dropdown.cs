using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Dropdown
    {
        public static GUIStyle DropdownStyle = new GUIStyle();
        private static readonly string dropdownBackgroundColor = "#222222";
        private static readonly string dropdownTextColor = "#FFFFFF";
        public static readonly int DropdownWidth = 165;
        public static readonly int DropdownHeight = 25;
        private static readonly int bevel = 3;

        public static void InitializeDropdownStyles()
        {
            DropdownStyle.normal.background = Main.IsRounded ? Helpers.CreateRoundedTexture(Helpers.CreateColorFromHex(dropdownBackgroundColor), DropdownWidth, DropdownHeight, bevel, "dropdown-normal-background-rounded") : Helpers.CreateTexture(Helpers.CreateColorFromHex(dropdownBackgroundColor), "dropdown-normal-background");
            DropdownStyle.normal.textColor = Helpers.CreateColorFromHex(dropdownTextColor);
            DropdownStyle.padding = new RectOffset(10, 10, 5, 5);
        }
    }
}
