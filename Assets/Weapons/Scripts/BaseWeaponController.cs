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
        private Animator _animator;
        private int shootVariant = 0;

        private BaseAudioController _audioController;

        private void Start()
        {
            _isShooting = false;
            _shootingCoroutineIsRunning = false;
            _audioController = GetComponent<BaseAudioController>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void FixedUpdate()
        {
            //Animation
            _animator.SetBool("isShooting", _isShooting);
            _animator.SetInteger("shootVariant", shootVariant);
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

            //Animation
            convertDirectionToShootVariant(shootingDirection);
            _animator.SetTrigger("shoot");

            if (_audioController != null)
            {
                var volumeModifier = Random.Range(1, 6)/10f;
                _audioController.PlayOnce("Shoot", volumeModifier);
            }
        }


        //God forgive me for this.
        //Correct way of doing this would be animating the hand to rotate based on vector direction and playing shooting animation on trigger.
        private void convertDirectionToShootVariant(Vector2 direction)
        {
            Vector2 shootDirection = new Vector2(Mathf.Abs(direction.x), direction.y);

            if((shootDirection.x >= 0 && shootDirection.x <= 0.25 ) && ( shootDirection.y <= 1 && shootDirection.y >=  0.968) )
            {
                shootVariant = 1;
            }

           if (shootDirection.x == 1 && shootDirection.y == 1)
            {
                shootVariant = 2;
            }

            if((shootDirection.x >  0.5 && shootDirection.x < 1 ) &&  (shootDirection.y < 0.8 && shootDirection.y > 0.3  )){

                shootVariant = 2;

            }         

            if (shootDirection.x == 1 && (shootDirection.y <= 0.2 && shootDirection.y >= -0.2))
            {
                shootVariant = 3;
            }

            if (shootDirection.x == 1 && shootDirection.y == -1)
            {
                shootVariant = 4;
            }

            if ((shootDirection.x <= 0.9 && shootDirection.x >= 0.4)  && (shootDirection.y <= -0.52 && shootDirection.y >= -0.9 ) )
            {
                shootVariant = 4;
            }

           if (shootDirection.x == 0 && shootDirection.y == -1)
            {
                shootVariant = 5;
            }

           if((direction.x <= 0.3 && direction.x >= -0.3) && (direction.y == -1))
            {
                shootVariant = 5;
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
