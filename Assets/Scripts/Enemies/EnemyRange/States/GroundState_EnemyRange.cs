using Unity.VisualScripting;
using UnityEngine;

public class GroundState_EnemyRange : EnemyState
{
    public GroundState_EnemyRange(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        if (_enemy.CanAttack())
            _machine.ChangeState<AttackState_EnemyRange>();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
