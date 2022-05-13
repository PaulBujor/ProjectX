using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///     Inspired by https://www.youtube.com/watch?v=JivuXdrIHK0
/// </summary>
public class PauseManager : MonoBehaviour
{
    public static bool IsGamePaused;
    [Header("The object whose visibility will be turned on/off")]
    [SerializeField]
    private GameObject _overlay;
    [SerializeField]
    private Text _timeValue;

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
        _overlay.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        AudioListener.pause = true;
        _timeValue.text = GetTimeSinceStart();
    }

    public void Resume()
    {
        _overlay.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        AudioListener.pause = false;
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
