using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

using TMPro;

using System.Collections;
using System.Collections.Generic;

public class LobbyHandler : MonoBehaviourPunCallbacks
{


    public TMP_Text playerNameText;
    public TMP_InputField newRoomName;

    [SerializeField] private Transform roomListParent;
    [SerializeField] private GameObject roomListItemPrefab;

    private Dictionary<string, GameObject> roomListItems = new Dictionary<string, GameObject>();

    public void Start()
    {
        playerNameText.text = PhotonNetwork.NickName;
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(newRoomName.text, options);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int index = 0;
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                if (roomListItems.ContainsKey(info.Name))
                {
                    Destroy(roomListItems[info.Name]);
                    roomListItems.Remove(info.Name);
                }
            }
            else
            {
                if (roomListItems.ContainsKey(info.Name))
                {
                    // Update existing item if needed
                    RoomListItem item = roomListItems[info.Name].GetComponent<RoomListItem>();
                    item.SetRoomInfo(info, index);
                }
                else
                {
                    GameObject newItem = Instantiate(roomListItemPrefab, roomListParent);
                    newItem.GetComponent<RoomListItem>().SetRoomInfo(info, index);
                    roomListItems[info.Name] = newItem;
                }

                index++;
            }
        }
    }


    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Room");
    }
}