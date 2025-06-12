using UnityEngine;

public class EnemySummoner : Enemy
{
    public float spawnMinionCooldown = 5;
    public int minionSpawnCount = 3;
    protected override void Awake()
    {
        base.Awake();
        _enemySM = new EnemySummonerStateMachine(this);
    }
    protected override void Start()
    {
        base.Start();
        _enemySM.Init<IdleState_EnemySummoner>();
    }
    protected override void Update()
    {
        base.Update();
        if (_enemySM != null)
            _enemySM.Update();
    }
    public void SpawnMinion()
    {
        SpawnerState_EnemySummoner spawnState = (SpawnerState_EnemySummoner)_enemySM.GetCurrentState();
        spawnState?.SetSpawner(true);
    }
}
