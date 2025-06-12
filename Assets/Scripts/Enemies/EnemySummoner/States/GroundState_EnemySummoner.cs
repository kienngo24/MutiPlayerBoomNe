using Unity.VisualScripting;
using UnityEngine;

public class GroundState_EnemySummoner : EnemyState
{
    private EnemySummoner summoner;
    private float spawnMinionCooldown;
    private float lastTimeSpawn;

    public GroundState_EnemySummoner(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
        summoner = (EnemySummoner)enemy;
        spawnMinionCooldown = summoner.spawnMinionCooldown;

    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        if (Time.time > spawnMinionCooldown + lastTimeSpawn)
        {
            lastTimeSpawn = Time.time;
            _machine.ChangeState<SpawnerState_EnemySummoner>();
        }
        if (_enemy.CanAttack())
            _machine.ChangeState<AttackState_EnemySummoner>();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
