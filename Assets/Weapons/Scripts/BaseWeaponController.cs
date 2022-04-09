using System.Collections;
using Assets.Characters;
using Assets.Weapons.Projectiles;
using UnityEngine;

namespace Assets.Weapons.Scripts
{
    [RequireComponent(typeof(BaseCharacterController))]
    public class BaseWeaponController : MonoBehaviour
    {
        [Header("Projectile")]
        [SerializeField]
        private GameObject _projectile;

        [Header("Weapon stats")]
        [SerializeField]
        [Tooltip("How many milliseconds between weapon shoots")]
        private float _rateOfFire = 3f;

        private bool _isShooting;

        private void Start()
        {
            _isShooting = false;
            StartCoroutine(ShootingAtRateOfFire());
        }

        //private void Update()
        //{
        //    if (_isShooting)
        //    {
        //        StartCoroutine(ShootingAtRateOfFire());
        //    }
        //    else
        //    {
        //        StopCoroutine(ShootingAtRateOfFire());
        //    }
        //}

        private IEnumerator ShootingAtRateOfFire()
        {
            while (true)
            {
                if (_isShooting)
                {
                    OnShoot();
                }

                yield return new WaitForSeconds(_rateOfFire / 10f);
            }
        }

        private void OnShoot()
        {
            var shootingDirection = GetComponent<BaseCharacterController>()?.GetLastDirection() ?? Vector2.right;
            var projectile = Instantiate(_projectile, transform.position, transform.rotation);
            var projectileController = projectile.GetComponent<BaseProjectileController>();
            projectileController.Shoot(shootingDirection);
        }

        protected void OnShootStart()
        {
            _isShooting = true;
        }

        protected void OnShootEnd()
        {
            _isShooting = false;
        }

        private void OnDestroy()
        {
            StopCoroutine(ShootingAtRateOfFire());
        }
    }
}
