using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titled_PC_Template.Classes;
using UnityEngine;

namespace Titled_PC_Template.Mods
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = {
            // tabs [0]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Settings", IsTab = true, TabToChangeTo = 1, ToolTip = "Opened settings." },
                new ButtonInfo{ DisplayText = "Room", IsTab = true, TabToChangeTo = 3, ToolTip = "Opened room mods." },
                new ButtonInfo{ DisplayText = "Photon", IsTab = true, TabToChangeTo = 5, ToolTip = "Opened Photon mods." },
                new ButtonInfo{ DisplayText = "Other", IsTab = true, TabToChangeTo = 4, ToolTip = "Opened other mods." }
            },
            // settings [1]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Return to Main", IsTab = true, TabToChangeTo = 0, ToolTip = "Returned to main." },
                new ButtonInfo{ DisplayText = "Menu Settings", IsTab = true, TabToChangeTo = 2, ToolTip = "Opened menu settings." }
            },
            // menu settings [2]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Return to Settings", IsTab = true, TabToChangeTo = 1, ToolTip = "Returned to settings." },
                new ButtonInfo{ DisplayText = "Example Setting", IsButton = true, IsToggle = true }
            },
            // room mods [3]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Disconnect", IsButton = true, IsToggle = false, Method = () => PhotonNetwork.Disconnect(), ToolTip = "Disconnects you from the room." },
                new ButtonInfo{ DisplayText = "Join Random", IsButton = true, IsToggle = false, Method = () => PhotonNetwork.JoinRandomRoom(), ToolTip = "Joins random room." },
                new ButtonInfo{ DisplayText = "Room Code", IsInput = true, },
                new ButtonInfo{ DisplayText = "Gamemode", IsDropdown = true, Items = new string[] { "Casual", "Infection", "Hunt", "Battle" }, DropdownIndex = 0 },
                new ButtonInfo{ DisplayText = "Create Private", IsButton = true, IsToggle = false, Method = () => Room.CreatePrivateRoom(Main.GetIndex("Room Code").InputValue  ?? "titled-template"), ToolTip = "Creates a private room." },
                new ButtonInfo{ DisplayText = "Create Public", IsButton = true, IsToggle = false, Method = () => Room.CreatePublicRoom(Main.GetIndex("Room Code").InputValue ?? "titled-template"), ToolTip = "Creates a public room." },
            },
            // other [4]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Log All Id's", IsButton = true, IsToggle = false, Method = () => { foreach (Player player in PhotonNetwork.PlayerList) Debug.Log(player.NickName + " - " + player.UserId); } },
            },
            // Photon [5]
            new ButtonInfo[] {
                new ButtonInfo{ DisplayText = "Name", IsInput = true},
                new ButtonInfo{ DisplayText = "Set Name", IsButton = true, IsToggle = false, Method = () => PhotonNetwork.NickName = Main.GetIndex("Name").InputValue  ?? "example", ToolTip = "Sets your Photon name."}
            },
        };
    }
}
