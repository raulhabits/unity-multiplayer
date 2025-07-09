using Photon.Pun;
using UnityEngine;

public class AbstractPlayerController : MonoBehaviourPun
{
    public float moveSpeed = 4f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (photonView.IsMine)
        {
            CameraFollower cam = Camera.main.GetComponent<CameraFollower>();
            if (cam != null)
            {
                cam.target = this.transform;
            }
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
    }
}