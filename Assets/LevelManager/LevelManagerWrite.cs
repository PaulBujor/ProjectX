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

        private static string LevelName;

        //public static Action<bool> OnEndLevel;
        //public static Action OnStartLevel;

        void Awake()
        {
            LevelName = SceneManager.GetActiveScene().name.ToLower();
            //OnEndLevel += Test;
            //OnStartLevel += StartLevel;
        }

        private void Test(bool success)
        {
            Debug.Log("TWERKS");
        }

        public static void StartLevel()
        {
            var startTime = DateTime.Now;
            PlayerPrefs.SetString(LevelName + "StartTime", startTime.ToString());
            PlayerPrefs.Save();
            Debug.Log("START TIME IS: " + startTime);
        }

        public static void EndLevel(bool success)
        {
            var endTime = DateTime.Now;
            PlayerPrefs.SetString(LevelName + "EndTime", endTime.ToString());
            PlayerPrefs.Save();
            if (success)
            {
                SaveScore();
                //Debug.Log($"Obtained score: {PlayerPrefs.GetInt(LevelName + "Score")}");
                Debug.Log($"Obtained score: {LevelManagerRead.GetScore(LevelName)}");
                LevelSwitcher.ChangeLevelWithFade("LevelSelector");
            }
            else
            {
                LevelSwitcher.ChangeLevelWithoutFade("MainMenu");
            }

        }

        private static void SaveScore()
        {
            var score = CalculateScore();
            if (IsThisScoreHigher(score))
            {
                PlayerPrefs.SetInt(LevelName + "Score", score);
                PlayerPrefs.Save();
            }
        }


        private static int CalculateScore()
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

        private static bool IsThisScoreHigher(int thisScore)
        {
            return thisScore > LevelManagerRead.GetScore(LevelName);
        }

    }
}
