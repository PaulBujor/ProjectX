using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [Header("Load next scene")]
    [SerializeField] public string scene;

    private GameObject LevelManager;

    private void Start()
    {
      LevelManager = GameObject.Find("LevelManager");
    }
    //TODO add what to load

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log(collision.gameObject.tag);
            LevelManager.GetComponent<LevelManagerWrite>().EndLevel(true);
        }
    }
}
