using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float lungeIntensity;
    [SerializeField] float lungeDelay = 1f;
    [SerializeField] float direction = -1f;
    [SerializeField] AudioClip attackTell;
    


    bool isLeaping = false;
    Animator myAnimator;
    AudioPlayer audioPlayer;
    Vector2 playerPosition;
    BoxCollider2D playerCheck;
    Rigidbody2D myRigidBody;


    private void Start() {
        playerCheck = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update() {
        Move();
    }

    //Look into adding wall movement later
    private void Move() {
        if(isLeaping) { return;}
        myRigidBody.velocity = new Vector2(Mathf.Sign(direction) * moveSpeed,  0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Player") {return;}
        if(isLeaping) {return;}
        playerPosition = other.transform.position;

        isLeaping = true;
        myAnimator.SetBool("isLeaping", true);

        audioPlayer.PlayAudioClip(attackTell, .4f);
        Invoke("Lunge", lungeDelay);
        Debug.Log("Leap at player");
        Invoke("LeapCooldown", lungeDelay*3);
    }



    private void Lunge() {
        float leapDirection = -Mathf.Sign(transform.position.x - playerPosition.x);

        //have lunge intensity be determined by the distance away from player
        Vector2 kickStrength = new Vector2(leapDirection* lungeIntensity*5, lungeIntensity*3);
        myRigidBody.AddForce(kickStrength);
    }

    private void LeapCooldown() {
        isLeaping = false;
        myAnimator.SetBool("isLeaping", false);
        Debug.Log("Can Leap again");
    }


}
