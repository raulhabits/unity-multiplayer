using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

using TMPro;

public class ScoreManager : MonoBehaviourPun
{
    public TMP_Text p1ScoreText, p2ScoreText;

    private int p1Score = 0, p2Score = 0;

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

            if (playerId == 1) p1Score++;
            else p2Score++;

            p1ScoreText.text = p1Score.ToString();
            p2ScoreText.text = p2Score.ToString();
    }

}