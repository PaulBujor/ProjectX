using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelManager
{
    public class LevelSwitcher : MonoBehaviour
    {
        public static void ChangeLevelWithFade(string levelName)
        {
            SimpleSceneFader.ChangeSceneWithFade(levelName);
        }

        public static void ChangeLevelWithoutFade(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public static void ReloadCurrentScene()
        {
            var levelName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(levelName);
        }
    }
}
