using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Characters.Player.Scripts
{
    public class PlayerController : BaseCharacterController
    {
        private void OnMove(InputAction.CallbackContext ctx)
        {
            InputVector = ctx.ReadValue<Vector2>();
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Jump();
            }
        }

        private void OnLockMovement(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                CharacterMovementIsLocked = true;
            }
            else if (ctx.canceled)
            {
                CharacterMovementIsLocked = false;
            }
        }
    }
}