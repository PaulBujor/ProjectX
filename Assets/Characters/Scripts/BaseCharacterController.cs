using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Audio.Scripts;

using UnityEngine;

namespace Assets.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BaseCharacterController : MonoBehaviour
    {
        [Header("Debugging")]
        [SerializeField] private GameObject _directionIndicator;

        [Header("Character movement")]
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _jumpForce = 20f;
        [SerializeField] private float _wallJumpCooldown = 0.3f;

        [Header("Jumpable surfaces")]
        [SerializeField] private List<string> _jumpableTags = new List<string>() { "Map", "Platform", "Wall" };     


        // Compensate for Time.deltaTime induced sluggishness
        private const int DeltaTimeCompensator = 20;
        private Vector2 _lastCharacterDirection;
        private BaseAudioController _audioController;
        private bool _characterIsDead = false;
        private bool isJumpCooldown = false;
        protected bool CharacterIsGrounded;
        protected Rigidbody2D Rigidbody;
        protected Vector2 InputVector;
        protected bool CharacterMovementIsLocked;

        protected void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _lastCharacterDirection = Vector2.right;
            _audioController = GetComponent<BaseAudioController>();
        }

        protected virtual void Update()
        {
            SaveLastDirection();
            if (_audioController != null)
            {
                if (CharacterIsGrounded && Rigidbody.velocity != Vector2.zero && InputVector != Vector2.zero)
                {
                    _audioController.PlayLooping("Walk");
                }
                else
                {
                    _audioController.StopLooping("Walk");
                }
            }

            if (_characterIsDead)
            {
                CharacterMovementIsLocked = true;
            }
        }

        protected void FixedUpdate()
        {
            Move();
        }

        public void Kill()
        {
            _characterIsDead = true;
        }

        private void SaveLastDirection()
        {
            Vector2 currentDirection = _lastCharacterDirection.normalized;

            if (InputVector != Vector2.zero)
            {
                currentDirection = InputVector;

                if (currentDirection.x != 0)
                {
                    _lastCharacterDirection = new Vector2(currentDirection.x, 0);
                }
            }

            if (_directionIndicator != null)
            {
                _directionIndicator.transform.localPosition = currentDirection;
            }
        }

        public void SetLastDirection(Vector2 direction)
        {
            _lastCharacterDirection = direction;
        }

        public Vector2 GetLastDirection()
        {
            return InputVector != Vector2.zero ? InputVector : _lastCharacterDirection;
        }

        private void Move()
        {
            
            if (!CharacterMovementIsLocked)
            {
                var movement =
                    new Vector2(
                        InputVector.x * Mathf.Max(0, _movementSpeed - Mathf.Abs(Rigidbody.velocity.x)) * Time.deltaTime *
                        DeltaTimeCompensator, 0);

               
                Rigidbody.AddForce(movement, ForceMode2D.Impulse);
            }
        }

        protected bool Jump()
        {
            if (CharacterIsGrounded && !isJumpCooldown)
            {
                StartCoroutine(WallJumpCooldown());
                Rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                if (_audioController != null)
                {
                    _audioController.PlayOnce("Jump");
                }
                return true;
            }
            
            return false;
        }

        private IEnumerator WallJumpCooldown()
        {
            isJumpCooldown = true;
            yield return new WaitForSeconds(_wallJumpCooldown);
            isJumpCooldown = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_jumpableTags.Contains(collision.gameObject.tag))
            {
                CharacterIsGrounded = true;
            }            
        }

        private void OnCollisionExit2D(Collision2D collision)
        {           
            if (_jumpableTags.Contains(collision.gameObject.tag))
            {
                CharacterIsGrounded = false;
            }  
        }
    }
}
