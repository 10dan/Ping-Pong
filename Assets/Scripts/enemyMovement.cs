using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {
    private float moveSpeed = 0.01f;
    void Start() {

    }

    void Update() {
        GameObject ball = GameObject.Find("playball");
        Transform ballTransform = ball.GetComponent<Transform>();
        float thisx = transform.position.x;
        //Get balls coordinates.
        float x = ballTransform.position.x;
        float y = ballTransform.position.y;

        //Dont move if on track to hit ball.
        if (x == thisx) { return; }

        //Calculate movespeed by looking at level (player score)
        float cMove = moveSpeed * ballmove.playerScore / 2;

        //how far away can enamy respond to ball movement.
        float distanceThresh = 20 - ballmove.playerScore / 2;

        if (y < distanceThresh) { //If ball is far away, do nothing.
            return;
        } else {//Ball close to bat.
            if (x < thisx) {
                transform.position -= new Vector3(cMove, 0, 0);
            } else {
                transform.position += new Vector3(cMove, 0, 0);
            }
        }
    }
}
