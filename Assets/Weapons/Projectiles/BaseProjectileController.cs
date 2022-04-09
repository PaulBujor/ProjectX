using System.Collections;
using UnityEngine;

namespace Assets.Weapons.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseProjectileController : MonoBehaviour
    {
        [Header("Projectile stats")]
        [SerializeField]
        protected float _projectileSpeed;

        [SerializeField] private float _timeToLive = 3f;

        protected void Start()
        {
            StartCoroutine(TimeToLive());
        }

        public abstract void Shoot(Vector2 direction);

        private IEnumerator TimeToLive()
        {
            yield return new WaitForSeconds(_timeToLive);
            Destroy(gameObject);
        }
    }
}