using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeStateMachine : EnemyStateMachine
{
    public EnemyMeleeStateMachine(Enemy enemy) : base(enemy)
    {
        _factory = new EnemyMeleeStateFactory(enemy, this);
        base.CreateState();
    }
}
