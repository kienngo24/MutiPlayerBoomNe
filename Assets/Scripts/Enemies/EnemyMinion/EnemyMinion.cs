using UnityEngine;

public class EnemyMinion : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        _enemySM = new EnemyMinionStateMachine(this);
    }
    protected override void Start()
    {
        base.Start();
        _enemySM.Init<IdleState_EnemyMinion>();
    }
    protected override void Update()
    {
        base.Update();
        if (_enemySM != null)
            _enemySM.Update();
    }
}
