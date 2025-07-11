using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using System.Collections.Generic;

using TMPro;

public class RoomHandler : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    public Transform playersListPanel;
    public GameObject playerEntryPrefab;
    public Button readyButton;
    public TMP_Text statusText;

    private Dictionary<int, GameObject> playerEntries = new Dictionary<int, GameObject>();

    private const float countdownDuration = 5f;
    private double countdownStartTime = -1;
    private bool countdownStarted = false;
    private string gameSceneName = "Fut5";

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            AddPlayerEntry(p);
            UpdatePlayerReadyStatus(p);
        }

        readyButton.onClick.AddListener(OnReadyClicked);

    }

    void Update()
    {
        if (countdownStarted && countdownStartTime > 0)
        {
            double timeRemaining = countdownStartTime + countdownDuration - PhotonNetwork.Time;

            if (timeRemaining > 0)
            {
                statusText.text = $"Game starting in {timeRemaining:F1} seconds...";
            }
            else
            {
                statusText.text = "Starting game...";
                countdownStarted = false;

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.LoadLevel($"{gameSceneName}Scene");
                }
            }
        }
    }

    void AddPlayerEntry(Player player)
    {
        GameObject entry = Instantiate(playerEntryPrefab, playersListPanel);
        entry.GetComponent<PlayerStatusItem>().SetPlayerName(player.NickName);
        entry.GetComponent<PlayerStatusItem>().IsReady(false);
        playerEntries[player.ActorNumber] = entry;
    }

    void UpdatePlayerReadyStatus(Player player)
    {
        if (playerEntries.TryGetValue(player.ActorNumber, out GameObject entry))
        {
            bool isReady = player.CustomProperties.ContainsKey("isReady") && (bool)player.CustomProperties["isReady"];
            entry.GetComponent<PlayerStatusItem>().IsReady(isReady);
        }
    }

    public void OnReadyClicked()
    {
        Hashtable props = new Hashtable { { "isReady", true } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        readyButton.interactable = false;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerEntry(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (playerEntries.ContainsKey(otherPlayer.ActorNumber))
        {
            Destroy(playerEntries[otherPlayer.ActorNumber]);
            playerEntries.Remove(otherPlayer.ActorNumber);
        }

        // Optional: reset countdown if someone leaves
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("isReady"))
        {
            UpdatePlayerReadyStatus(targetPlayer);
            CheckAllPlayersReady();
        }
    }

    void CheckAllPlayersReady()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (!p.CustomProperties.ContainsKey("isReady") || !(bool)p.CustomProperties["isReady"])
                return;
        }

        // All ready - start countdown if master
        if (PhotonNetwork.IsMasterClient && !PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("startTime"))
        {
            double startTime = PhotonNetwork.Time;
            Hashtable props = new Hashtable { { "startTime", startTime } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("startTime"))
        {
            countdownStartTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["startTime"];
            countdownStarted = true;
        }
    }

}