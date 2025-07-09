using Photon.Pun;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using UnityEngine.SceneManagement;

public class PlayerNameHandler : MonoBehaviour
{
    public TMP_InputField playerName;

    
    const string key = "PlayerName";
    void Start()
    {
        if (PlayerPrefs.HasKey(key))
        {
            playerName.text = PlayerPrefs.GetString(key);
        }
        PhotonNetwork.NickName = playerName.text;
    }
    public void Join()
    {
        if (string.IsNullOrEmpty(playerName.text))
        {
            return;
        }
        PhotonNetwork.NickName = playerName.text;
        PlayerPrefs.SetString(key, playerName.text);

        SceneManager.LoadScene("TransitionToLobbyScene");
    }
}