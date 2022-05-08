using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerRead : MonoBehaviour
{
    public int GetScore(string level)
    {
        return PlayerPrefs.GetInt(level.ToLower());
    }
}
