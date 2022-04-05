using UnityEngine;

namespace Assets.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BaseCharacter : MonoBehaviour
    {
        [Header("Debugging")]
        [SerializeField] private GameObject _directionIndicator;

        [Header("Character movement")]
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _jumpForce = 20f;

        //Compensate for Time.deltaTime induced sluggishness
        private readonly int _deltaTimeCompensator = 20;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _lastCharacterDirection;
        private bool _characterIsGrounded;

        protected Vector2 InputVector;
        protected bool CharacterMovementIsLocked;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _lastCharacterDirection = new Vector2(1, 0);
        }

        void Update()
        {
            GetLastDirection();
        }

        void FixedUpdate()
        {
            HandleMovement();
        }

        private void GetLastDirection()
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

        private void HandleMovement()
        {
            if (!CharacterMovementIsLocked)
            {
                var movement =
                    new Vector2(
                        InputVector.x * Mathf.Max(0, _movementSpeed - Mathf.Abs(_rigidbody2D.velocity.x)) * Time.deltaTime *
                        _deltaTimeCompensator, 0);
                _rigidbody2D.AddForce(movement, ForceMode2D.Impulse);
                Debug.Log($"Player moving: {movement}");
            }
            else
            {
                Debug.Log($"Player movement locked: {CharacterMovementIsLocked}");
            }
        }

        protected void HandleJump()
        {
            Debug.Log($"Player grounded: {_characterIsGrounded}");
            if (_characterIsGrounded)
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                Debug.Log("Player jumped!");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Map"))
            {
                _characterIsGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Map"))
            {
                _characterIsGrounded = false;
            }
        }
    }
}
