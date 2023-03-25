using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    [Header("Player Injured Kick Amount")]
    [SerializeField] int horizontalKick = 2000;
    [SerializeField] int verticalKick = 1500;

    [Header("Player Sound Settings")]
    [SerializeField] AudioClip playerJump;
    [SerializeField] AudioClip playerLand;
    [SerializeField] List<AudioClip> playerwalk = new List<AudioClip>();

    float baseMoveSpeed;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Vector2 moveInput;
    Animator myAnimator;
    AudioPlayer audioPlayer;
    DreadPlayer dreadScript;
    public bool hasControl = true;
    bool stepSoundActive = false;
    float lastPlayTime;
    bool isDead = false;
    

    float startGravity;


    private void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        dreadScript = GetComponent<DreadPlayer>();
        //audioPlayer = FindObjectOfType<AudioPlayer>();
        audioPlayer = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
        startGravity = myRigidBody.gravityScale;
        baseMoveSpeed = moveSpeed;
    }


    private void FixedUpdate() {
        if(!hasControl) { return; }
        Move();
        FlipSprite();
        ManageDreadConsequences();

    }

    private void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value) {
        if(!hasControl) { return; }
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) ) {
            if(value.isPressed) {
                myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                audioPlayer.PlayCustomAudioClip(playerJump, gameObject.transform.position);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Ground") {
            audioPlayer.PlayCustomAudioClip(playerLand, gameObject.transform.position, .02f);
        }
    }




    void Move() {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        myAnimator.SetBool("isMoving", Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon);

        if(moveInput.x == 0) {return;}

        if(!stepSoundActive && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            lastPlayTime = Time.time;
            int stepSound = Random.Range(0, 3);
            audioPlayer.PlayCustomAudioClip(playerwalk[stepSound], gameObject.transform.position, .3f);
            stepSoundActive = true;
        } else if (Time.time - lastPlayTime > .3) {
            stepSoundActive = false;
        }
    
    }

    void FlipSprite() {
        bool checkDirection = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(checkDirection) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    public IEnumerator PlayerKick(Vector2 hazard) {
        Vector2 knockBackDirection = new Vector2(transform.position.x - hazard.x, 0);
        myAnimator.SetBool("isInjured", true);

        if(knockBackDirection.x > 0) {
            myRigidBody.velocity = new Vector2(horizontalKick, verticalKick);
        } else {
            myRigidBody.velocity = new Vector2(-horizontalKick, verticalKick);
        }
        hasControl = false;
        yield return new WaitForSeconds(1f);
        if(!isDead) {
            hasControl = true;
            myAnimator.SetBool("isInjured", false);
        }
    }


    public void PlayerDied() {
        isDead = true;
        hasControl = false;
        myAnimator.SetBool("isDead", true);
    }

    public bool GetIsDead() {
        return isDead;
    }

    public bool GetHasControl() {
        return hasControl;
    }

    public void SetHasControl(bool hasControlSetting) {
        hasControl = hasControlSetting;
    }

    public IEnumerator PlayerDash(float dashPower, float dashDuration) {
        hasControl = false;
        float playerGravity = myRigidBody.gravityScale;
        myRigidBody.gravityScale = 0f;
        myRigidBody.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        Debug.Log("Movement action begin");
        yield return new WaitForSeconds(dashDuration);
        myRigidBody.gravityScale = playerGravity;
        hasControl = true;
        myAnimator.SetBool("rapierAction", false);

    }

    private void ManageDreadConsequences() {
        if(dreadScript.GetDreadValue() < 25) {
            moveSpeed = baseMoveSpeed;
        } else if(dreadScript.GetDreadValue() < 50) {
            moveSpeed = baseMoveSpeed * .75f;
        } else if(dreadScript.GetDreadValue() < 75) {
            moveSpeed = baseMoveSpeed *.5f;
        } else if(dreadScript.GetDreadValue() == 100) {
            moveSpeed = 0;
        }
    }





}
