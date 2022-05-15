using Assets.LevelManager;
using UnityEngine;

namespace Assets.Environment
{
    public class EndLevel : MonoBehaviour
    {
        [Header("Load next scene")]
        [SerializeField] private string scene;


        void Start()
        {
            LevelManagerWrite.StartLevel();
        }
        //TODO add what to load

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                Debug.Log(collision.gameObject.tag);
                LevelManagerWrite.EndLevel(true);

            }
        }
    }
}
