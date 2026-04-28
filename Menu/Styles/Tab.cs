using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Menu.Styles
{
    internal class Tab
    {
        public static GUIStyle tabStyle = new GUIStyle();
        public static string tabBackgroundColor = "#222222";
        public static string tabActiveBackgroundColor = "#364088";

        public static void InitializeTabStyles()
        {
            tabStyle.normal.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabBackgroundColor));
            tabStyle.active.background = Helpers.CreateTexture(Helpers.CreateColorFromHex(tabActiveBackgroundColor));
        }
    }
}
