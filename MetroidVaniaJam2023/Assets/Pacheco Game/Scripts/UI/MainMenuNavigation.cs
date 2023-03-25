using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject musicCheckPanel;
    [SerializeField] GameObject optionsPanel;

    GameObject currentActivePanel;

    private void Awake() {
        currentActivePanel = mainMenuPanel;
    }

    public void ActivateMainMenu() {
        currentActivePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        currentActivePanel = mainMenuPanel;
    }

    public void ActivateCreditsPanel() {
        currentActivePanel.SetActive(false);
        creditsPanel.SetActive(true);
        currentActivePanel = creditsPanel;
    }

    public void ActivateMusicCheckPanel() {
        currentActivePanel.SetActive(false);
        musicCheckPanel.SetActive(true);
        currentActivePanel = musicCheckPanel;
    }

    public void ActivateOptionsPanel() {
        currentActivePanel.SetActive(false);
        optionsPanel.SetActive(true);
        currentActivePanel = optionsPanel;
    }


}
