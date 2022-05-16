using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTimer : MonoBehaviour
{
    private static float _timer;
    void Start()
    {
        _timer = 0;
    }

    void Update()
    {
        if (!PauseManager.IsGamePaused)
        {
            _timer += Time.deltaTime;
        }
    }

    public static (string time, float seconds) GetTimeSinceStart()
    {
        var seconds = TimeSpan.FromSeconds(_timer);
        var time = seconds.ToString("mm\\:ss");
        return (time, _timer);
    }
}
