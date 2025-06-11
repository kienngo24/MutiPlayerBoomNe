using System;
using System.Collections.Generic;

public class EnemyMeleeStateFactory : IStateFactory
{
    private readonly Enemy _enemy;
    private readonly IStateMachine _machine;
    public EnemyMeleeStateFactory(Enemy enemy, IStateMachine machine)
    {
        _enemy = enemy;
        _machine = machine;
    }

    public Dictionary<Type, IState> CreateState()
    {
        var Playerdictionary = new Dictionary<Type, IState>
        {
            {typeof(IdleState_EnemyMelee), new IdleState_EnemyMelee(_enemy, _machine, "Idle")},
            {typeof(MoveState_EnemyMelee), new MoveState_EnemyMelee(_enemy, _machine, "Move")},
            {typeof(AttackState_EnemyMelee), new AttackState_EnemyMelee(_enemy, _machine, "Attack")},
        };
        return Playerdictionary;
    }

}
