using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void ChangeLevelTo(string levelName)
    {
        SimpleSceneFader.ChangeSceneWithFade(levelName);
    }
}
