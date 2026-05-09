using Photon.Pun;
using System.Collections.Generic;
using Titled_PC_Template.Extensions;
using UnityEngine;

namespace Titled_PC_Template.Mods
{
    internal class Visuals // this is gt spring cleaning specific
    {
        private static Dictionary<int, Material> originalMaterials = new Dictionary<int, Material>();
        private static Dictionary<int, Color> visualColors = new Dictionary<int, Color>();
        private static Shader visualsShader = null;
        public static void EnableChams()
        {
            var infectedColor = Main.GetIndex("Infected Color");
            var normalColor = Main.GetIndex("Normal Color");

            if (visualsShader == null)
                visualsShader = Shader.Find("GUI/Text Shader");

            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == null || vrrig.photonView.Owner == PhotonNetwork.LocalPlayer || vrrig.isMyPlayer)
                    continue;

                int actorNumber = vrrig.photonView.Owner.ActorNumber;

                if (!originalMaterials.ContainsKey(actorNumber))
                    originalMaterials.Add(actorNumber, vrrig.mainSkin.material);

                Color targetColor = vrrig.IsTagged() ? (infectedColor != null ? infectedColor.ColorPickerColor : Color.red) : (normalColor != null ? normalColor.ColorPickerColor : Color.green);
                if (vrrig.mainSkin.material.shader == visualsShader && visualColors.TryGetValue(actorNumber, out Color lastColor) && lastColor == targetColor)
                    continue;

                vrrig.mainSkin.material = new Material(visualsShader);
                vrrig.mainSkin.material.color = targetColor;
                visualColors[actorNumber] = targetColor;
            }
        }

        public static void DisableChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == null || vrrig.photonView.Owner == PhotonNetwork.LocalPlayer || vrrig.isMyPlayer || !originalMaterials.ContainsKey(vrrig.photonView.Owner.ActorNumber))
                    continue;

                int actorNumber = vrrig.photonView.Owner.ActorNumber;

                vrrig.mainSkin.material = originalMaterials[actorNumber];
                originalMaterials.Remove(actorNumber);
                visualColors.Remove(actorNumber);
            }
        }
    }
}
