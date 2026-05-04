using BepInEx;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace Titled_PC_Template.Notifications
{
    [BepInPlugin("com.titled.notifications", "library", "1.0.0")]
    public class Library : BaseUnityPlugin
    {
        private void Start()
        {
            base.Logger.LogInfo("Plugin NotificationLibrary is loaded!");
            Initialize();
        }

        public static void Initialize()
        {
            if (Camera.main == null)
            {
                Debug.Log("Main camera is null.");
                return;
            }

            MainCamera = Camera.main.gameObject;
            NotificationChild = new GameObject("notification-child");
            NotificationChild.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 1f, MainCamera.transform.position.z - 20f);

            NotificationContainer = new GameObject("notification-container");
            NotificationContainer.AddComponent<Canvas>();
            NotificationContainer.AddComponent<GraphicRaycaster>();
            NotificationContainer.GetComponent<Canvas>().enabled = true;
            NotificationContainer.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            NotificationContainer.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
            NotificationContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
            NotificationContainer.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 1f, MainCamera.transform.position.z - 20f);
            NotificationContainer.transform.parent = NotificationChild.transform;
            NotificationContainer.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            Vector3 eulerAngles = NotificationContainer.GetComponent<RectTransform>().rotation.eulerAngles;
            eulerAngles.y = -270f;
            NotificationContainer.transform.localScale = new Vector3(1f, 1f, 1f);
            NotificationContainer.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);

            CanvasScaler canvasScaler = NotificationContainer.AddComponent<CanvasScaler>();
            canvasScaler.dynamicPixelsPerUnit = 1f;
            canvasScaler.referencePixelsPerUnit = 2500f;
            canvasScaler.scaleFactor = 1f;
            NotificationText = new GameObject
            {
                transform =
                {
                    parent = NotificationContainer.transform
                }
            }.AddComponent<UnityEngine.UI.Text>();

            NotificationText.text = "";
            NotificationText.fontSize = FontSize;
            NotificationText.font = NotificationFont;
            NotificationText.rectTransform.sizeDelta = new Vector2(700f, 400f);
            NotificationText.alignment = TextAnchor.LowerLeft;
            NotificationText.resizeTextForBestFit = false;
            NotificationText.rectTransform.localScale = new Vector3(0.0033f, 0.0033f, 0.33f);
            NotificationText.rectTransform.localPosition = new Vector3(0f, -1f, -1f);
            NotificationText.material = new Material(Shader.Find("GUI/Text Shader"));
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

                NotificationText.text = sb.ToString();
            }
            else
                NotificationText.text = "";
        }
        public static void SendNotification(string text)
        {
            if (!IsEnabled)
                return;

            float displayTime = Mathf.Max(2.0f, text.Length * TimeOffsetPerCharacter);

            notifications.Add(new Notification
            {
                Content = text,
                ExpireTime = Time.time + displayTime
            });

            while (notifications.Count > NotificationThreshold)
            {
                notifications.RemoveAt(0);
            }
        }
      
        public static void ClearAllNotifications()
        {
            notifications.Clear();
            Library.NotificationText.text = "";
        }

        private static GameObject NotificationContainer;
        private static GameObject NotificationChild;
        private static GameObject MainCamera;
        public static Font NotificationFont = Font.CreateDynamicFontFromOSFont("Agency FB", 24) ?? Resources.GetBuiltinResource<Font>("Arial.ttf");
        private static Text NotificationText;
        private static readonly List<Notification> notifications = new List<Notification>();
        public static int NotificationThreshold = 30;
        public static int FontSize = 18;
        public static bool IsEnabled = true;
        public static float TimeOffsetPerCharacter = 0.05f;
    }
}