using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerStatusItem : MonoBehaviour
{


    [SerializeField] public TMP_Text playerName;
    [SerializeField] public TMP_Text playerStatus;
    
    public Color[] rowColors;
    
    public void SetPlayerName(string playerName)
    {
        this.playerName.text = playerName;
    }
    public void IsReady(bool isReady)
    {
        this.playerStatus.text = isReady ? "Ready" : "Not Ready";
        this.playerStatus.color = isReady ? Color.green : Color.red;
    }


}
