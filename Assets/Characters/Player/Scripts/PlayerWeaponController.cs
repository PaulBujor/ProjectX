using Assets.Weapons.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Characters.Player.Scripts
{
    public class PlayerWeaponController : BaseWeaponController
    {
        public void OnFire(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                OnShootStart();
            }
            
            if (ctx.canceled)
            {
                OnShootEnd();
            }
        }
    }
}