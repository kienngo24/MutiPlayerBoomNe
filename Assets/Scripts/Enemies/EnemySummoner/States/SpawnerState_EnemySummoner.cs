using Unity.VisualScripting;
using UnityEngine;

public class SpawnerState_EnemySummoner : EnemyState
{
    private EnemySummoner summoner;
    private int minionSpawnCount;

    private bool canSpawnMinion;
    public SpawnerState_EnemySummoner(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
        minionSpawnCount = summoner.minionSpawnCount;
    }

    public override void Enter()
    {
        base.Enter();
        canSpawnMinion = false;
    }
    public override void Excute()
    {
        base.Excute();
        if (CanSpawn())
        {
            SetSpawner(false);

            // spawn something here
        }
        if (Istrigger)
        {
            _machine.ChangeState<IdleState_EnemySummoner>();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    public void SetSpawner(bool active) => canSpawnMinion = active;
    private bool CanSpawn() => canSpawnMinion;
}
