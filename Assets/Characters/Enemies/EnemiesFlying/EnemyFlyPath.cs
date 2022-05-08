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
    [SerializeField] private float detectionRange = 5;

    private Transform playerPosition;
    private Vector2 currentPosition;
    private SpriteRenderer spriteRenderer;
    private GameObject playerObj;
    private bool isRightDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPosition = playerObj.GetComponent<Transform>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        spriteRenderer.flipX = isRightDirection;
    }

    // Update is called once per frame
    void Update()
    {
        updateCurrentPosition();

        Flip();

        if (isFollowingPlayer)
        {
            FlyTowardsPlayer();
        }
        else MoveToCheckpoints(checkpoints);
    }

    private void Flip()
    {
       if(currentPosition.x > playerPosition.transform.position.x)
        {
            spriteRenderer.flipX = !isRightDirection;
        }
       else spriteRenderer.flipX = isRightDirection;
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
