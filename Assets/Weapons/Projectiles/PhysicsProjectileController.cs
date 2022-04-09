using UnityEngine;

namespace Assets.Weapons.Projectiles
{
    public class PhysicsProjectileController : BaseProjectileController
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;

        private new void Start()
        {
            base.Start();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.freezeRotation = true;
            InternalShoot();
        }

        public override void Shoot(Vector2 direction)
        {
            _direction = direction;
        }

        private void InternalShoot()
        {
            _rigidbody2D.AddForce(_direction * _projectileSpeed, ForceMode2D.Impulse);
        }
    }
}