using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected PlayerController _player;
    protected IStateMachine _machine;
    private Animator _anim;
    protected string _animName;
    protected float _stateTimer;
    public PlayerState(PlayerController player, IStateMachine stateMachine, string animName)
    {
        _player = player;
        _machine = stateMachine;
        _animName = animName;
        _anim = player._amin; // lấy Animator từ PlayerController
    }
    public virtual void Enter()
    {
        _stateTimer = 0;
        _anim.SetBool(_animName, true);
    }

    public virtual void Excute()
    {
        _stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        _anim.SetBool(_animName, false);
    }
}
