using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected PlayerController m_player;
    protected IStateMachine m_machine;
    private Animator m_anim;
    protected string m_animName;
    protected float m_stateTimer;

    public bool Istrigger { get; set; }

    public PlayerState(PlayerController player, IStateMachine stateMachine, string animName)
    {
        m_player = player;
        m_machine = stateMachine;
        m_animName = animName;
        m_anim = player.m_amin; // lấy Animator từ PlayerController
    }
    public virtual void Enter()
    {
        Istrigger = false;
        m_stateTimer = 0;
        m_anim.SetBool(m_animName, true);
    }

    public virtual void Excute()
    {
        m_stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        m_anim.SetBool(m_animName, false);
    }
    public void SetAnimationTrigger() => Istrigger = true;
}
