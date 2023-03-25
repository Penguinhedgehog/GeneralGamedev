using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Sprite offSprite;
    [SerializeField] Sprite onSprite;
    [SerializeField] bool oneWaySwitch = false;
    [SerializeField] bool switchValue = false;
    //[SerializeField] bool autoTrigger = false;

    [Header("Timer Options")]
    [SerializeField] float timeDuration;

    [Tooltip("This list will be signaled to do something when triggered (good for objects already on screen)")]
    [SerializeField] List<GameObject> linkedObjects = new List<GameObject>();
    [Tooltip("This list will activate/deactivate objects (good for not on screen objects)")]
    [SerializeField] List<GameObject> activatableObjects = new List<GameObject>();
    
    AudioPlayer audioPlayer;
    [SerializeField] bool isButton = false;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        //if autotrigger false
        if(oneWaySwitch == true && switchValue == true) {return;}
        if(other.tag == "Player" || other.tag == "Dagger") {
            StartCoroutine(SwitchPressed());
        }
    }

    //Work on creating a player prompt for this 
    private void OnTriggerStay2D(Collider2D other) {
        //Move On trigger Enter code into here
        // if(other.tag == "Player" &&)
        //Event manager UI
    }


    private void ActivateSwitch() {
        switchValue = !switchValue;
        SpriteRenderer activeSprite = gameObject.GetComponent<SpriteRenderer>();

        if(!switchValue) {
            activeSprite.sprite = offSprite;
        } else {
            activeSprite.sprite = onSprite;
        }

        foreach(GameObject linkedObject in linkedObjects) {
            MovingPlatforms platforms = linkedObject.GetComponent<MovingPlatforms>();
            Spawner spawn = linkedObject.GetComponent<Spawner>();

            if(platforms != null) {
                platforms.SwitchActive();
                Debug.Log("Platforms Activated");
            }
            if (spawn != null) {
                spawn.SwitchActive();
                Debug.Log("Spawners activated");
            }
        }

        //This activates the object if it is not currently active in the scene
        foreach(GameObject activatableObject in activatableObjects) {
            activatableObject.SetActive(!activatableObject.activeSelf);
        }
    }


    private IEnumerator SwitchPressed() {
        ActivateSwitch();
        if(isButton) {
            audioPlayer.WorldButtonPress();
        } else {
            audioPlayer.WorldLeverSwitch();
        }

        yield return new WaitForSeconds(timeDuration);

        if(timeDuration > 0.2) {
            ActivateSwitch();
        }
    }

}
