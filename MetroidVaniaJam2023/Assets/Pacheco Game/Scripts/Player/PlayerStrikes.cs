using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikes : MonoBehaviour
{
    [SerializeField] List<GameObject> attacks = new List<GameObject>();
    [SerializeField] List<AudioClip> attackSounds = new List<AudioClip>();
    GameObject attackHitbox;
    Animator myAnimator;
    PlayerMovement player;
    AudioPlayer audioPlayer;
    int comboCount = 0;
    float lastClickTime = 0f;
    float comboWindow = 1f;

    //[SerializeField] List<GameObject> comboList = new List<GameObject>();


    private void Start() {
        myAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Update() {
        //Resets animation if nothing is done
        if(myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack1")) {
            myAnimator.SetBool("attack1", false);
            //audioPlayer.PlayAudioClip(attackSounds[0]);
        }
        if(myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2")) {
            myAnimator.SetBool("attack2", false);
            //audioPlayer.PlayAudioClip(attackSounds[1]);
        }
        if(myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack3")) {
            myAnimator.SetBool("attack3", false);
            //audioPlayer.PlayAudioClip(attackSounds[2]);
            comboCount = 0;
        }

        //Reset Clicks if combo dropped
        if(Time.time - lastClickTime > comboWindow) {
            comboCount = 0;
        }

    }


    private void OnAttack() {
        if(!player.GetHasControl()) {return;}
        if(comboCount >= 3) { return; }
        lastClickTime = Time.time;
        comboCount++;
        


        //Allow for queued attacks
        if(comboCount == 1) {
            myAnimator.SetBool("attack1", true);
            attackHitbox = Instantiate(attacks[0], transform.position, Quaternion.identity);
            attackHitbox.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1, 1);
        } else if(comboCount == 2) {
            StartCoroutine(QueueAttack(comboCount));
        } else if(comboCount == 3) {
            StartCoroutine(QueueAttack(comboCount));
        } else {
            return;
        }

    }


    private IEnumerator QueueAttack(int attack) {
        if(attack == 2) {
            yield return new WaitUntil( () => myAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack1") && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f == true );
            myAnimator.SetBool("attack1", false);
            myAnimator.SetBool("attack2", true);
            attackHitbox = Instantiate(attacks[1], transform.position, Quaternion.identity);
            attackHitbox.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1, 1);
        }
        if(attack == 3) {
            yield return new WaitUntil(() => myAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2") && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f == true);
            myAnimator.SetBool("attack2", false);
            myAnimator.SetBool("attack3", true);
            attackHitbox = Instantiate(attacks[2], transform.position, Quaternion.identity);
            attackHitbox.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1, 1);
        }
        yield return null;
    }
}
