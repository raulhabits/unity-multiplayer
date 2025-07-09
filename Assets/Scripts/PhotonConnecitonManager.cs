using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnecitonManager : MonoBehaviourPunCallbacks
{
    void Start() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.LoadLevel("Lobby");
    }
}
