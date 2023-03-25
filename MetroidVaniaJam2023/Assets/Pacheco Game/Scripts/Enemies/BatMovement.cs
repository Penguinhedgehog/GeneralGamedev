using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float attackSpeed = 20f;
    [SerializeField] float attackDelay = 1f;
    [SerializeField][Range(-1,1)] float direction = 1;
    [SerializeField] AudioClip attackTell;

    float wobble = .2f;
    float x;
    bool isMoving = true;
    bool reachedTarget = false;

    Vector3 currentPos;
    Vector3 startingPos;
    Vector3 targetPos;
    Vector3 endPos;
    AudioPlayer audioPlayer;
    Animator myAnimator;

    
    BoxCollider2D playerCheck;
    Rigidbody2D myRigidBody;


    private void Start() {
        playerCheck = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        startingPos = gameObject.transform.position;
        audioPlayer = FindObjectOfType<AudioPlayer>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if(isMoving) {
            Move();
        } else {
            myAnimator.SetBool("isAttacking", true);
            StartCoroutine(BatLunge());
        }
    }

    private void Move() {
        if(!isMoving) {return;}
        x = Mathf.Repeat(Time.time, Mathf.PI*2);
        currentPos = gameObject.transform.position;
        gameObject.transform.position = new Vector2(currentPos.x, startingPos.y + (Mathf.Sin(x)/wobble ));
        myRigidBody.velocity = new Vector2(Mathf.Sign(direction) * moveSpeed, 0);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Player") {return;}
        if(!isMoving) {return;}
        audioPlayer.PlayAudioClip(attackTell, .6f);
        myAnimator.SetBool("attackTell", true);
        targetPos = other.transform.position;
        endPos = new Vector2(targetPos.x +  (targetPos.x - currentPos.x), currentPos.y);
        isMoving = false;
        myRigidBody.velocity = new Vector2(0,0);
    }


    //See if I can turn this into a curve/parabola later
    private IEnumerator BatLunge() {
        currentPos = gameObject.transform.position;

        if(reachedTarget) {
            MoveToTarget(endPos);
            if(currentPos == endPos) {
                isMoving = true;

                yield return new WaitForSeconds(attackDelay);
                reachedTarget = false;
                myAnimator.SetBool("attackTell", false);
                myAnimator.SetBool("isAttacking", false);
                Debug.Log("Finished");
                FlipEnemyFacing();
            }
        } else {
            MoveToTarget(targetPos);
            if(currentPos == targetPos) {
                reachedTarget = true;
                Debug.Log("Halfway there");
            }
        }

    }



    private void MoveToTarget(Vector2 targetPosition) {
        float delta = Time.deltaTime * attackSpeed;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
        
    }

    private void FlipEnemyFacing() {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        direction = Mathf.Sign(-direction);
    }


}
