using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongTrigger : MonoBehaviour
{
    [SerializeField] int trackToPlay;
    [SerializeField] bool triggerOnce = false;
    [SerializeField] bool triggerAmbiance = false;

    AudioPlayer audioPlayer;
    BoxCollider2D boxCollider2D;

    private void Start() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            audioPlayer.PlayMusicTrack(trackToPlay);
            if(triggerOnce) {
                Destroy(gameObject);
            }
            if(triggerAmbiance) {
                audioPlayer.StartSewerAmbiance();
            }
        }
    }

}
