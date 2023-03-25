using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //Do we want to manage death by going to a new scene or showing a UI game over the scene?

    AudioPlayer audioPlayer;

    private void Start() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
        audioPlayer.EnableAudioLoop();
        audioPlayer.PlayMusicTrack(0);

    }

    public void LoadGame() {
        Debug.Log("WIP -Learn to manage with saves");
    }

    public void LoadTestScene() {
        SceneManager.LoadScene("Testing Scene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void GameOverUI() {
        SceneManager.LoadScene("GameOverScreen");
        audioPlayer.PlayerDeathSound();
        audioPlayer.DisableAudioLoop();
    }

    public void VictoryUI() {
        SceneManager.LoadScene("VictoryScreen");
    }
    


}
