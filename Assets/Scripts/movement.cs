using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    [SerializeField] float speed = 14f;
    [SerializeField] ParticleSystem leftParticles, rightParticles;
    AudioClip[] tpSounds;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        tpSounds = new AudioClip[] {
            (AudioClip)Resources.Load("sounds/tpsounds/0"),
            (AudioClip)Resources.Load("sounds/tpsounds/1"),
            (AudioClip)Resources.Load("sounds/tpsounds/2"),
            (AudioClip)Resources.Load("sounds/tpsounds/3"),
            (AudioClip)Resources.Load("sounds/tpsounds/4"),
            (AudioClip)Resources.Load("sounds/tpsounds/5")
        };
    }


    void Update() {
        arrowKeyPressed();
    }

    private void arrowKeyPressed() {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.D)) {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            pos.x -= speed * Time.deltaTime;
        }

        //Bounds of arena between -10 and 10.
        if (pos.x < -10) {
            pos.x = 9.5f;
            teleportEffects(false);
        }

        if (pos.x > 10) {
            pos.x = -9.5f;
            teleportEffects(true);
        }

        transform.position = pos;


    }

    private void teleportEffects(bool isRight) {
        if (isRight) {
            audioSource.panStereo = 0.8f;
        } else {
            audioSource.panStereo = -0.8f;
        }

        leftParticles.Play();
        rightParticles.Play();
        int clip = Random.Range(0, tpSounds.Length);
        audioSource.PlayOneShot(tpSounds[clip]);
    }
}
