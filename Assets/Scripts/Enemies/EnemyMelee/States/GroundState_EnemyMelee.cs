using Unity.VisualScripting;
using UnityEngine;

public class GroundState_EnemyMelee : EnemyState
{
    public GroundState_EnemyMelee(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
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
            _machine.ChangeState<AttackState_EnemyMelee>();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
