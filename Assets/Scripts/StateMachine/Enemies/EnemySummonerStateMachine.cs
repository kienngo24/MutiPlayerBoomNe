using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummonerStateMachine : EnemyStateMachine
{
    public EnemySummonerStateMachine(Enemy enemy) : base(enemy)
    {
        _factory = new EnemySummonerStateFactory(enemy, this);
        base.CreateState();
    }
}
