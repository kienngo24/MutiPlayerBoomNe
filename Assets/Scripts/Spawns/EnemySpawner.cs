using System;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private Transform enemyMelee;
    public float spawnCooldown;
    public int quality = 10;
    private int currentQuality =0;
    private float lastSpawnTimer;
    private void Update()
    {
        if (!IsServer)
            return;
        if (currentQuality >= quality)
            return;
        
        if (Time.time > lastSpawnTimer + spawnCooldown)
        {
            lastSpawnTimer = Time.time;
            SpawnEnemy();
            currentQuality++;
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
