using System;

namespace Titled_PC_Template.Classes
{
    public class ButtonInfo
    {
        public string DisplayText = string.Empty; // the text of the button
        public bool IsEnabled = false; // if the button is enabled
        public bool IsButton = false; // is it a button? if so, if its also a toggle , it will run on enabled and disabled, and every frame it runs method, if its not a toggle it will just run method on click.
        public bool IsTab = false; // is it a tab? if true on click it will change the tab to whatever TabToChangeTo is set tos
        public bool IsToggle = false; // is it a toggle? if true on click it will run on enabled, if clicked again it will run on disable, and every frame it runs method.
        public bool IsInput = false; // is it a string input?
        public bool IsDropdown = false; // is it a dropdown?
        public bool DropdownOpen = false; // is the dropdown open?
        public int TabToChangeTo = 0; // tab button changes to (if its a tab)
        public int DropdownIndex = 0; // dropdown selected index
        public Action OnEnable; // on toggle on (if toggle)
        public Action OnDisable; // on toggle off (if toggle)
        public Action Method; // every frame, (if toggle)
        public string[] Items; // items in the dropdown
        public string InputValue = string.Empty;
        public string ToolTip = "This button does not have a tooltip."; // sent by notif lib
    }
}
