using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Viveport;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

namespace Titled_PC_Template.Notifications
{
    [BepInPlugin("org.titled.notifications", "library", "1.0.0")]
    public class Library : BaseUnityPlugin
    {
        private void Start()
        {
            base.Logger.LogInfo("Plugin NotificationLibrary is loaded!");
            Initialize();
        }
        private class Notification
        {
            public string Content;
            public float ExpireTime;
        }
        private static readonly List<Notification> notifications = new List<Notification>();
        public static void Initialize()
        {
            if (Camera.main == null)
            {
                Debug.Log("Main camera is null.");
                return;
            }

            MainCamera = Camera.main.gameObject;
            NotificationChild = new GameObject("notification-child");

            NotificationContainer = new GameObject("notification-container");
            NotificationContainer.AddComponent<Canvas>();
            NotificationContainer.AddComponent<GraphicRaycaster>();
            NotificationContainer.GetComponent<Canvas>().enabled = true;
            NotificationContainer.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            NotificationContainer.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
            NotificationContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
            NotificationContainer.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 1f, MainCamera.transform.position.z - 20f);
            NotificationChild.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 1f, MainCamera.transform.position.z - 20f);
            NotificationContainer.transform.parent = NotificationChild.transform;
            NotificationContainer.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            Vector3 eulerAngles = NotificationContainer.GetComponent<RectTransform>().rotation.eulerAngles;
            eulerAngles.y = -270f;
            NotificationContainer.transform.localScale = new Vector3(1f, 1f, 1f);
            NotificationContainer.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);

            CanvasScaler cs = NotificationContainer.AddComponent<CanvasScaler>();
            cs.dynamicPixelsPerUnit = 1f;
            cs.referencePixelsPerUnit = 2500f;
            cs.scaleFactor = 1f;
            NotifiText = new GameObject
            {
                transform =
                {
                    parent = NotificationContainer.transform
                }
            }.AddComponent<UnityEngine.UI.Text>();

            NotifiText.text = "";
            NotifiText.fontSize = 18;
            NotifiText.font = AgencyFB;
            NotifiText.rectTransform.sizeDelta = new Vector2(700f, 400f);
            NotifiText.alignment = TextAnchor.LowerLeft;
            NotifiText.resizeTextForBestFit = false;
            NotifiText.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            NotifiText.rectTransform.localPosition = new Vector3(0f, -1f, -1f);
            NotifiText.material = new Material(Shader.Find("GUI/Text Shader"));
        }

        private void Update()
        {
            if (MainCamera == null)
                return;

            NotificationChild.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            NotificationChild.transform.rotation = MainCamera.transform.rotation;

            float currentTime = Time.time;
            if (notifications.Count > 0)
            {
                notifications.RemoveAll(n => currentTime >= n.ExpireTime);

                StringBuilder sb = new StringBuilder();
                foreach (Notification notification in notifications)
                {
                    sb.AppendLine(notification.Content);
                }

                NotifiText.text = sb.ToString();
            }
            else
                NotifiText.text = "";
            
        }
        public static void SendNotification(string text)
        {
            try
            {
                if (IsEnabled)
                {
                    float displayTime = Mathf.Max(2.0f, text.Length * 0.1f);

                    notifications.Add(new Notification
                    {
                        Content = text,
                        ExpireTime = Time.time + displayTime
                    });

                    while (notifications.Count > NoticationThreshold)
                    {
                        notifications.RemoveAt(0);
                    }
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"Notification failed: {ex.Message}");
            }
        }
      
        public static void ClearAllNotifications()
        {
            Library.NotifiText.text = "";
        }

        public static void ClearPastNotifications(int amount)
        {
            string text = "";
            foreach (string text2 in Enumerable.ToArray<string>(Enumerable.Skip<string>(Library.NotifiText.text.Split(Environment.NewLine.ToCharArray()), amount)))
            {
                if (text2 != "")
                {
                    text = text + text2 + "\n";
                }
            }
            NotifiText.text = text;
        }

        private static GameObject NotificationContainer;
        private static GameObject NotificationChild;
        private static GameObject MainCamera;
        public static int NoticationThreshold = 30;
        private string[] Notifilines;
        private string newtext;
        public static string PreviousNotifi;
        private static Text NotifiText;
        public static bool IsEnabled = true;
        public static Font AgencyFB = Font.CreateDynamicFontFromOSFont("Agency FB", 24);
    }
}