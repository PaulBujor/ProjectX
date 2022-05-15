using System.Collections;
using System.Collections.Generic;
using Assets.LevelManager;
using UnityEngine;

public class LevelCompletedHandler : MonoBehaviour
{
    private static OverlayConfiguration _overlay;

    void Awake()
    {
        _overlay = gameObject.GetComponent<OverlayConfiguration>();
    }


    public void RestartLevel()
    {
        PauseManager.ResumeGame();
        LevelSwitcher.ReloadCurrentScene();
    }

    public void GoHome()
    {
        PauseManager.ResumeGame();
        LevelSwitcher.ChangeLevelWithFade("MainMenu");
    }

    public static void HandleLevelCompleted()
    {
        PauseManager.PauseGame();
        _overlay.SetOverlayVisible(true);
    }
}
