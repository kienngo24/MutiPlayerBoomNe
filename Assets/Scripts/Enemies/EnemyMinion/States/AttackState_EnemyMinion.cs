using Unity.VisualScripting;
using UnityEngine;

public class AttackState_EnemyMinion : EnemyState
{
    public AttackState_EnemyMinion(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        if (Istrigger)
        {
            Debug.Log("IStrigger");
            _machine.ChangeState<IdleState_EnemyMinion>();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
