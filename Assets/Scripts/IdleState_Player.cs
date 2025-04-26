using Unity.VisualScripting;
using UnityEngine;

public class IdleState_Player : GroundState_Player
{
    
    public IdleState_Player(PlayerController player, IStateMachine stateMachine, string anim) : base(player, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _stateTimer = 3;
    }
    public override void Excute()
    {
        base.Excute();
        if(_stateTimer < 0)
            _machine.ChangeState<MoveState_Player>();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
