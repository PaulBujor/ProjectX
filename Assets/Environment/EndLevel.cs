using Assets.Characters.Player.Scripts;
using Assets.LevelManager;
using UnityEngine;

namespace Assets.Environment
{
    public class EndLevel : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player") && collision.gameObject.GetComponent<PlayerController>() != null)
            {
                Debug.Log(collision.gameObject.tag);
                LevelManagerWrite.EndLevel(true);
            }
        }
    }
}
