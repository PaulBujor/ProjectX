using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour
{
    private const string _star1 = "StarLeft";
    private const string _star2 = "StarMiddle";
    private const string _star3 = "StarRight";

    public int Score = 0;

    private const int _maxScore = 3;

    void Start()
    {
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
