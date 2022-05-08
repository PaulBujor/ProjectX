using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [Header("Load next scene")]
    [SerializeField] public string scene;


    private void Start()
    {
      var levelManager =   gameObject.GetComponent<LevelManagerWrite>();
        levelManager.StartLevel();
    }
    //TODO add what to load

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log(collision.gameObject.tag);
            var levelManager = gameObject.GetComponent<LevelManagerWrite>();
            levelManager.EndLevel(true);
        }
    }
}
