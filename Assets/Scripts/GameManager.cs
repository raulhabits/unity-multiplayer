using Photon.Pun;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public GameObject ballPrefab;
    public GameObject aiPrefab;
    public Transform ballSpawn;

    public GameObject scoreManager;

    void Start()
    {
        // Spawn player
        int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[index].position, spawnPoints[index].rotation);

        // Spawn ball (only once by Master Client)
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(WaitForOpponent());
            var direction = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
            CreateBall(direction.x, direction.y, direction.z);
        }
    }

    IEnumerator WaitForOpponent()
    {
        yield return new WaitForSeconds(2f);
    }

    [PunRPC]
    public void CreateBall(float x, float y, float z)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var direction = new Vector3(x, y, z);
            var ballPosition = ballSpawn.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            PhotonNetwork.Instantiate(ballPrefab.name, ballPosition, targetRotation);
        }
    }

    public void ResetBall(GameObject ball)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(ball);
            var direction = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
            
            CreateBall(direction.x, direction.y, direction.z);
        }

    }



}
