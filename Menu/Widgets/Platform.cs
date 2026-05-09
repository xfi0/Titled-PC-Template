using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Widgets
{
    internal class Platform
    {
        public static void CreatePlatform(float buttonStartX, float buttonStartY, float buttonOffsetX, float buttonOffsetY, float startY, float currentCol, float currentRow)
        {
            float buttonPlatformOffsetX = 210;
            float buttonPlatformOffsetY = 85;
            float platformStartX = 190;
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect breakRect = new Rect(platformX, platformY + 25, Menu.Styles.Platform.platformWidth, 2);
            Rect buttonPlatformRect = new Rect(platformStartX + (currentCol * buttonPlatformOffsetX), startY + (currentRow * buttonPlatformOffsetY), Styles.Platform.platformWidth, Styles.Platform.platformHeight);

            GUI.Box(buttonPlatformRect, "", Menu.Styles.Platform.PlatformStyle);
            GUI.Box(breakRect, "", Menu.Styles.Platform.PlatformBreakStyle);
        }
    }
}
