using System.Collections;
using Assets.Characters.Scripts;
using Assets.LevelManager;
using UnityEngine;

namespace Assets.Characters.Player.Scripts
{
    public class PlayerHealthController : BaseHealthController
    {
        [Header("Player specific")]
        [SerializeField]
        [Tooltip("Time of immunity - when the player takes damage, he will only take more damage after the cooldown is finished")]
        private float _immunityCooldown = 1f;
        [SerializeField]
        [Tooltip("How many seconds between character flashes when taking damage")]
        private float _damageVisualizerFrequency = 3f;

        private bool _isImmune = false;
        private PauseHandler _pauseHandler;
        void Awake()
        {
            _pauseHandler = GetComponentInChildren<PauseHandler>();
        }

        protected override void OnDeath()
        {
            var childrenSprites = GetComponentsInChildren<SpriteRenderer>();

            foreach (var childrenSprite in childrenSprites)
            {
                childrenSprite.color = Color.black;
            }

            // make sure the character can no longer move
            var baseCharacterController = GetComponent<BaseCharacterController>();
            if (baseCharacterController != null)
            {

                baseCharacterController.Kill();

            }

            StartCoroutine(EndGameTimeout());
        }

        private IEnumerator EndGameTimeout()
        {
            yield return new WaitForSeconds(1);
            LevelManagerWrite.EndLevel(false);
        }

        private IEnumerator DamageCooldown()
        {
            _isImmune = true;
            StartCoroutine(DamageVisualize());
            yield return new WaitForSeconds(_immunityCooldown);
            _isImmune = false;
        }

        private IEnumerator DamageVisualize()
        {
            var childrenSprites = GetComponentsInChildren<SpriteRenderer>();

            while (_isImmune)
            {
                foreach (var childrenSprite in childrenSprites)
                {
                    childrenSprite.enabled = !childrenSprite.enabled;
                }
                yield return new WaitForSeconds(_damageVisualizerFrequency / 20f);
            }

            foreach (var childrenSprite in childrenSprites)
            {
                childrenSprite.enabled = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DeadlyTrap"))
            {
                TakeDamage(10);
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (!_isImmune)
                {
                    TakeDamage();
                    StartCoroutine(DamageCooldown());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("DeadlyTrap"))
            {
                TakeDamage(10);
            }
        }
    }
}
