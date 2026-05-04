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
        public static void CreateDropdown(ButtonInfo[] mods, ref int openDropdownIndex, ref float openCol, ref float openRow, float inputStartY, float inputStartX, float inputOffsetY, float inputOffsetX, float buttonPlatformOffsetX, float buttonPlatformOffsetY, float platformStartX, float startY, int i, float currentRow, float currentCol)
        {
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect dropdownRect = new Rect(inputStartX + (currentCol * inputOffsetX), inputStartY + (currentRow * inputOffsetY), 180, 25);
            Rect textRect = new Rect(platformX + 5, platformY + 10, 200, 200);

            GUI.Label(textRect, mods[i].DisplayText);
            if (GUI.Button(dropdownRect, mods[i].Items[mods[i].DropdownIndex], Menu.Styles.Button.buttonStyle))
            {
                mods[i].DropdownOpen = !mods[i].DropdownOpen;
            }

            if (mods[i].DropdownOpen)
            {
                openDropdownIndex = i;
                openCol = currentCol;
                openRow = currentRow;
            }
        }
    }
}
