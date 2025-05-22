using System;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private Transform enemyMelee;
    public float spawnCooldown;
    private float lastSpawnTimer;
    private void Update()
    {
        if (!IsServer)
            return;
        if (Time.time > lastSpawnTimer + spawnCooldown)
        {
            lastSpawnTimer = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Transform enemy = Instantiate(enemyMelee);
        enemy.GetComponent<NetworkObject>().Spawn();
    }
    [ServerRpc(RequireOwnership = false)]
    public void RequestDespawnServerRpc()
    {
        // Nếu enemy bị bắn thì server xử lý despawn
        NetworkObject.Despawn(true);
    }
}
