using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;
using Titled_PC_Template.Menu.Styles;
using UnityEngine;

namespace Titled_PC_Template.Menu.Widgets // TODO: set the slider to the correct hue when opening, and do the same for picker. add inputs for different values like hex, alpha, etc.
{
    internal class ColorPicker
    {
        private float Hue = 0f;
        private float lastHue = -1f;

        private float PickerDotX = 0f;
        private float PickerDotY = 0f;

        private int BackgroundWidth = 150;
        private int BackgroundHeight = 150;

        private int PickerDotWidth = 10;
        private int PickerDotHeight = 10;

        private string borderColor = "#44454F";

        private int PaletteSize = 120;

        private int SliderWidth = 12;
        private int SliderHeight = 120;
        
        private int HueSliderIndicatorWidth = 8; // more used as a offset but shh
        private int HueSliderIndicatorHeight = 10;

        private int Padding = 7;

        private Texture2D paletteTexture = null;
        private Texture2D hueSliderIndicatorTexture = null;
        private Texture2D hueSliderBackgroundTexture = null;
        private Texture2D pickerDotTexture = null;
        private Texture2D backgroundTexture = null;
        private Texture2D borderTexture = null;

        private bool isOpen = false;
        
        private Texture2D CreateHueSliderBackgroundTexture(int width, int height)
        {
            if (hueSliderBackgroundTexture != null)
                return hueSliderBackgroundTexture;

            hueSliderBackgroundTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            for (int y = 0; y < height; y++) {
                float hue = 1f - y / (float)(height - 1);

                Color color = Color.HSVToRGB(hue, 1f, 1f);

                for (int x  = 0; x < width; x++)
                    hueSliderBackgroundTexture.SetPixel(x, y, color);
            }
            hueSliderBackgroundTexture.Apply();

            return hueSliderBackgroundTexture;
        }

        private Texture2D CreatePaletteTexture(int width, int height, float hue)
        {
            if (paletteTexture != null && paletteTexture.width == width && paletteTexture.height == height && Mathf.Approximately(lastHue, hue))
                return paletteTexture;

            paletteTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            for (int y = 0; y < height; y++)
            {
                float value = y / (float)(height - 1);

                for (int x = 0; x < width; x++)
                {
                    float saturation = x / (float)(width - 1);
                    Color color = Color.HSVToRGB(hue, saturation, value);

                    paletteTexture.SetPixel(x, y, color);
                }
            }
            lastHue = hue; // so cache misses and recreates the texture with the new hue

            paletteTexture.Apply();
            return paletteTexture;
        }

        private Texture2D CreatePickerDot()
        {
            if (pickerDotTexture != null)
                return pickerDotTexture;

            pickerDotTexture = new Texture2D(PickerDotWidth, PickerDotHeight, TextureFormat.RGBA32, false);
            Color[] colors = new Color[PickerDotWidth * PickerDotHeight];

            float centerX = (PickerDotWidth - 1f)/ 2f;
            float centerY = (PickerDotHeight - 1f) / 2f;
            float radius = Mathf.Min(centerX, centerY);

            for (int x = 0; x < PickerDotWidth; x++)
            {
                for (int y = 0; y < PickerDotHeight; y++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                    float alpha = Mathf.Clamp01(1f - (distance - (radius - 1f)));
                    colors[y * PickerDotWidth + x] = new Color(1f, 1f, 1f, alpha);
                }
            }

            pickerDotTexture.SetPixels(colors);
            pickerDotTexture.Apply();
            return pickerDotTexture;
        }

        private Texture2D CreateHueSliderIndicator(int width, int height)
        {
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            Color[] colors = new Color[width * height];

            for (int i = 0; i < colors.Length; i++)
                colors[i] = Color.clear;

            int tipXLeft = 0;
            int tipXRight = width - 1;
            int midXLeft = width / 3;
            int midXRight = width - width / 3;
            int tipY = height / 2;

            for (int y = 0; y < height; y++)
            {
                float t = 1f - Mathf.Abs(y - tipY) / (float)tipY;
                int reachX = (int)(midXLeft * t);

                for (int x = tipXLeft; x < reachX; x++)
                    colors[y * width + x] = Color.white;
            }

            for (int y = 0; y < height; y++)
            {
                float t = 1f - Mathf.Abs(y - tipY) / (float)tipY;
                int reachX = (int)(midXRight + (tipXRight - midXRight) * (1f - t));

                for (int x = reachX; x <= tipXRight; x++)
                    colors[y * width + x] = Color.white;
            }

            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }

