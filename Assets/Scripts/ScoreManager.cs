using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

using System.Collections;
using System.Collections.Generic;

using TMPro;

public class ScoreManager : MonoBehaviourPun
{
    public TMP_Text p1ScoreText, p2ScoreText;

    private int p1Score = 0, p2Score = 0;

    public GameObject goalPanel;

    public float goalPanelTimeout = 5f; 

    public void ScorePoint(int playerId)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("UpdateScore", RpcTarget.All, playerId);
        }
    }

    [PunRPC]
    void UpdateScore(int playerId)
    {
        StartCoroutine(ShowPanelForTime());

        if (playerId == 1) p1Score++;
        else p2Score++;

        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p2Score.ToString();
    }
    
    IEnumerator ShowPanelForTime()
    {
        goalPanel.SetActive(true); // Show the panel
        yield return new WaitForSeconds(goalPanelTimeout); // Wait
        goalPanel.SetActive(false); // Hide the panel
    }

}