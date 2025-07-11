using Photon.Pun;
using Photon.Realtime;

using ExitGames.Client.Photon;

using UnityEngine;

using TMPro;

public class CountdownCallback : MonoBehaviourPunCallbacks
{
    public TMP_Text timerText;
    public float matchDuration = 300f; // 5 minutes in seconds

    private double startTime;
    private bool timerRunning = false;

    public GameObject gameOverPanel;

    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            double startTime = PhotonNetwork.Time;
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();
            props["matchStartTime"] = startTime;
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
    }

    void Update()
    {
        if (!timerRunning) return;

        double timeElapsed = PhotonNetwork.Time - startTime;
        float timeLeft = matchDuration - (float)timeElapsed;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            timerRunning = false;
            ShowGameOverPanel();
        }

        UpdateTimerUI(timeLeft);
    }

    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }



    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("matchStartTime"))
        {
            startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["matchStartTime"];
            timerRunning = true;
        }
    }
}
