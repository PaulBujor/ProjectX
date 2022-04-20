using System.Collections;
using Assets.Audio.Scripts;
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
        private bool _shootingCoroutineIsRunning;

        private BaseAudioController _audioController;

        private void Start()
        {
            _isShooting = false;
            _shootingCoroutineIsRunning = false;
            _audioController = GetComponent<BaseAudioController>();
        }

        private IEnumerator ShootingAtRateOfFire()
        {
            _shootingCoroutineIsRunning = true;
            while (_isShooting)
            {
                OnShoot();
                yield return new WaitForSeconds(_rateOfFire / 10f);
            }

            _shootingCoroutineIsRunning = false;
        }

        private void OnShoot()
        {
            var shootingDirection = GetComponent<BaseCharacterController>()?.GetLastDirection() ?? Vector2.right;
            var projectile = Instantiate(_projectile, transform.position, transform.rotation);
            var projectileController = projectile.GetComponent<BaseProjectileController>();
            projectileController.tag = tag;
            projectileController.Shoot(shootingDirection);

            if (_audioController != null)
            {
                var volumeModifier = Random.Range(1, 6)/10f;
                _audioController.PlayOnce("Shoot", volumeModifier);
            }
        }

        protected void OnShootStart()
        {
            _isShooting = true;
            if (!_shootingCoroutineIsRunning)
            {
                StartCoroutine(ShootingAtRateOfFire());
            }
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
