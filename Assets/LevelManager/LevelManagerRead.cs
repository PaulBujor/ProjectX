using UnityEngine;

namespace Assets.LevelManager
{
    public class LevelManagerRead : MonoBehaviour
    {
        public int GetScore(string level)
        {
            return PlayerPrefs.GetInt(level.ToLower() + "Score");
        }
    }
}
