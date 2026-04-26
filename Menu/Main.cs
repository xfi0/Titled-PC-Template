using BepInEx;
using Photon.Voice.PUN.UtilityScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;
using Titled_PC_Template.Menu.Styles;
using Titled_PC_Template.Mods;
using UnityEngine;

namespace Titled_PC_Template
{
    [BepInPlugin("com.xfi0.Titled-PC-Template", "Example", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static string MenuName = "Example";
        public static int CurrentTab = 0;
        public static int PageNumber = 0;
        public static int ButtonsPerPage = 6;
        public static int Columns = 2;
        public static int Rows = 3;


        void Awake()
        {
            Debug.Log("Thanks for using Titled PC Template! You may remove this, or keep it, I dont mind.");
        }

        void OnGUI()
        {
            Window.InitializeWindowStyles();
            Window.windowRect = UnityEngine.GUI.Window(10000, Window.windowRect, InitializeGUI, "", Window.windowStyle);
        }

        private static void InitializeGUI(int windowID)
        {
            UnityEngine.GUI.Box(new Rect(0, 0, 140, Window.windowRect.height), "");
            UnityEngine.GUI.Box(new Rect(140, 0, Window.windowRect.width - 140, Window.windowRect.height), "");

            UnityEngine.GUI.Label(new Rect(15, 5, 200, 30), MenuName);
            UnityEngine.GUI.DragWindow(new Rect(0, 0, Window.windowRect.width, 22));

            CreateTabs();
            CreateMods();
        }

        private static void CreateTabs()
        {
            var Tabs = Buttons.buttons[0].Where(b => b.IsTab).ToArray();

            float startY2 = 40;
            for (int i = 0; i < Tabs.Length; i++)
            {
                Rect buttonRect = new Rect(5, startY2 + (i * 37), 130, 35);
                bool isSelected = (CurrentTab == Tabs[i].TabToChangeTo);

                if (UnityEngine.GUI.Button(buttonRect, Tabs[i].DisplayText))
                {
                    CurrentTab = Tabs[i].TabToChangeTo;
                    RunEnabled(Tabs[i].DisplayText);
                }
            }
        }

        private static void CreateMods()
        {
            ButtonInfo[] Mods = Buttons.buttons[CurrentTab].Skip(PageNumber * ButtonsPerPage).Take(ButtonsPerPage).ToArray();
            for (int i = 0; i < Mods.Length && i < Columns * Rows; i++)
            {
                if (Mods[i] != null)
                {
                    float currentRow = i / Columns;
                    float currentCol = i % Columns;
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
                    float textStartX = 190;
                    float textStartY = 40;

                    CreatePlatform(buttonStartX, buttonStartY, buttonOffsetX, buttonOffsetY, startY, currentCol, currentRow);
                    if (!Mods[i].IsTab && !Mods[i].IsInput)
                    {
                        Rect buttonRect = new Rect(buttonStartX + (currentCol * buttonOffsetX), buttonStartY + (currentRow * buttonOffsetY), 25, 25);

                        Rect textRect = new Rect(textStartX + (currentCol * buttonPlatformOffsetX), textStartY + (currentRow * buttonPlatformOffsetY), 200, 200);
                        GUI.Label(textRect, Mods[i].DisplayText);
                        if (GUI.Button(buttonRect, "", Window.buttonStyle))
                        {
                            RunEnabled(Mods[i].DisplayText);
                        }
                    }
                    else
                    {
                        Rect inputRect = new Rect(inputStartX + (currentCol * inputOffsetX), inputStartY + (currentRow * inputOffsetY), 180, 25);
                        Rect textRect = new Rect(textStartX + (currentCol * buttonPlatformOffsetX), textStartY + (currentRow * buttonPlatformOffsetY), 200, 200);
                        GUI.Label(textRect, Mods[i].DisplayText);
                        Mods[i].InputValue = GUI.TextField(inputRect, Mods[i].InputValue);
                    }
                }
            }
        }
        
        private static void CreatePlatform(float buttonStartX, float buttonStartY, float buttonOffsetX, float buttonOffsetY, float startY, float currentCol, float currentRow)
        {
            float buttonPlatformOffsetX = 210;
            float buttonPlatformOffsetY = 85;
            float platformStartX = 190;

            Rect breakRect = new Rect(buttonStartX + (currentCol * buttonOffsetX + 5), buttonStartY + (currentRow * buttonOffsetY), 180, 15);
            Rect buttonPlatformRect = new Rect(platformStartX + (currentCol * buttonPlatformOffsetX), startY + (currentRow * buttonPlatformOffsetY), 200, 75);

            GUI.Box(breakRect, "", Window.buttonStyle);
            GUI.Box(buttonPlatformRect, "", Window.buttonPlatformStyle);
        }

        void Update()
        {
            HandleEnabledButtons();
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

        private static void CreateButton(ButtonInfo buttonInfo)
        {

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
                        //Library.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
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
                        //Library.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                        if (target.OnEnable != null)
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
                    //Library.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
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

            return null;
        }
    }
}
