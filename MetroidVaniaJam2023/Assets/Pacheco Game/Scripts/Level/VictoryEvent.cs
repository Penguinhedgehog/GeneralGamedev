using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryEvent : MonoBehaviour
{
    //Temporary end Code
    SceneManagement sceneManagement;
    AudioPlayer audioPlayer;


    private void Awake() {
        sceneManagement = FindObjectOfType<SceneManagement>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Invoke("PlayerVictory", 6f);
    }

    private void PlayerVictory() {
        sceneManagement.VictoryUI();
        audioPlayer.PlayMusicTrack(0);
    }


}
