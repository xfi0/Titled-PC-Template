using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using Titled_PC_Template.Classes;
using Titled_PC_Template.Menu.Styles;
using Titled_PC_Template.Menu.Widgets;
using Titled_PC_Template.Mods;
using Titled_PC_Template.Notifications;
using UnityEngine;
using UnityEngine.UIElements;

// README
// This template is completely free to use, modify, and distribute under the MIT license.
// The project can be found at https://github.com/xfi0/Titled-PC-Template, where you can also report any issues or request features.
// If you paid for this template, or did not recieve it from a official source such as my github, you may have been scammed.
// Credits are cool, but not really needed.
// Thanks


namespace Titled_PC_Template
{
    [BepInPlugin("com.xfi0.Titled-PC-Template", "Example", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static string MenuName = "Example";

        public static int CurrentTab = 0;
        public static int PageNumber = 0;
        public static int ButtonsPerPage = 12;
        public static int Columns = 3;
        public static int Rows = 4;
        public static readonly int WindowId = MenuName.GetHashCode();
        public static int[] ParentTab = Enumerable.Repeat(-1, Buttons.buttons.Length).ToArray();

        public static bool IsRounded = true;

        private static Dictionary<string, ColorPicker> colorPickerInstances = new Dictionary<string, ColorPicker>(); // mod display text, color picker instance

        void Awake()
        {
            Debug.Log("Thanks for using Titled PC Template! You may remove this, or keep it, I dont mind.");
        }

        void OnGUI()
        {
            Menu.Styles.Window.InitializeWindowStyles();
            Menu.Styles.Button.InitializeButtonStyles();
            Menu.Styles.Tab.InitializeTabStyles();
            Menu.Styles.Input.InitializeInputStyles();
            Menu.Styles.Dropdown.InitializeDropdownStyles();
            Menu.Styles.Platform.InitializePlatformStyles();
            GUI.skin.horizontalScrollbar = GUIStyle.none;
            GUI.skin.verticalScrollbar = GUIStyle.none;

            Window.windowRect = UnityEngine.GUI.Window(WindowId, Window.windowRect, InitializeGUI, "", Window.windowStyle);
        }

        void Update()
        {
            HandleEnabledButtons();
        }

        private static void InitializeGUI(int windowID)
        {
            UnityEngine.GUI.Box(new Rect(0, 0, 140, Window.windowRect.height), "", Tab.tabContainerStyle);
            UnityEngine.GUI.Box(new Rect(140, 0, Window.windowRect.width - 140, Window.windowRect.height), "");

            UnityEngine.GUI.Label(new Rect(15, 5, 200, 30), MenuName);
            UnityEngine.GUI.DragWindow(new Rect(0, 0, Window.windowRect.width, 22));

            CreateTabs();
            CreateMods();
        }

        private static void CreateTabs()
        {
            var tabGroup = GetTabGroup();
            var tabs = Buttons.buttons[tabGroup].Where(b => b != null && b.IsTab).ToArray();

            float startY = 40;
            for (int i = 0; i < tabs.Length; i++)
            {
                Rect buttonRect = new Rect(5, startY + (i * 37), 130, 35);
                bool isSelected = CurrentTab == tabs[i].TabToChangeTo;

                if (isSelected)
                {
                    UnityEngine.GUI.Button(buttonRect, tabs[i].DisplayText, Tab.tabStyleActive);
                    continue;
                }

                if (UnityEngine.GUI.Button(buttonRect, tabs[i].DisplayText, Tab.tabStyle))
                {
                    ParentTab[tabs[i].TabToChangeTo] = CurrentTab;
                    CurrentTab = tabs[i].TabToChangeTo;
                    RunEnabled(tabs[i].DisplayText);
                }
            }
        }

        private static int GetTabGroup()
        {
            int group = CurrentTab;
            HashSet<int> visited = new HashSet<int>();

            while (group >= 0 && !visited.Contains(group))
            {
                visited.Add(group);

                if (Buttons.buttons[group].Any(b => b != null && b.IsTab))
                    return group;

                group = ParentTab[group];
            }

            return 0;
        }

        private static void CreateMods()
        {
            ButtonInfo[] mods = Buttons.buttons[CurrentTab].Where(b => b != null && !b.IsTab).Skip(PageNumber * ButtonsPerPage).Take(ButtonsPerPage).ToArray();

            int openDropdownIndex = -1;
            float openCol = 0;
            float openRow = 0;
            float startY = 40;
            float buttonStartY = 65;
            float buttonStartX = 190;
            float buttonOffsetY = 100;
            float buttonOffsetX = 210;
            float inputStartY = 65;
            float inputStartX = 190;
            float inputOffsetY = 100;
            float inputOffsetX = 210;
            float buttonPlatformOffsetX = 210;
            float buttonPlatformOffsetY = 85;
            float platformStartX = 190;

            for (int i = 0; i < mods.Length && i < Columns * Rows; i++)
            {
                if (mods[i] != null)
                {
                    float currentRow = i / Columns;
                    float currentCol = i % Columns;

                    Menu.Widgets.Platform.CreatePlatform(buttonStartX, buttonStartY, buttonOffsetX, buttonOffsetY, startY, currentCol, currentRow);
                    if (mods[i].IsButton)
                    {
                        Menu.Widgets.Button.CreateButton(mods[i], platformStartX, buttonPlatformOffsetX, buttonPlatformOffsetY, startY, currentCol, currentRow);
                    }
                    else if (mods[i].IsInput)
                    {
                        Menu.Widgets.Input.CreateInput(mods[i], platformStartX, buttonPlatformOffsetX, buttonPlatformOffsetY, startY, currentRow, currentCol);
                    }
                    else if (mods[i].IsDropdown)
                    {
                        Menu.Widgets.Dropdown.CreateDropdown(mods[i], ref openDropdownIndex, ref openCol, ref openRow, inputStartY, inputStartX, inputOffsetY, inputOffsetX, buttonPlatformOffsetX, buttonPlatformOffsetY, platformStartX, startY, i, currentRow, currentCol);
                    }
                    else if (mods[i].IsColorPicker)
                    {
                        if (colorPickerInstances.TryGetValue(mods[i].DisplayText, out ColorPicker existingColorPicker)) // if this mod already has a color picker instance
                        {
                            existingColorPicker.CreateColorPicker(mods[i], platformStartX, buttonPlatformOffsetX, buttonPlatformOffsetY, startY, currentCol, currentRow);
                            continue;
                        }

                        ColorPicker colorPicker = new ColorPicker(); // create new one if the mod doesnt already have one
                        colorPickerInstances.Add(mods[i].DisplayText, colorPicker);

                        colorPicker.CreateColorPicker(mods[i], platformStartX, buttonPlatformOffsetX, buttonPlatformOffsetY, startY, currentCol, currentRow);
                    }
                }
            }
            if (openDropdownIndex != -1)
            {
                ButtonInfo mod = mods[openDropdownIndex];

                for (int item = 0; item < mod.Items.Length; item++)
                {
                    Rect itemRect = new Rect(inputStartX + (openCol * inputOffsetX), inputStartY + (openRow * inputOffsetY) + ((item + 1) * 25), 180, 25);
                    if (GUI.Button(itemRect, mod.Items[item], Menu.Styles.Dropdown.DropdownStyle))
                    {
                        mod.DropdownIndex = item;
                        mod.DropdownOpen = false;
                        RunEnabled(mod.DisplayText);
                    }
                }
            }

            int totalButtons = Buttons.buttons[CurrentTab].Count(b => b != null && !b.IsTab);
            if (totalButtons > ButtonsPerPage)
            {
                float navY = Window.windowRect.height - 35;
                Rect previousPageRect = new Rect(150, navY, Menu.Styles.Button.navigationButtonWidth, 25);
                Rect nextPageRect = new Rect(280, navY, Menu.Styles.Button.navigationButtonWidth, 25);

                if (GUI.Button(previousPageRect, "Previous Page", Menu.Styles.Button.NavigationButtonStyle))
                    PageNumber = Mathf.Max(0, PageNumber - 1);

                if (GUI.Button(nextPageRect, "Next Page", Menu.Styles.Button.NavigationButtonStyle))
                {
                    int maxPage = Mathf.CeilToInt((float)totalButtons / ButtonsPerPage) - 1;
                    PageNumber = Mathf.Min(maxPage, PageNumber + 1);
                }
            }
        }

        private static void HandleEnabledButtons()
        {
            foreach (ButtonInfo[] buttonInfos in Buttons.buttons)
            {
                foreach (ButtonInfo buttonInfo in buttonInfos)
                {
                    if (buttonInfo.IsEnabled)
                        buttonInfo.Method?.Invoke();
                }
            }
        }

        public static void RunEnabled(string buttonText)
        {
            ButtonInfo target = GetIndex(buttonText);
            if (target != null)
            {
                if (target.IsToggle)
                {
                    target.IsEnabled = !target.IsEnabled;
                    if (target.IsEnabled)
                    {
                        Library.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.ToolTip);
                        if (target.OnEnable != null)
                        {
                            try
                            {
                                target.OnEnable.Invoke();
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }
                    }
                    else
                    {
                        Library.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.ToolTip);
                        if (target.OnDisable != null)
                        {
                            try
                            {
                                target.OnDisable.Invoke();
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }
                    }
                }
                else
                {
                    Library.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.ToolTip);
                    if (target.Method != null)
                    {
                        try
                        {
                            target.Method.Invoke();
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                        }
                    }
                }
            }
            else
            {
                UnityEngine.Debug.LogError(buttonText + " does not exist");
            }
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in Buttons.buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button == null)
                        continue;
                    

                    if (button.DisplayText == buttonText)
                    {
                        return button;
                    }
                }
            }

            Debug.LogError("Could not find: " + buttonText);
            return null;
        }
    }
}
