using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyMovementController : MonoBehaviour
{
    

    [Header("Enemy movement")]
    [SerializeField] private float _movementSpeed = 5f;

   
    private Rigidbody2D enemyTarget;

    private void Start()
    {
        enemyTarget = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    public void MoveToDirection(bool isRight)
    {
        if(isRight)
        {
            transform.position += transform.right * _movementSpeed * Time.deltaTime;
        }
        // -transform.right = left
        else transform.position += -transform.right * _movementSpeed * Time.deltaTime;

    }

    public void MoveToCheckpoints()
    {

    }

}
