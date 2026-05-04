using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;
using UnityEngine;

namespace Titled_PC_Template.Menu.Widgets
{
    internal class Input
    {
        public static void CreateInput(ButtonInfo[] mods, float inputStartY, float inputOffsetY, int i, float platformStartX, float buttonPlatformOffsetX, float buttonPlatformOffsetY, float startY, float currentRow, float currentCol)
        {
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect inputRect = new Rect(platformX, inputStartY + (currentRow * inputOffsetY), 180, 25);
            Rect textRect = new Rect(platformX + 5, platformY + 10, 200, 200);

            GUI.Label(textRect, mods[i].DisplayText);
            mods[i].InputValue = GUI.TextField(inputRect, mods[i].InputValue, Menu.Styles.Input.inputStyle);
        }
    }
}
