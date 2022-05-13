using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelManager
{
    /// <summary>
    ///     Inspired by https://www.red-gate.com/simple-talk/development/dotnet-development/saving-game-data-with-unity/
    /// </summary>
    public class LevelManagerWrite : MonoBehaviour
    {
        private LevelManagerRead _levelManagerRead;
        private LevelSwitcher _levelManagerSwitcher;

        private const int TwoStarTime = 6 * 60;
        private const int ThreeStarTime = 3 * 60;

        private string LevelName;

        void Awake()
        {
            LevelName = SceneManager.GetActiveScene().name.ToLower();
        }

        public void StartLevel()
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
                SaveScore();
                //Debug.Log($"Obtained score: {PlayerPrefs.GetInt(LevelName + "Score")}");
                Debug.Log($"Obtained score: {_levelManagerRead.GetScore(LevelName)}");
                _levelManagerSwitcher.ChangeLevelWithFade("LevelSelector");
            }
            else
            {
                _levelManagerSwitcher.ChangeLevelWithoutFade("MainMenu");
            }

        }

        private void SaveScore()
        {
            var score = CalculateScore();
            if (IsThisScoreHigher(score))
            {
                PlayerPrefs.SetInt(LevelName + "Score", score);
                PlayerPrefs.Save();
            }
        }


        private int CalculateScore()
        {
            var startTime = DateTime.Parse(PlayerPrefs.GetString(LevelName + "StartTime"));
            var endTime = DateTime.Parse(PlayerPrefs.GetString(LevelName + "EndTime"));
            var finishTime = endTime - startTime;

            int score = 0;

            // More than 5 minutes -> 1 star
            if (finishTime.TotalSeconds > TwoStarTime)
            {
                score = 1;
            }

            // More than 2 less than 5 minutes -> 2 stars
            if (finishTime.TotalSeconds > ThreeStarTime && finishTime.TotalSeconds <= TwoStarTime)
            {
                score = 2;
            }

            // Less than 2 minutes -> 3 stars
            if (finishTime.TotalSeconds <= ThreeStarTime)
            {
                score = 3;
            }

            return score;
        }

        private bool IsThisScoreHigher(int thisScore)
        {
            return thisScore > _levelManagerRead.GetScore(LevelName);
        }

    }
}
