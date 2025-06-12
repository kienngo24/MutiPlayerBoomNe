using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private List<Transform> enemies;
    public float spawnCooldown;
    public int quality = 10;
    private int currentQuality = 0;
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
            foreach (Transform enemy in enemies)
            {
                SpawnEnemy(enemy);
            }
            currentQuality++;
        }
    }

    private void SpawnEnemy(Transform enemy)
    {
        Transform enemyScript = Instantiate(enemy);
        enemyScript.GetComponent<NetworkObject>().Spawn();
    }
    [ServerRpc(RequireOwnership = false)]
    public void RequestDespawnServerRpc()
    {
        // Nếu enemy bị bắn thì server xử lý despawn
        NetworkObject.Despawn(true);
    }
}
