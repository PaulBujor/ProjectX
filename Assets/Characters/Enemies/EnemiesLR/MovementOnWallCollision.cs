using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets { 
    public class MovementOnWallCollision : EnemyMovementController
    {
        [SerializeField]
        private bool isRightDirection = false;

        [SerializeField]
        private float rcDistance = 0.7f;

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _spriteRenderer.flipX = isRightDirection;
        }

        private void Update()
        {
            MoveToDirection(isRightDirection);

            CheckForWalls();
            
        }

        private void CheckForWalls()
        {
            Vector3 rcDirection = (isRightDirection) ? Vector3.right : Vector3.left;

            RaycastHit2D hit = Physics2D.Raycast(transform.position + rcDirection * rcDistance - new Vector3(0f, 0.01f, 0f), rcDirection, 0.1f);

            if (hit.collider != null)
            {
                if(hit.transform.tag == "Wall")
                {
                    Debug.Log(hit.transform.tag);
                    isRightDirection = !isRightDirection;
                    _spriteRenderer.flipX = !isRightDirection;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
}
