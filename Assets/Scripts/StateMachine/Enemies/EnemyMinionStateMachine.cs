using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinionStateMachine : EnemyStateMachine
{
    public EnemyMinionStateMachine(Enemy enemy) : base(enemy)
    {
        _factory = new EnemyMinionStateFactory(enemy, this);
        base.CreateState();
    }
}
