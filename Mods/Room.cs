using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titled_PC_Template.Mods
{
    internal class Room
    {
        public static void CreatePublicRoom(string code)
        {
            ExitGames.Client.Photon.Hashtable customRoomProperties = ((!(PhotonNetworkController.Instance.currentJoinTrigger.gameModeName != "city")) ? new ExitGames.Client.Photon.Hashtable {
        {
            "gameMode",
            PhotonNetworkController.Instance.currentJoinTrigger.gameModeName + GorillaComputer.instance.currentQueue + "CASUAL"
        } } : new ExitGames.Client.Photon.Hashtable {
        {
            "gameMode",
            PhotonNetworkController.Instance.currentJoinTrigger.gameModeName + GorillaComputer.instance.currentQueue + GorillaComputer.instance.currentGameMode
        } });
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
            PhotonNetworkController.Instance.currentJoinTrigger = PhotonNetworkController.Instance.privateTrigger;
            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable {
        {
            "gameMode",
            PhotonNetworkController.Instance.currentJoinTrigger.gameModeName + GorillaComputer.instance.currentQueue + GorillaComputer.instance.currentGameMode
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
