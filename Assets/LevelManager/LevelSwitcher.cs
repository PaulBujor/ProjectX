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
    }
}
