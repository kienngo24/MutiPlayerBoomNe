using UnityEngine;

public class MoveState_Player : GroundState_Player
{
    public MoveState_Player(PlayerController player, IStateMachine stateMachine, string anim) : base(player, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _stateTimer = 4;
    }

    public override void Excute()
    {
        base.Excute();
        if(_stateTimer < 0)
            _machine.ChangeState<IdleState_Player>();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
