using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyPath : EnemyMovementController
{

    [Header("Fly path checkpoints")]
    [SerializeField]
    private Transform[] checkpoints;

    [Header("Custom behaviour")]
    [SerializeField] private bool isFollowingPlayer = false;
    [SerializeField] private GameObject player;
    [SerializeField] private float detectionRange = 5;

    private Transform playerPosition;
    private Vector2 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        updateCurrentPosition();

        if (isFollowingPlayer)
        {
            FlyTowardsPlayer();
        }
        else MoveToCheckpoints(checkpoints);
    }

    private void FlyTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, playerPosition.position) < detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, _movementSpeed * Time.deltaTime);
        }
        else MoveToCheckpoints(checkpoints);
    }

    private void updateCurrentPosition()
    {
        currentPosition = transform.position;
    }
}
