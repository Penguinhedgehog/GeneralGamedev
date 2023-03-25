using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] float transitionDuration = .5f;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Transform canvasPosition;


    PlayerMovement player;
    PlayerEyeCatchers eyeCatchers;
    AudioPlayer audioPlayer;
    GameObject menuUI;


    private void Awake() {
        player = FindObjectOfType<PlayerMovement>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        eyeCatchers = FindObjectOfType<PlayerEyeCatchers>();
    }


    private void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            if(Time.timeScale > 0.1f) {
                PauseMenu();
            } else {
                ResumeGame();
            }
        }
    }

    private void PauseMenu() {
        StartCoroutine(TransitionCamera(30f, 3f, true));

        audioPlayer.PauseMusic();
        player.SetHasControl(false);
        Time.timeScale = 0f;
        Debug.Log("Timescale is: " + Time.timeScale);
    }

    private IEnumerator TransitionCamera(float start, float end, bool menu) {
        float currentTime = 0;

        while(currentTime < transitionDuration) {
            currentTime += Time.unscaledDeltaTime;
            playerCamera.m_Lens.OrthographicSize = Mathf.Lerp(start, end, currentTime/transitionDuration);

            yield return null;
        }

        if(menu) {
            //pauseMenuUI.SetActive(true);

            menuUI = Instantiate(pauseMenuUI, canvasPosition);
            eyeCatchers.ActivateFrame();
        }

        yield break;
    }

    private void ResumeGame() {
        //pauseMenuUI.SetActive(false);
        Destroy(menuUI);
        eyeCatchers.DeactivateFrame();
        StartCoroutine(TransitionCamera(3f, 30f, false));

        audioPlayer.PlayMusic();
        player.SetHasControl(true);
        Time.timeScale = 1f;
    }

    

}
