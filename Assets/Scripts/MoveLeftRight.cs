using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    float speed = .5f;
    float distance = 3;
    float startX;

    private void Start(){
        Vector2 startPosition = transform.position;
        transform.position = startPosition;
    }

    private void Update() {
        Vector2 newPosition = transform.position;
        newPosition.x = Mathf.SmoothStep(startX, startX+distance, Mathf.PingPong(Time.time* speed,1));
        transform.position = newPosition;
    }




}
