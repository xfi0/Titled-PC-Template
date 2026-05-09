using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;
using Titled_PC_Template.Menu.Styles;
using UnityEngine;

namespace Titled_PC_Template.Menu.Widgets
{
    internal class Input
    {
        public static void CreateInput(ButtonInfo mod, float platformStartX, float buttonPlatformOffsetX, float buttonPlatformOffsetY, float startY, float currentRow, float currentCol)
        {
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect inputRect = new Rect(platformX + 12, platformY + 40, Styles.Input.inputWidth, Styles.Input.inputHeight);
            Rect textRect = new Rect(platformX + 7, platformY + 5, 200, 200);

            GUI.Label(textRect, mod.DisplayText);
            mod.InputValue = GUI.TextField(inputRect, mod.InputValue, Menu.Styles.Input.inputStyle);
        }
    }
}
