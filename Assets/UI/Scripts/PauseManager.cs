using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
///     Inspired by https://www.youtube.com/watch?v=JivuXdrIHK0
/// </remarks>
public class PauseManager : MonoBehaviour
{
    public static bool IsGamePaused = false;
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
