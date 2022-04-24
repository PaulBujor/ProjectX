using Assets.Characters.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : BaseHealthController
{

    public Action OnCharacterDeath { get; set; }

    protected override void OnDeath()
    {
      OnCharacterDeath.Invoke();
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
