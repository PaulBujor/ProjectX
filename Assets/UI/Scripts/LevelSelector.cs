using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
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
