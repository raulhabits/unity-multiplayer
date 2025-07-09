using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] public TMP_Text roomNameText;
    private string roomName;

    public Color[] rowColors;

    public void SetRoomInfo(RoomInfo info, int index)
    {
        roomName = info.Name;
        roomNameText.text = $"{info.Name} ({info.PlayerCount}/{info.MaxPlayers})";

        Transform child = transform.Find("LayoutElement");
        Image img = child.GetComponent<Image>();
        img.color = rowColors[index % rowColors.Length];
    }

    public void OnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}