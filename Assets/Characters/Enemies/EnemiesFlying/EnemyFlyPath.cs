using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyPath : EnemyMovementController
{

    [SerializeField]
    private Transform[] checkpoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToCheckpoints(checkpoints);
    }
}
