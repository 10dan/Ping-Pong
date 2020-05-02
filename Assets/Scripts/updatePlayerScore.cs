using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class updatePlayerScore : MonoBehaviour {
    void Start() {

    }

    void Update() {
        TextMeshPro t = GetComponent<TextMeshPro>();
        int playerScore = ballmove.playerScore;
        t.text = "Level: " + playerScore.ToString();
    }
}
