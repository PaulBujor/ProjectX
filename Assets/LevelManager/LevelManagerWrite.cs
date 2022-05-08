using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelManager
{
    public class LevelManagerWrite : MonoBehaviour
    {
        private const int TwoStarTime = 6*60;
        private const int ThreeStarTime = 3*60;
    
        private string LevelName;
        void Start()
        {
            LevelName = SceneManager.GetActiveScene().name.ToLower();
            StartLevel();
        }

        private void StartLevel()
        {
            var startTime = DateTime.Now;
            PlayerPrefs.SetString(LevelName + "StartTime", startTime.ToString());
            PlayerPrefs.Save();
            Debug.Log("START TIME IS: " + startTime);
        }

        public void EndLevel(bool success)
        {
       
            var endTime = DateTime.Now;
            PlayerPrefs.SetString(LevelName + "EndTime", endTime.ToString());
            PlayerPrefs.Save();
            if (success)
            {
                CalculateScore();
                Debug.Log($"Obtained score: {PlayerPrefs.GetInt(LevelName+"Score")}");
            }

            FindObjectOfType<LevelSwitcher>().ChangeLevelWithFade("LevelSelector");
        }

        private void CalculateScore()
        {
            var startTime = DateTime.Parse(PlayerPrefs.GetString(LevelName + "StartTime"));
            var endTime = DateTime.Parse(PlayerPrefs.GetString(LevelName + "EndTime"));
            Debug.Log($"CALCULATING START TIME: {startTime}");
            Debug.Log($"CALCULATING END TIME: {endTime}");
            var finishTime = endTime - startTime;
            Debug.Log($"CALCULATING FINISH TIME: {finishTime}");
            Debug.Log($"CALCULATING FINISH TIME SECONDS: {finishTime.TotalSeconds}");

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
}
