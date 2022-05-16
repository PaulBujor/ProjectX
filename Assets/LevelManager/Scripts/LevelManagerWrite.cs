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

        private const int TwoStarTime = 6 * 60;
        private const int ThreeStarTime = 3 * 60;

        private static string LevelName;

        void Awake()
        {
            LevelName = SceneManager.GetActiveScene().name.ToLower();
        }
        
        public static void EndLevel(bool success)
        {
            if (success)
            {
                SaveScore();
                var score = CalculateScore();
                Debug.Log($"Obtained score: {score}");
                LevelCompletedHandler.HandleLevelCompleted(score);
            }
            else
            {
                DeathHandler.HandleDeath();
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
            var finishTime = LevelManagerTimer.GetTimeSinceStart().seconds;

            int score = 0;

            // More than 6 minutes -> 1 star
            if (finishTime > TwoStarTime)
            {
                score = 1;
            }

            // Between 3 and 6 minutes -> 2 stars
            if (finishTime > ThreeStarTime && finishTime <= TwoStarTime)
            {
                score = 2;
            }

            // Less than 3 minutes -> 3 stars
            if (finishTime <= ThreeStarTime)
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
