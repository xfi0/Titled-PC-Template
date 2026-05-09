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
        public static void CreateButton(ButtonInfo mod, float platformStartX, float buttonPlatformOffsetX, float buttonPlatformOffsetY, float startY, float currentCol, float currentRow)
        {
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect buttonRect = new Rect(platformX + 12, platformY + 40, Styles.Button.buttonWidth, Styles.Button.buttonWidth);
            Rect textRect = new Rect(platformX + 7, platformY + 5, 200, 200);

            GUI.Label(textRect, mod.DisplayText);
            if (mod.IsEnabled)
            {
                if (GUI.Button(buttonRect, "", Styles.Button.ButtonStyleActive))
                    Main.RunEnabled(mod.DisplayText);

                return;
            }

            if (GUI.Button(buttonRect, "", Styles.Button.ButtonStyle))
            {
                Main.RunEnabled(mod.DisplayText);
            }
        }
    }
}
