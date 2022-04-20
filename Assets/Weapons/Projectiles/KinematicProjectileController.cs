using UnityEngine;

namespace Assets.Weapons.Projectiles
{
    public class KinematicProjectileController : BaseProjectileController
    {
        private Vector2 _direction;

        protected override void Start()
        {
            base.Start();
            Rigidbody.isKinematic = true;
        }

        private void FixedUpdate()
        {
            Rigidbody.velocity = _direction * ProjectileSpeed;
        }

        public override void Shoot(Vector2 direction)
        {
            _direction = direction;
        }
    }
}