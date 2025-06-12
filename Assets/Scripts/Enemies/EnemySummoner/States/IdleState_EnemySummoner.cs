using Unity.VisualScripting;
using UnityEngine;

public class IdleState_EnemySummoner : GroundState_EnemySummoner
{
    public IdleState_EnemySummoner(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
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
            _machine.ChangeState<MoveState_EnemySummoner>();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
