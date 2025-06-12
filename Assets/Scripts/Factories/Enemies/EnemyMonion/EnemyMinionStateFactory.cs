using System;
using System.Collections.Generic;

public class EnemyMinionStateFactory : IStateFactory
{
    private readonly Enemy _enemy;
    private readonly IStateMachine _machine;
    public EnemyMinionStateFactory(Enemy enemy, IStateMachine machine)
    {
        _enemy = enemy;
        _machine = machine;
    }

    public Dictionary<Type, IState> CreateState()
    {
        var Playerdictionary = new Dictionary<Type, IState>
        {
            {typeof(IdleState_EnemyMinion), new IdleState_EnemyMinion(_enemy, _machine, "Idle")},
            {typeof(MoveState_EnemyMinion), new MoveState_EnemyMinion(_enemy, _machine, "Idle")},
            {typeof(AttackState_EnemyMinion), new AttackState_EnemyMinion(_enemy, _machine, "Attack")},
        };
        return Playerdictionary;
    }

}
