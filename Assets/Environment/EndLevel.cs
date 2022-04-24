using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [Header("Load next scene")]
    [SerializeField] public string scene;

    //TODO add what to load

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("End me");
        }
    }
}
