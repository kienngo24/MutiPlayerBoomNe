using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected Enemy _enemy;
    protected IStateMachine _machine;
    private Animator m_anim;
    protected string m_animName;
    protected float _stateTimer;

    public bool Istrigger { get; set; }

    public EnemyState(Enemy enemy, IStateMachine stateMachine, string animName)
    {
        _enemy = enemy;
        _machine = stateMachine;
        m_animName = animName;
        m_anim = enemy._anim; 
    }
    public virtual void Enter()
    {
        Istrigger = false;
        _stateTimer = 0;
        m_anim.SetBool(m_animName, true);
    }
    public void FindTarget()
    {
    
    }
    public virtual void Excute()
    {
        _stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        m_anim.SetBool(m_animName, false);
    }
    public void SetAnimationTrigger() => Istrigger = true;
}
