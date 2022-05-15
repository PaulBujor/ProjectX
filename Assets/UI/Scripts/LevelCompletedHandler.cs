using System.Collections;
using System.Collections.Generic;
using Assets.LevelManager;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedHandler : MonoBehaviour
{
    private static OverlayConfiguration _overlay;

    public static bool IsGameFinished;

    void Awake()
    {
        _overlay = gameObject.GetComponent<OverlayConfiguration>();
        IsGameFinished = false;
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

    public static void HandleLevelCompleted(int score)
    {
        PauseManager.PauseGame();
        IsGameFinished = true;
        _overlay.SetOverlayVisible(true);
        DisplayStars(score);
    }

    private static void DisplayStars(int score)
    {
        for (int i = 1; i <= score; i++)
        {
            var star = GameObject.Find(GetStarObjectName(i));
            star.GetComponent<Image>().color = Color.white;
        }
    }

    private static string GetStarObjectName(int score)
    {
        switch (score)
        {
            case 1: return "StarLeftFilled";
            case 2: return "StarCenterFilled";
            case 3: return "StarRightFilled";
        }

        return string.Empty;
    }
}
