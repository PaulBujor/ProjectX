using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerWrite : MonoBehaviour
{
    private const int TwoStarTime = 6*60;
    private const int ThreeStarTime = 3*60;

    private static readonly string LevelName = SceneManager.GetActiveScene().name.ToLower();

    public void StartLevel()
    {
        Debug.Log(LevelName);
        var startTime = DateTime.Now;
        PlayerPrefs.SetString(LevelName + "StartTime", startTime.ToString());
        PlayerPrefs.Save();
    }

    public void EndLevel(bool success)
    {
       
        var endTime = DateTime.Now;
        PlayerPrefs.SetString(LevelName + "EndTime", endTime.ToString());
        PlayerPrefs.Save();
        if (success)
        {
            CalculateScore();
        }
    }

    private void CalculateScore()
    {
        var startTime = DateTime.Parse(PlayerPrefs.GetString(LevelName + "StartTime"));
        var endTime = DateTime.Parse(PlayerPrefs.GetString(LevelName + "EndTime"));
        var finishTime = endTime - startTime;

        // More than 5 minutes -> 1 star
        if (finishTime.TotalSeconds > TwoStarTime)
        {
            PlayerPrefs.SetInt(LevelName + "Score", 1);
        }

        // More than 2 less than 5 minutes -> 2 stars
        if (finishTime.TotalSeconds > ThreeStarTime && finishTime.TotalSeconds <= TwoStarTime)
        {
            PlayerPrefs.SetInt(LevelName + "Score", 2);
        }

        // Less than 2 minutes -> 3 stars
        if (finishTime.TotalSeconds <= ThreeStarTime)
        {
            PlayerPrefs.SetInt(LevelName + "Score", 3);
        }

        PlayerPrefs.Save();
    }


}
