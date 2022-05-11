using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelManager
{
    public class LevelSwitcher : MonoBehaviour
    {

        public void ChangeLevelWithFade(string levelName)
        {

            SimpleSceneFader.ChangeSceneWithFade(levelName);

        }
        public void ChangeLevelWithoutFade(string levelName)
        {

            SceneManager.LoadScene(levelName);
        }

        public void ReloadCurrentScene()
        {
            var levelName = SceneManager.GetActiveScene().name;
            //var levelName = "level 1";
            //SimpleSceneFader.ChangeSceneWithFade(levelName);
            SceneManager.LoadScene(levelName);

        }
    }
}
