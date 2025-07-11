using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class OnSoccerBallOutOfBounds : MonoBehaviourPun
{
    public GameManager gameManager;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            gameManager.ResetBall(other.gameObject);
        }
    }
}
