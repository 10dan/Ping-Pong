using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballmove : MonoBehaviour {

    AudioSource audioSource;
    Rigidbody rb;
    TrailRenderer trail;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem winParticle;
    [SerializeField] ParticleSystem loseParticle;
    float initSpeed = 15;
    float waitTime = 2f;
    public static int count = 0;
    public static int destroyed = 0;
    public static int playerScore = 0;
    public static int enemyScore = 0;

    private Vector3 lastFrameVelocity;
    public enum State {Playing, Waiting };
    public static State state = State.Playing;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        Invoke("initBallMovement", waitTime);

        //Loading the mp3s into the array. couldnt find a more elegant way to add them.
        hitSounds = new AudioClip[] {
            (AudioClip)Resources.Load("sounds/hits/1"),
            (AudioClip)Resources.Load("sounds/hits/2"),
            (AudioClip)Resources.Load("sounds/hits/3"),
            (AudioClip)Resources.Load("sounds/hits/4"),
            (AudioClip)Resources.Load("sounds/hits/5"),
            (AudioClip)Resources.Load("sounds/hits/6"),
            (AudioClip)Resources.Load("sounds/hits/7"),
            (AudioClip)Resources.Load("sounds/hits/8"),
            (AudioClip)Resources.Load("sounds/hits/9"),
            (AudioClip)Resources.Load("sounds/hits/10"),
            (AudioClip)Resources.Load("sounds/hits/11"),
            (AudioClip)Resources.Load("sounds/hits/12"),
            (AudioClip)Resources.Load("sounds/hits/13"),
            (AudioClip)Resources.Load("sounds/hits/14"),
            (AudioClip)Resources.Load("sounds/hits/15"),
            (AudioClip)Resources.Load("sounds/hits/16"),
            (AudioClip)Resources.Load("sounds/hits/17")
        };
    }


    private void initBallMovement() {
        state = State.Playing;
        transform.position = new Vector3(0, 15, 0);
        Vector3 initVel = new Vector3(Random.Range(-initSpeed, initSpeed), -initSpeed, 0);
        rb.velocity = initVel;
    }

    void Update() {
        //Save the velocity of ball each frame to help calculate bounces.
        lastFrameVelocity = rb.velocity;

        //Only remder trail if playing.
        if(state == State.Playing) {
            trail.enabled = true;
        } else {
            trail.enabled = false;
        }

        checkOffScreen();//Check if ball is off screen.
        checkMovement(); //Check ball is moving correct
    }

    //Triggered when the ball collides with an object.
    private void OnCollisionEnter(Collision collision) {
        if (state == State.Playing) {
            //Pick a random pop sound.
            int clip = Random.Range(0, hitSounds.Length);
            audioSource.PlayOneShot(hitSounds[clip]);

            //play the hit particle effect at the point of the collision.
            hitParticle.transform.position = transform.position;
            hitParticle.Play();

            //Send normal to bounce method.
            Bounce(collision.contacts[0].normal); 
        }
    }

    //Calculated velocity after bounce.
    private void Bounce(Vector3 collisionNormal) {
        float speed = lastFrameVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized,
            collisionNormal);
        rb.velocity = direction * Mathf.Max(speed, initSpeed);
    }
    //Check if the ball is no longer on the screen.
    private void checkOffScreen() {
        Vector3 pos = transform.position;
        if (pos.y > 40) { //Check if ball is past top bat.
            playerWonReset(true); //Player won this round == true
        } else if (pos.y < -10) {
            playerWonReset(false);
        }
    }

    private void playerWonReset(bool playerWon) {
        //set state to waiting for next round.
        state = State.Waiting;
        trail.enabled = false;
        if (playerWon) {
            playerScore++;
            winParticle.Play();
            audioSource.PlayOneShot(winSound);
        } else {
            playerScore--;
            loseParticle.Play();
            audioSource.PlayOneShot(loseSound);
        }
        //reset ball position and velocity.
        transform.position = new Vector3(0, 15, 0);
        rb.velocity = new Vector3(0, 0, 0);
        Invoke("initBallMovement", waitTime);
    }

    private void checkMovement() {
        //If for any reason the ball stops moving, reset.
        if(rb.velocity.magnitude < 0) {
            initBallMovement();
        }
        //If ball goes too slow.
        if (Mathf.Abs(rb.velocity.y) < initSpeed) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y*1.1f , 0);
        }
    }
}
