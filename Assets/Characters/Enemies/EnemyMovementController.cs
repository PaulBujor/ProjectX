using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class EnemyMovementController : MonoBehaviour
{
    

    [Header("Physics")]
    [SerializeField] protected float _movementSpeed = 5f;

    private int checkpointsPosition = 0;

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
        // -transform.right == left
        else transform.position += -transform.right * _movementSpeed * Time.deltaTime;

    }

    public void MoveToCheckpoints(Transform[] checkpoints)
    {
        if(Vector2.Distance(transform.position, checkpoints[checkpointsPosition].position) < 0.02f){

            checkpointsPosition++;
            if (checkpointsPosition == checkpoints.Length)
            {
                checkpointsPosition = 0;
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, checkpoints[checkpointsPosition].position, Time.deltaTime * _movementSpeed);
               
    }

}
