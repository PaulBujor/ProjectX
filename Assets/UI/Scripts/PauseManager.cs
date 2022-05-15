using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsGamePaused;
    public static void PauseGame()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
        AudioListener.pause = true;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        AudioListener.pause = false;
    }
}
