using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool IsGamePaused;
    public GameObject Overlay;
    public Text TimeValue;

    public void OnEscape(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Overlay.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;

        TimeValue.text = GetTimeSinceStart();
    }

    public void Resume()
    {
        Overlay.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    private string GetTimeSinceStart()
    {
        var levelName = SceneManager.GetActiveScene().name.ToLower();
        var time = DateTime.Parse(PlayerPrefs.GetString(levelName + "StartTime"));
        var timeSinceStart = DateTime.Now - time;
        var timeSinceStartString = timeSinceStart.ToString("mm\\:ss");
        return timeSinceStartString;
    }
}
