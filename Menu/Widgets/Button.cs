using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;
using UnityEngine;

namespace Titled_PC_Template.Menu.Widgets
{
    internal class Button
    {
        public static void CreateButton(ButtonInfo[] mods, int i, float platformStartX, float buttonPlatformOffsetX, float buttonPlatformOffsetY, float startY, float currentCol, float currentRow)
        {
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect buttonRect = new Rect(platformX + 10, platformY + 40, 25, 25);
            Rect textRect = new Rect(platformX + 5, platformY + 10, 200, 200);

            GUI.Label(textRect, mods[i].DisplayText);
            if (GUI.Button(buttonRect, "", Styles.Button.buttonStyle))
            {
                Main.RunEnabled(mods[i].DisplayText);
            }
        }
    }
}
