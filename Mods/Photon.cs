using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Mods
{
    internal class Photon
    {
        public static void ChangeName(string name)
        {
            PhotonNetwork.NickName = name;
            GorillaComputer.instance.currentName = name;
            GorillaComputer.instance.savedName = name;
            PlayerPrefs.SetString("playerName", name);
        }
    }
}
