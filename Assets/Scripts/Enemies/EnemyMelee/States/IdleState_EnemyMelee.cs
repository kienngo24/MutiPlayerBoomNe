using Unity.VisualScripting;
using UnityEngine;

public class IdleState_EnemyMelee : GroundState_EnemyMelee
{
    public IdleState_EnemyMelee(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
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
            _machine.ChangeState<MoveState_EnemyMelee>();        
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
