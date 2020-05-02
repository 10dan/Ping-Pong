using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {
    MeshRenderer skin;
    BoxCollider box;
    float speedMod;
    void Start() {
        skin = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
        skin.enabled = false;
        box.enabled = false;
    }

    void Update() {
        //Rotate the object
        speedMod = ballmove.playerScore * 10;
        if (ballmove.playerScore >= 3) {
            skin.enabled = true;
            box.enabled = true;
        } else {
            skin.enabled = false;
            box.enabled = false;
        }
        if (ballmove.state == ballmove.State.Playing) {
            transform.Rotate(Vector3.forward * Time.deltaTime * speedMod);
        }
    }
}
