using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Assets.Characters {
    public class EnemyAI : MonoBehaviour
    {
        [Header("Pathfinding")]
        public Transform target;
        public float activateDistance = 50f;
        public float pathUpdateSeconds = 0.5f;
        public float rcDistance = 1f;

        [Header("Physics")]
        public float speed = 200f;
        public float nextWaypointDistance = 3f;
        public float jumpNodeHeightRequirement = 0.8f;
        public float jumpModifier = 0.3f;
        public float jumpCheckOffset = 0.1f;

        [Header("Custom Behavior")]
        public bool followEnabled = true;
        public bool jumpEnabled = true;
        public bool directionLookEnabled = true;

        private Path path;
        private int currentWaypoint = 0;
        private bool isRightDirection = true;
        private bool isCloseToWall = false;
        private bool isEnemyGrounded;
        RaycastHit2D isGrounded;
        Seeker seeker;
        Rigidbody2D rb;

        public void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();

            InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        }

       

        private void FixedUpdate()
        {

            Debug.Log(isGrounded);
          

            if (TargetInDistance() && followEnabled)
            {
                PathFollow();
            }

            isNearWall();

            if (isCloseToWall)
            {
                JumpTestas();
            }
        }

        private void UpdatePath()
        {
            if (followEnabled && TargetInDistance() && seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }

        private void PathFollow()
        {
            if (path == null)
            {
                return;
            }

            // Reached end of path
            if (currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }

            // See if colliding with anything
            Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
            isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

            // Direction Calculation
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            // Jump
            if (jumpEnabled && isGrounded)
            {
                if (direction.y > jumpNodeHeightRequirement)
                {
                    rb.AddForce(Vector2.up * speed * jumpModifier);
                }
            }

            // Movement
            rb.AddForce(force);

            // Next Waypoint
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            // Direction Graphics Handling
            if (directionLookEnabled)
            {
                if (rb.velocity.x > 0.05f)
                {
                    transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (rb.velocity.x < -0.05f)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
        }

        private void JumpTestas()
        {
            if (isEnemyGrounded)
            {
                
                rb.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
                
            }

            
        }

        private void isNearWall()
        {
            if (rb.velocity.x > 0.05f)
            {
                isRightDirection = true;
            }
            else if (rb.velocity.x < -0.05f)
            {
                isRightDirection = false;
            }




            Vector3 rcDirection = (isRightDirection) ? Vector3.right : Vector3.left;

            RaycastHit2D hit = Physics2D.Raycast(transform.position + rcDirection * rcDistance - new Vector3(0f, 1f, 0f), rcDirection, 0.1f);

            Debug.DrawRay(transform.position, rcDirection * rcDistance);


            if (hit.collider != null)
            {
                if (hit.transform.tag == "Map")
                {
                    /*     Debug.Log(hit.transform.tag);*/
                    /*isRightDirection = !isRightDirection;*/
                    isCloseToWall = true;
                }
                else isCloseToWall = false;
            }


        }


        private bool TargetInDistance()
        {
            return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Map"))
            {
                isEnemyGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Map"))
            {
                isEnemyGrounded = false;
            }
        }
    }


}