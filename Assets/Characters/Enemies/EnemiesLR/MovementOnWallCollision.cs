using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Characters.Scripts
{
    public class MovementOnWallCollision : MonoBehaviour
    {
        [Header("Movement speed")]
        [SerializeField]
        private float moveSpeed = 6f;

        [Header("Start direction")]
        [SerializeField]
        private float movementDirection = 1f;

        [Header("Enemy target")]
        [SerializeField]
        private Rigidbody2D enemyTarget;

        private bool facingRight;
        private Vector3 localScale;

        private void Start()
        {
            localScale = transform.localScale;
            enemyTarget = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            enemyTarget.velocity = new Vector2(movementDirection * moveSpeed, enemyTarget.velocity.y);
        }

        private void LateUpdate()
        {
            checkFacingDirection();
        }

        private void checkFacingDirection()
        {
            {
                if (movementDirection > 0)
                {
                    facingRight = true;
                }
                else if (movementDirection < 0)
                {
                    facingRight = false;
                }

                if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
                {
                    localScale.x *= -1;
                }

                transform.localScale = localScale;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                movementDirection *= -1;
            }
        }

       


    }
}
