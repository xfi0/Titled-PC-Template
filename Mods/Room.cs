using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Titled_PC_Template.Mods
{
    internal class Room
    {
        public static void CreatePublicRoom(string code)
        {
            var gamemodeButton = Main.GetIndex("Gamemode");
            if (gamemodeButton == null)
            {
                Debug.LogError("Gamemode button was not found.");
                return;
            }
            var currentJoinTrigger = PhotonNetworkController.Instance.currentJoinTrigger ?? GameObject.FindAnyObjectByType<GorillaNetworkJoinTrigger>();
            var currentTrigger = currentJoinTrigger.gameModeName + GorillaComputer.instance.currentQueue + gamemodeButton.Items[gamemodeButton.DropdownIndex].ToUpper() ?? currentJoinTrigger.gameModeName + GorillaComputer.instance.currentQueue + GorillaComputer.instance.currentGameMode; Debug.Log("currentTrigger: " + currentTrigger);

            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable {
        {
            "gameMode",
            currentTrigger
        } };

            RoomOptions roomOptions = new RoomOptions
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 10,
                CustomRoomProperties = customRoomProperties,
                PublishUserId = true,
                CustomRoomPropertiesForLobby = new string[1] { "gameMode" }
            };

            PhotonNetwork.CreateRoom(code, roomOptions);
        }

        public static void CreatePrivateRoom(string code)
        {
            var gamemodeButton = Main.GetIndex("Gamemode");
            if (gamemodeButton == null)
            {
                Debug.LogError("Gamemode button was not found.");
                return;
            }
            var currentJoinTrigger = PhotonNetworkController.Instance.currentJoinTrigger ?? GameObject.FindAnyObjectByType<GorillaNetworkJoinTrigger>();
            var currentTrigger = currentJoinTrigger.gameModeName + GorillaComputer.instance.currentQueue + gamemodeButton.Items[gamemodeButton.DropdownIndex].ToUpper() ?? currentJoinTrigger.gameModeName + GorillaComputer.instance.currentQueue + GorillaComputer.instance.currentGameMode;
            Debug.Log("currentTrigger: " + currentTrigger);
            PhotonNetworkController.Instance.currentJoinTrigger = PhotonNetworkController.Instance.privateTrigger;
            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable {
        {
            "gameMode",
            currentTrigger
        } };
            RoomOptions roomOptions = new RoomOptions
            {
                IsVisible = false,
                IsOpen = true,
                MaxPlayers = 10,
                CustomRoomProperties = customRoomProperties,
                PublishUserId = true,
                CustomRoomPropertiesForLobby = new string[1] { "gameMode" }
            };
            PhotonNetwork.CreateRoom(code, roomOptions);
        }
    }
}
