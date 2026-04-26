using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titled_PC_Template.Classes
{
    public class ButtonInfo
    {
        public string DisplayText = string.Empty; // the text of the button
        public bool IsEnabled = false; // if the button is enabled
        public bool IsTab = false; // is it a tab? if true on click it will change the tab to whatever TabToChangeTo is set tos
        public bool IsToggle = false; // is it a toggle? if true on click it will run on enabled, if clicked again it will run on disable, and every frame it runs method.
        public bool IsInput = false; // is it a string input?
        public int TabToChangeTo = 0; // tab button changes to (if its a tab)
        public Action OnEnable; // on toggle on (if toggle)
        public Action OnDisable; // on toggle off (if toggle)
        public Action Method; // every frame, (if toggle)
        public string InputValue = string.Empty;
        public string ToolTip = "This button does not have a tooltip."; // sent by notif lib
    }
}
