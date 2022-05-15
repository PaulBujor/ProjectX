using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseHandler : MonoBehaviour
{

    private OverlayConfiguration _overlay;


    void Awake()
    {
        _overlay = gameObject.GetComponent<OverlayConfiguration>();
    }

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
        _overlay.SetOverlayVisible(true);
    }

    public void Resume()
    {
        PauseManager.ResumeGame();
        _overlay.SetOverlayVisible(false);
    }

    //private string GetTimeSinceStart()
    //{
    //    var levelName = SceneManager.GetActiveScene().name.ToLower();
    //    var time = DateTime.Parse(PlayerPrefs.GetString(levelName + "StartTime"));
    //    var timeSinceStart = DateTime.Now - time;
    //    var timeSinceStartString = timeSinceStart.ToString("mm\\:ss");
    //    return timeSinceStartString;
    //}
}
