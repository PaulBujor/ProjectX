using UnityEngine;

namespace Assets.Characters.Scripts
{
    public abstract class BaseHealthController : MonoBehaviour
    {
        [Header("State at spawn")]
        [SerializeField] private int _health = 3;

        private void Start()
        {
            if (_health <= 0)
            {
                Debug.Log($"{this.GetType().Name} was killed on Start() - Initial health: {_health}");
            }
        }

        protected void TakeDamage(int damage = 1)
        {
            _health -= damage;
            Debug.Log($"{this.GetType().Name} took {damage} HP damage - Health: {_health}");

            if (_health <= 0)
            {
                OnDeath();
            }
        }

        protected abstract void OnDeath();
    }
}
