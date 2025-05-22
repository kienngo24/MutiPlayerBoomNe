using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected Enemy m_enemy;
    protected IStateMachine m_machine;
    private Animator m_anim;
    protected string m_animName;
    protected float m_stateTimer;
    public EnemyState(Enemy enemy, IStateMachine stateMachine, string animName)
    {
        m_enemy = enemy;
        m_machine = stateMachine;
        m_animName = animName;
        m_anim = enemy._anim; 
    }
    public virtual void Enter()
    {
        m_stateTimer = 0;
        m_anim.SetBool(m_animName, true);
    }
    public void FindTarget()
    {
    
    }
    public virtual void Excute()
    {
        m_stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        m_anim.SetBool(m_animName, false);
    }
}
