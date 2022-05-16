using UnityEngine;

namespace Assets.LevelManager
{
    public class LevelManagerRead : MonoBehaviour
    {
        public static int GetScore(string level)
        {
            return PlayerPrefs.GetInt(level.ToLower() + "Score");
        }
    }
}
