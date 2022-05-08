using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : BaseSpawnController
{
    [Header("Spawn object")]
    [SerializeField] public bool isRespawning = true;
    [SerializeField] public float respawnTimer = 4f;


    protected override void Start()
    {
        base.Start();
        Respawn();
    }

    private IEnumerator RespawnObjectAfterTime()
    {
        if (isRespawning)
        {
            yield return new WaitForSeconds(respawnTimer);
            Respawn();
        }
    }

    private void Respawn()
    {
        GameObject game = Spawn();
        var healtController = game.GetComponentInChildren<EnemyHealthController>();
        healtController.OnCharacterDeath = () => StartCoroutine(RespawnObjectAfterTime());
    }
}
