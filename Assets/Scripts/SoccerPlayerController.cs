using Photon.Pun;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayerController : AbstractPlayerController
{
    public float kickForce = 10f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            PhotonView ballPV = collision.gameObject.GetComponent<PhotonView>();

            if (ballPV.IsMine == false)
            {
                // Request ownership if not already owned
                ballPV.RequestOwnership();
            }

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 kickDir = collision.contacts[0].point - transform.position;
            rb.AddForce(kickDir.normalized * kickForce, ForceMode.Impulse);
        }
    }
}
