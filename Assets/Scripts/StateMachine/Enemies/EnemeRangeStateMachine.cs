using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemeRangeStateMachine : EnemyStateMachine
{
    public EnemeRangeStateMachine(Enemy enemy) : base(enemy)
    {
        _factory = new EnemyMeleeStateFactory(enemy, this);
        base.CreateState();
    }
}
