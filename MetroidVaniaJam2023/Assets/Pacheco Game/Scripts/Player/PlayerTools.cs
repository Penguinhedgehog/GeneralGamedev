using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerTools : MonoBehaviour
{
    PlayerMovement player;
    bool toolCoolDown = false;

    [SerializeField] TextMeshProUGUI toolNotification;

    [Header("Torch Tool")]
    [SerializeField] GameObject torch;
    [SerializeField] Sprite torchIcon;
    [SerializeField] float torchCooldown = .5f;

    [Header("BoneArm Tool")]
    [SerializeField] GameObject  boneArm;
    [SerializeField] Sprite boneArmIcon;


    [Header("Rapier Tool")]
    [SerializeField] GameObject rapier;
    [SerializeField] Sprite rapierIcon;
    [SerializeField] float rapierCooldown = 2f;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = .3f;
    GameObject rapierBox;

    [Header("Dagger Tool")]
    [SerializeField] GameObject dagger;
    [SerializeField] Sprite daggerIcon;
    [SerializeField] float daggerCooldown = .5f;
    [SerializeField] AudioClip daggerThrowSound;

    List<GameObject> tools = new List<GameObject>();
    int activeTool = 0;
    Animator myAnimator;
    AudioPlayer audioPlayer;




    private void Awake() {
        player = GetComponent<PlayerMovement>();
        myAnimator = GetComponent<Animator>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        tools.Add(dagger);
        tools.Add(torch);
        //tools.Add(boneArm);
        tools.Add(rapier);
        rapierBox = gameObject.transform.GetChild(0).gameObject;
    }


    private void OnSwitchTool(InputValue value) {
        if(!player.GetHasControl()) {return;}
        var buttonValue = value.Get<float>();
        activeTool += ((int)buttonValue);

        if(activeTool < 0) {
            activeTool = (tools.Count - 1);
        } else if(activeTool >= tools.Count) {
            activeTool = 0;
        }

        toolNotification.text = tools[activeTool].name;
        
        Debug.Log("Active Tool: " + tools[activeTool].ToString());

    }
    
    
    //Edit later so that tools cannot be used when weapon is attacking
    private void OnUseTool() {
        if(!player.GetHasControl()) { return;}
        if(toolCoolDown) {return;}

        if(tools[activeTool] == dagger) {
            StartCoroutine(DaggerAction());
        } else if(tools[activeTool] == torch) {
            StartCoroutine(TorchAction());
        } else if(tools[activeTool] == rapier) {
            StartCoroutine(RapierAction());
        }
    } 

    private IEnumerator DaggerAction() {
        toolCoolDown = true;
        myAnimator.SetBool("throwDagger", true);
        yield return new WaitForSeconds(.2f); //Buffer
        audioPlayer.PlayCustomAudioClip(daggerThrowSound, gameObject.transform.position, .5f);
        Instantiate(dagger, gameObject.transform.position, Quaternion.identity);
        myAnimator.SetBool("throwDagger", false);
        yield return new WaitForSeconds(daggerCooldown);
        toolCoolDown = false;
    }

    private IEnumerator TorchAction() {

        toolCoolDown = true;
        myAnimator.SetBool("throwDagger", true);
        yield return new WaitForSeconds(.2f);
        Instantiate(torch, gameObject.transform.position, Quaternion.identity);
        myAnimator.SetBool("throwDagger", false);

        yield return new WaitForSeconds(torchCooldown);
        toolCoolDown = false;
    }

    private IEnumerator BoneArmAction() {
        //WIP
        yield return new WaitForSeconds(.2f);
    }

    private IEnumerator RapierAction() {
        toolCoolDown = true;
        Debug.Log("toolAction Begin");


        GameObject attackHitbox = Instantiate(rapier, gameObject.transform.position, Quaternion.identity);
        attackHitbox.transform.localScale = new Vector3(1 * gameObject.transform.localScale.x, 1, 1);
        myAnimator.SetBool("rapierAction", true);
        StartCoroutine(player.PlayerDash(dashSpeed, dashDuration));


        yield return new WaitForSeconds(rapierCooldown);

        toolCoolDown = false;
    }


    /* Debug.Log("Do Rapier action");
        toolCoolDown = true;
        myAnimator.SetBool("rapierAction", true);
        player.SetHasControl(false);
        //yield return new WaitForSeconds(.2f);


        Rigidbody2D playerRB = GetComponent<Rigidbody2D>();
        playerRB.velocity = new Vector2(30f, 10f);

        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(30f, 10f);

        rapier.SetActive(true);


        player.SetHasControl(true);
        myAnimator.SetBool("rapierAction", false);
        rapier.SetActive(false);

        yield return new WaitForSeconds(rapierCooldown);
        toolCoolDown = false; */



}
