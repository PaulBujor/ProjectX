using System.Collections;
using Assets.Characters.Scripts;
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
        [Tooltip("How ofthen should the character flash when taking damage")]
        private float _damageVisualizerFrequency = .05f;

        private bool _isImmune = false;

        protected override void OnDeath()
        {
            Debug.LogError("PLAYER DED"); //todo elegant

            var childrenSprites = GetComponentsInChildren<SpriteRenderer>();


            foreach (var childrenSprite in childrenSprites)
            {
                childrenSprite.color = Color.black;
            }
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
                    yield return new WaitForSeconds(_damageVisualizerFrequency);
                }
            }

            foreach (var childrenSprite in childrenSprites)
            {
                childrenSprite.enabled = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (!_isImmune)
                {
                    TakeDamage();
                    StartCoroutine(DamageCooldown());
                }
            }
        }
    }
}
