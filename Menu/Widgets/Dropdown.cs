using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;
using UnityEngine;

namespace Titled_PC_Template.Menu.Widgets
{
    internal class Dropdown
    {
        public static void CreateDropdown(ButtonInfo mod, ref int openDropdownIndex, ref float openCol, ref float openRow, float inputStartY, float inputStartX, float inputOffsetY, float inputOffsetX, float buttonPlatformOffsetX, float buttonPlatformOffsetY, float platformStartX, float startY, int i, float currentRow, float currentCol)
        {
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect dropdownRect = new Rect(platformX + 12, platformY + 40, Styles.Dropdown.DropdownWidth, Styles.Dropdown.DropdownHeight);
            Rect textRect = new Rect(platformX + 7, platformY + 5, 200, 200);

            GUI.Label(textRect, mod.DisplayText);
            if (GUI.Button(dropdownRect, mod.Items[mod.DropdownIndex], Menu.Styles.Dropdown.DropdownStyle))
                mod.DropdownOpen = !mod.DropdownOpen;
            

            if (mod.DropdownOpen)
            {
                openDropdownIndex = i;
                openCol = currentCol;
                openRow = currentRow;
            }
        }
    }
}
