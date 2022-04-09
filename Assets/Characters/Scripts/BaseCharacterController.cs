using System.Linq;
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

        //Compensate for Time.deltaTime induced sluggishness
        private const int DeltaTimeCompensator = 20;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _lastCharacterDirection;
        private bool _characterIsGrounded;

        protected Vector2 InputVector;
        protected bool CharacterMovementIsLocked;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _lastCharacterDirection = Vector2.right;
        }

        private void Update()
        {
            SaveLastDirection();
        }

        private void FixedUpdate()
        {
            Move();
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
                        InputVector.x * Mathf.Max(0, _movementSpeed - Mathf.Abs(_rigidbody2D.velocity.x)) * Time.deltaTime *
                        DeltaTimeCompensator, 0);
                _rigidbody2D.AddForce(movement, ForceMode2D.Impulse);
            }
        }

        protected void Jump()
        {
            if (_characterIsGrounded)
            {
                _characterIsGrounded = false;
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contacts.Select(contact => contact.point.y).Min() < transform.position.y)
            {
                _characterIsGrounded = true;
            }
        }
    }
}
