using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnController : MonoBehaviour
{
    [Header("Spawn object")]
    [SerializeField] public GameObject prefab;    

    private Vector2 spawnPoint;
  

    void Start()
    {
        spawnPoint = transform.position;
        Spawn();
    }   

    public GameObject Spawn()
    {
       return Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}
