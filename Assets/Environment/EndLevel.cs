using Assets.LevelManager;
using UnityEngine;

namespace Assets.Environment
{
    public class EndLevel : MonoBehaviour
    {
        [Header("Load next scene")]
        [SerializeField] public string scene;

        private LevelManagerWrite _levelManager;

        private void Start()
        {
            _levelManager = GetComponentInParent<LevelManagerWrite>();
        }
        //TODO add what to load

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                Debug.Log(collision.gameObject.tag);
                _levelManager.EndLevel(true);
            }
        }
    }
}
