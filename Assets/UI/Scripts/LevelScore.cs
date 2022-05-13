using System.Collections;
using System.Collections.Generic;
using Assets.LevelManager;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour
{
    private const string _star1 = "StarLeft";
    private const string _star2 = "StarMiddle";
    private const string _star3 = "StarRight";

    private const int _maxScore = 3;


    [SerializeField]
    private int Score = 0;
    [SerializeField]
    private string LevelName = "Level 1";


    void Start()
    {
        Score = FindObjectOfType<LevelManagerRead>().GetScore(LevelName);

        if (Score == 0)
        {
            return;
        }

        if (Score > _maxScore)
        {
            Score = _maxScore;
        }

        for (int i = 1; i <= Score; i++)
        {
            var star = gameObject.transform.Find(GetStarPath(i));
            star.GetComponent<Image>().color = Color.white;
        }
       
    }

    private string GetStarPath(int star)
    {
        var wrapperPath = "StarsWrapper/";

        switch (star)
        {
            case 1: return wrapperPath + _star1;
            case 2: return wrapperPath + _star2;
            case 3: return wrapperPath + _star3;
        }

        return string.Empty;
    }
}
