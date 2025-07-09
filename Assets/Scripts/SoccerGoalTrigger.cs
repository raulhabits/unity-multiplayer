using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerGoalTrigger : MonoBehaviourPun
{
    public int goalTeamScorer;
    public ScoreManager scoreManager;
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameManager.ResetBall(collision.gameObject);
            scoreManager.ScorePoint(goalTeamScorer);
        }
    }
}
