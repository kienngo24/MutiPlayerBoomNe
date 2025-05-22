using Unity.VisualScripting;
using UnityEngine;

public class MoveState_EnemyMelee : GroundState_EnemyMelee
{
    private Transform target;
    public MoveState_EnemyMelee(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }
    
    public override void Enter()
    {
        target = m_enemy.target;
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        if (target == null)
        {
            m_machine.ChangeState<IdleState_EnemyMelee>();
            return;
        }
        m_enemy.transform.position = Vector3.MoveTowards(m_enemy.transform.position,target.transform.position,m_enemy.speed * Time.deltaTime);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
