using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _directionIndicator;
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _jumpForce = 20f;

        private int _deltaTimeCompensator = 20;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _inputVector;
        private Vector2 _lastDirection;
        private bool _isLocked;
        private bool _isGrounded;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _lastDirection = new Vector2(1, 0);
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
            Vector2 currentDirection = _lastDirection.normalized;

            if (_inputVector != Vector2.zero)
            {
                currentDirection = _inputVector;

                if (currentDirection.x != 0)
                {
                    _lastDirection = new Vector2(currentDirection.x, 0);
                }
            }

            _directionIndicator.transform.localPosition = currentDirection;
        }

        private void HandleMovement()
        {
            if (!_isLocked)
            {
                var movement =
                    new Vector2(
                        _inputVector.x * Mathf.Max(0, _movementSpeed - Mathf.Abs(_rigidbody2D.velocity.x)) * Time.deltaTime *
                        _deltaTimeCompensator, 0);
                _rigidbody2D.AddForce(movement, ForceMode2D.Impulse);
                Debug.Log($"Player moving: {movement}");
            }
            else
            {
                Debug.Log($"Player movement locked: {_isLocked}");
            }
        }

        private void HandleJump()
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Debug.Log("Player jumped!");
        }

        void OnMove(InputAction.CallbackContext ctx)
        {
            _inputVector = ctx.ReadValue<Vector2>();
        }

        void OnJump(InputAction.CallbackContext ctx)
        {
            Debug.Log($"Player grounded: {_isGrounded}");
            if (_isGrounded && ctx.started)
            {
                HandleJump();
            }
        }

        void OnLockMovement(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                _isLocked = true;
            }
            else if (ctx.canceled)
            {
                _isLocked = false;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Map"))
            {
                _isGrounded = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Map"))
            {
                _isGrounded = false;
            }
        }
    }
}