        public void CreateColorPicker(ButtonInfo mod, float platformStartX, float buttonPlatformOffsetX, float buttonPlatformOffsetY, float startY, float currentCol, float currentRow)
        {
            float platformX = platformStartX + (currentCol * buttonPlatformOffsetX);
            float platformY = startY + (currentRow * buttonPlatformOffsetY);

            Rect buttonRect = new Rect(platformX + 12, platformY + 40, 25, 25);
            Rect textRect = new Rect(platformX + 7, platformY + 5, 200, 200);
            Rect colorPickerContainerRect = new Rect(platformX + 10, platformY + 70, BackgroundWidth, BackgroundHeight);
            Rect paletteRect = new Rect(platformX + 10 + Padding, platformY + 70 + Padding, PaletteSize, PaletteSize);
            Rect hueSliderRect = new Rect(platformX + 10 + Padding + PaletteSize + 4, platformY + 70 + Padding, SliderWidth, SliderHeight);

            GUI.Label(textRect, mod.DisplayText);

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && buttonRect.Contains(Event.current.mousePosition)) // 0 is left mouse button
            {
                isOpen = !isOpen;
                if (isOpen)
                {
                    PickerDotX = paletteRect.x + paletteRect.width / 2f;
                    PickerDotY = paletteRect.y + paletteRect.height / 2f;
                }

                Event.current.Use(); // prevent spamming
            }

            DrawPreview(buttonRect, Hue);

            if (!isOpen)
                return;

            if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag) && paletteRect.Contains(Event.current.mousePosition))
            {
                PickerDotX = Event.current.mousePosition.x;
                PickerDotY = Event.current.mousePosition.y;
                mod.ColorPickerColor = GetSelectedColor(paletteRect);
                Event.current.Use();
            }

            if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag) && hueSliderRect.Contains(Event.current.mousePosition))
            {
                Hue = Mathf.Clamp01((Event.current.mousePosition.y - hueSliderRect.y) / hueSliderRect.height);
                mod.ColorPickerColor = GetSelectedColor(paletteRect);
                Event.current.Use();
            }

            DrawFullPalette(colorPickerContainerRect, paletteRect, Hue);
            DrawHueSliderBackground(hueSliderRect);

            DrawPickerDot();
            DrawHueSliderIndicator(hueSliderRect);
        }

        private Color GetSelectedColor(Rect paletteRect)
        {
            int x = (int)(PickerDotX - paletteRect.x);
            int y = (int)(paletteTexture.height - 1 - (PickerDotY - paletteRect.y));
            Color selectedColor = paletteTexture.GetPixel(x, y);

            return selectedColor;
        }

        private void DrawPickerDot()
        {
            Rect pickerRect = new Rect(PickerDotX - PickerDotWidth / 2f, PickerDotY - PickerDotHeight / 2f, PickerDotWidth, PickerDotHeight);
            GUI.DrawTexture(pickerRect, CreatePickerDot());
        }

        private void DrawFullPalette(Rect colorPickerContainerRect, Rect paletteRect, float hue)
        {
            if (backgroundTexture == null)
                backgroundTexture = Styles.Helpers.CreateTexture(BackgroundWidth, BackgroundHeight, Helpers.CreateColorFromHex(Window.windowBackgroundColor), "fullpalette-normal-background");

            if (borderTexture == null)
                borderTexture = Styles.Helpers.CreateTexture(BackgroundWidth, BackgroundHeight, Helpers.CreateColorFromHex(borderColor), "fullpalette-border");

            GUIStyle backgroundStyle = new GUIStyle();
            backgroundStyle.normal.background = backgroundTexture;

            GUIStyle borderStyle = new GUIStyle();
            borderStyle.normal.background = borderTexture;

            float borderSize = 4f;
            Rect borderRect = new Rect(colorPickerContainerRect.x - 2, colorPickerContainerRect.y - 2, colorPickerContainerRect.width + borderSize, colorPickerContainerRect.height + borderSize);
            
            GUI.Box(borderRect, "", borderStyle);
            GUI.Box(colorPickerContainerRect, "", backgroundStyle);
            GUI.DrawTexture(paletteRect, CreatePaletteTexture(PaletteSize, PaletteSize, hue)); // this is the full size palette
        }

        private void DrawHueSliderIndicator(Rect hueSliderRect)
        {
            float indicatorWidth = hueSliderRect.width + HueSliderIndicatorWidth;

            float y = hueSliderRect.y + Hue * hueSliderRect.height - HueSliderIndicatorHeight / 2f;
            float x = hueSliderRect.x - 4;

            Rect indicatorRect = new Rect(x, y, indicatorWidth, HueSliderIndicatorHeight);
            GUI.DrawTexture(indicatorRect, CreateHueSliderIndicator((int)indicatorWidth, HueSliderIndicatorHeight));
        }
        private void DrawHueSliderBackground(Rect hueSliderRect)
        {
            GUI.DrawTexture(hueSliderRect, CreateHueSliderBackgroundTexture(SliderWidth, SliderHeight));
        }

        private void DrawPreview(Rect buttonRect, float Hue)
        {
            GUI.DrawTexture(buttonRect, CreatePaletteTexture(25, 25, Hue)); // this is the preview
        }
    }
}
