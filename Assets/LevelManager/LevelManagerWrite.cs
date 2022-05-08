using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerWrite : MonoBehaviour
{

    private const int TwoStarTime = 6*60;
    private const int ThreeStarTime = 3*60;

    public void StartTimer(string level)
    {
        var startTime = DateTime.Now;
        PlayerPrefs.SetString(level.ToLower() + "StartTime", startTime.ToString());
        PlayerPrefs.Save();
    }

    public void EndTimer(string level)
    {
        var endTime = DateTime.Now;
        PlayerPrefs.SetString(level.ToLower() + "EndTime", endTime.ToString());
        PlayerPrefs.Save();
        CalculateScore(level.ToLower());
    }

    private void CalculateScore(string level)
    {
        var startTime = DateTime.Parse(PlayerPrefs.GetString(level + "StartTime"));
        var endTime = DateTime.Parse(PlayerPrefs.GetString(level + "EndTime"));
        var finishTime = endTime - startTime;

        // More than 5 minutes -> 1 star
        if (finishTime.TotalSeconds > TwoStarTime)
        {
            PlayerPrefs.SetInt(level + "Score", 1);
        }

        // More than 2 less than 5 minutes -> 2 stars
        if (finishTime.TotalSeconds > ThreeStarTime && finishTime.TotalSeconds <= TwoStarTime)
        {
            PlayerPrefs.SetInt(level + "Score", 2);
        }

        // Less than 2 minutes -> 3 stars
        if (finishTime.TotalSeconds <= ThreeStarTime)
        {
            PlayerPrefs.SetInt(level + "Score", 3);
        }

        PlayerPrefs.Save();
    }


}
