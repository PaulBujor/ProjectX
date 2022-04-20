using Assets.Audio.Scripts;
using UnityEngine;

namespace Assets.Characters.Scripts
{
    public abstract class BaseHealthController : MonoBehaviour
    {
        [Header("State at spawn")]
        [SerializeField] private int _health = 3;

        private BaseAudioController _audioController;

        private void Start()
        {
            _audioController = GetComponent<BaseAudioController>();
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
                if (_audioController != null)
                {
                    _audioController.PlayOnce("Death");
                }
            }
            else
            {
                if (_audioController != null)
                {
                    _audioController.PlayOnce("TakeDamage");
                }
            }
        }

        protected abstract void OnDeath();
    }
}
