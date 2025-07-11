using System.Collections;

using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameOverEventHandler : MonoBehaviourPunCallbacks
{
    public string onExitCurrentRoomTarget;


    public void ExitCurrentRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
        

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(onExitCurrentRoomTarget);
    }
}
