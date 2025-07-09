using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponent : MonoBehaviour {
    public float speed = 3f;
    void Update(){
      GameObject ball = GameObject.FindWithTag("Ball");
      Vector3 dir = ball.transform.position;
      dir.y = transform.position.y;
      transform.position = Vector3.MoveTowards(transform.position, dir, speed * Time.deltaTime);
    }
}