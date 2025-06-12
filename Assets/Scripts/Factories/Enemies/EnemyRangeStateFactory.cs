using System;
using System.Collections.Generic;

public class EnemyRangeStateFactory : IStateFactory
{
    private readonly Enemy _enemy;
    private readonly IStateMachine _machine;
    public EnemyRangeStateFactory(Enemy enemy, IStateMachine machine)
    {
        _enemy = enemy;
        _machine = machine;
    }

    public Dictionary<Type, IState> CreateState()
    {
        var Playerdictionary = new Dictionary<Type, IState>
        {
            {typeof(IdleState_EnemyRange), new IdleState_EnemyRange(_enemy, _machine, "Idle")},
            {typeof(MoveState_EnemyRange), new MoveState_EnemyRange(_enemy, _machine, "Move")},
            {typeof(AttackState_EnemyRange), new AttackState_EnemyRange(_enemy, _machine, "Attack")},
        };
        return Playerdictionary;
    }

}
