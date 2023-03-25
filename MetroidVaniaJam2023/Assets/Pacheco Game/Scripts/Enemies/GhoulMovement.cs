using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] AudioClip ghoulAggro;
    
    Rigidbody2D myRigidBody2D;
    AudioPlayer audioPlayer;
    bool hasAggro = false;


    private void Start() {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Player") {return;}
        if(!hasAggro) {
            audioPlayer.PlayAudioClip(ghoulAggro, .7f);
            hasAggro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag != "Player") { return;}
        hasAggro = false;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag != "Player") { return; }
        Vector3 playerPosition = other.transform.position;
        

        if(playerPosition.x >= gameObject.transform.position.x)  {
            MoveRight();
        } else {
            MoveLeft();
        }
    }

    private void MoveLeft() {
        myRigidBody2D.velocity = new Vector2(-moveSpeed, myRigidBody2D.velocity.y);
    }

    private void MoveRight() {
        myRigidBody2D.velocity = new Vector2(moveSpeed, myRigidBody2D.velocity.y);
    }

    private void FlipEnemyFacicng() {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody2D.velocity.x)), 1f);
    }
    


}
