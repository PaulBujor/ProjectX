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
        protected float ProjectileSpeed = 20;
        [SerializeField] private float _timeToLive = 3f;
        [SerializeField] private int _inflictedDamage = 1;

        protected Rigidbody2D Rigidbody;
        protected Collider2D Collider;

        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.freezeRotation = true;

            Collider = GetComponent<Collider2D>();
            Collider.isTrigger = true;

            StartCoroutine(TimeToLive());
        }

        public int GetDamage()
        {
            return _inflictedDamage;
        }

        public abstract void Shoot(Vector2 direction);

        private IEnumerator TimeToLive()
        {
            yield return new WaitForSeconds(_timeToLive);
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D triggeredCollider)
        {
            if (triggeredCollider.gameObject.tag != tag)
            {
                Destroy(gameObject);
            }
        }
    }
}