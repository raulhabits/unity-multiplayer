using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
  public Transform target;
  public Vector3 offset = new Vector3(0, 5, -10);
  public float smoothSpeed = 5f;

  void LateUpdate()
  {
    if (target == null) return;

    /*
        Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(target);
            */
      Vector3 newPos = new Vector3(target.position.x, offset.y, offset.z);

      transform.position = newPos;
    }
}