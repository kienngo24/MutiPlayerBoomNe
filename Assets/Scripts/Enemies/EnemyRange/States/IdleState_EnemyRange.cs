using Unity.VisualScripting;
using UnityEngine;

public class IdleState_EnemyRange : GroundState_EnemyMelee
{
    public IdleState_EnemyRange(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _stateTimer = 1;
    }
    public override void Excute()
    {
        base.Excute();
        if (_stateTimer < 0)
        {
            _machine.ChangeState<MoveState_EnemyRange>();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
