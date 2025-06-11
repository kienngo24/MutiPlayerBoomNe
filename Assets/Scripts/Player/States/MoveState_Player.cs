using UnityEngine;

public class MoveState_Player : GroundState_Player
{
    public MoveState_Player(PlayerController player, IStateMachine stateMachine, string anim) : base(player, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_stateTimer = 4;
    }

    public override void Excute()
    {
        base.Excute();
        if(m_player.m_movement.GetPosstion().magnitude == 0)
            m_machine.ChangeState<IdleState_Player>();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
