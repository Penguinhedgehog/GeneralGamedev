using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerTool : MonoBehaviour
{
    [SerializeField] float daggerSpeed = 10f;
    [SerializeField] AudioClip daggerImpactSound;
    PlayerMovement player;
    Rigidbody2D daggerRigidbody;
    BoxCollider2D boxCollider2D;
    AudioPlayer audioPlayer;
    float direction;
    

    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
        daggerRigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        //Change later to aim up based on another factor
        direction = player.transform.localScale.x * daggerSpeed;

    }

    private void FixedUpdate()
    {
        DaggerMove();
    }


    private void DaggerMove() {
        daggerRigidbody.velocity = new Vector2(direction, 0f);
        transform.localScale = new Vector2(Mathf.Sign(direction), 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground") {
            audioPlayer.PlayCustomAudioClip(daggerImpactSound, gameObject.transform.position, 1.2f);
            Destroy(gameObject);

        }
    }

}
