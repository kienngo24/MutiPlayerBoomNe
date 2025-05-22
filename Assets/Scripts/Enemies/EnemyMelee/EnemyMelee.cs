using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        _enemySM.Init<IdleState_EnemyMelee>();
    }
    protected override void Update()
    {
        base.Update();
        if(_enemySM != null)
            _enemySM.Update();
    }
}
