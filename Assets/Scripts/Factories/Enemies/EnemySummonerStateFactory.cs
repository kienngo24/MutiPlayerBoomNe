using System;
using System.Collections.Generic;

public class EnemySummonerStateFactory : IStateFactory
{
    private readonly Enemy _enemy;
    private readonly IStateMachine _machine;
    public EnemySummonerStateFactory(Enemy enemy, IStateMachine machine)
    {
        _enemy = enemy;
        _machine = machine;
    }

    public Dictionary<Type, IState> CreateState()
    {
        var Playerdictionary = new Dictionary<Type, IState>
        {
            {typeof(IdleState_EnemySummoner), new IdleState_EnemySummoner(_enemy, _machine, "Idle")},
            {typeof(MoveState_EnemySummoner), new MoveState_EnemySummoner(_enemy, _machine, "Move")},
            {typeof(AttackState_EnemySummoner), new AttackState_EnemySummoner(_enemy, _machine, "Attack")},
            {typeof(SpawnerState_EnemySummoner), new SpawnerState_EnemySummoner(_enemy, _machine, "Summoner")},

        };
        return Playerdictionary;
    }

}
