using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;

namespace Titled_PC_Template.Mods
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = {
            // tabs [0]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Settings", IsTab = true, TabToChangeTo = 1 },
                new ButtonInfo{ DisplayText = "Room", IsTab = true, TabToChangeTo = 3 }
            },
            // settings [1]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Menu Settings", IsTab = true, TabToChangeTo = 2 }
            },
            // menu settings [2]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Example Setting", IsToggle = true }
            },
            // room mods [3]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Disconnect", IsToggle = false, Method = () => PhotonNetwork.Disconnect() },
                new ButtonInfo{ DisplayText = "Join Random", IsToggle = false, Method = () => PhotonNetwork.JoinRandomRoom() },
                new ButtonInfo{ DisplayText = "Room Code", IsInput = true, },
                new ButtonInfo{ DisplayText = "Create Private", IsToggle = false, Method = () => Room.CreatePrivateRoom(Main.GetIndex("Private Code").InputValue) },
                new ButtonInfo{ DisplayText = "Create Public", IsToggle = false, Method = () => Room.CreatePublicRoom(Main.GetIndex("Private Code").InputValue) },

            },
        };
    }
}
