using Assets.Characters.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyHealthController : BaseHealthController
{
    private Animator _animator;

    public Action OnCharacterDeath { get; set; }

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    protected override void OnDeath()
    {
        _animator.SetBool("isAlive", false);
        _animator.Play("Death");

        if (OnCharacterDeath != null)
        {
            OnCharacterDeath.Invoke();
        }

        StartCoroutine(WaitDeathAnimation());
    }

    private IEnumerator WaitDeathAnimation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            TakeDamage();
        }
    }
}