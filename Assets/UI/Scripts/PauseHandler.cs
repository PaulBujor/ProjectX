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
public class PauseHandler : MonoBehaviour
{
    [Tooltip("The object whose visibility will be turned on/off")]
    [SerializeField]
    private GameObject _overlay;
    [SerializeField]
    private Text _timeValue;

    public void OnEscape(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (PauseManager.IsGamePaused)
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
        PauseManager.PauseGame();
        _overlay.SetActive(true);
        _timeValue.text = GetTimeSinceStart();
    }

    public void Resume()
    {
        PauseManager.ResumeGame();
        _overlay.SetActive(false);
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
